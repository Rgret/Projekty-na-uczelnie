import zmq
import json
import time
import socket
import threading

class Server:
    def __init__(self, log = False, port = '5555'):
        self.connected_clients = []
        self.wassupList = []
        self.logging = log
        self.port = port
        self.__ip = self.__get_local_ip()

    def _log(self, message):
        if self.logging:
            print(f"[Server] "+message)

    def clear(self):
        self.stop_server.clear()

    def __get_local_ip(self):
        try:
            # Create a socket object
            s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

            # Connect to any remote server (does not send any data)
            s.connect(("8.8.8.8", 80))

            # Get the local IP address
            local_ip = s.getsockname()[0]

            # Close the socket
            s.close()

            return local_ip
        except socket.error as e:
            print(f"Error: {e}")
            return None

    def __start(self):
        poller = zmq.Poller()
        poller.register(self.socket, zmq.POLLIN)
        print("[Server] Listening...")
        while not self.stop_server.is_set():
            socks = dict(poller.poll(timeout=100)) 

            if self.socket in socks and socks[self.socket] == zmq.POLLIN:
                message = self.socket.recv_multipart()
                client_id, data = message
                data = json.loads(data)
                self.__broadcast(client_id, data, self.socket)
        
        self._log("Server Closed.")
        self.socket.close()
        self.context.term()

    def __start_Audio(self):
        try:
            poller = zmq.Poller()
            poller.register(self.socket_audio, zmq.POLLIN)
            print("[Server] Listening...")
            while not self.stop_server.is_set():
                socks = dict(poller.poll(timeout=100)) 

                if self.socket_audio in socks and socks[self.socket_audio] == zmq.POLLIN:
                    message = self.socket_audio.recv_multipart()
                    client_id, data = message 
                    data = json.loads(data)
                    self.__broadcast(client_id, data, self.socket_audio)
        except:
            pass
        self._log("Server Closed.")
        self.socket_audio.close()
        self.context.term()

    def __broadcast(self, client_id, data, soc):
        data_type = data['type']
        if data_type == 'text':
            self._log(f"Received text data from {client_id}: {data}")
            if client_id not in self.connected_clients:
                self.connected_clients.append(client_id)
                self._log(f"Added [{client_id}]  to clients")
            for user in self.connected_clients:
                soc.send_multipart([user, client_id, json.dumps({'type': 'text', 'data': data['data']}).encode("utf-8")])
        elif data_type == 'audio':
            for user in self.connected_clients:
                if user != client_id:
                    soc.send_multipart([user, client_id, json.dumps({'type': 'audio', 'data': data['data']}).encode("utf-8")])
        elif data_type == 'query':
            if data['data'] == 'connected_clients':
                self._log(f"Sending client list to [{client_id}]")
                soc.send_multipart([client_id, b'server', json.dumps({'type': 'answer', 'data': f'{self.connected_clients}'}).encode("utf-8")])
        elif data_type == 'disconnect':
            self._log(f"User disconnected [{client_id}]")
            soc.send_multipart([client_id, b"server", json.dumps({'type': 'disconnected', 'data': 'trash'}).encode("utf-8")])
            self.connected_clients.remove(client_id)
        elif data_type == 'handshake':
            self._log(f"Handshake from [{client_id}] '{data['username']}'")
            if client_id not in self.connected_clients:
                self.connected_clients.append(client_id)
                self._log(f"Added [{client_id}]  to clients")
                soc.send_multipart([client_id, b"server", json.dumps({'type': 'handshake', 'data': 'success'}).encode("utf-8")])
            else:
                soc.send_multipart([client_id, b"server", json.dumps({'type': 'handshake', 'data': 'nameTaken'}).encode("utf-8")])
            
    def connect(self):
        self.context = zmq.Context()
        self.socket = self.context.socket(zmq.ROUTER)
        self.socket.bind(f"tcp://*:{self.port}")

        self.context = zmq.Context()
        self.socket_audio = self.context.socket(zmq.ROUTER)
        self.socket_audio.bind(f"tcp://*:{int(self.port)+1}")

        self.stop_server = threading.Event()
        self.server_thread = threading.Thread(target=self.__start, daemon=True)
        self.audio_thread = threading.Thread(target=self.__start_Audio, daemon=True)
        self.audio_thread.start()
        self.server_thread.start()
        self._log(f"Server listening on tcp://{self.__ip}:{self.port}")

    def close(self):
        self._log("Server closing...")
        self.stop_server.set()
        self.server_thread.join() 