import zmq
import sounddevice as sd
import numpy as np
import json
import socket
import threading
import pyaudio
import base64

FORMAT = np.int16
CHANNELS = 1
RATE = 44100
CHUNK = 1024

class Client:
    def __init__(self, inputDevice, outputDevice, username):
        p = pyaudio.PyAudio()
        self.inputdevice = p.open(format=pyaudio.paInt16,
                channels=1,
                rate=44100,
                input=True,
                frames_per_buffer=2048,
                input_device_index=inputDevice)
        self.outputDevice = p.open(format=pyaudio.paInt16,
                channels=1,
                rate=44100,
                output=True,
                frames_per_buffer=2048,
                output_device_index=outputDevice)
        self.muted = False
        self.deafen = False
        self.closing = False
        self._isConnected = True
        self.blockedUsers = []
        self.mutedUsers = []
        self.client_id = username
        self.ip = self.__get_local_ip()
        self.lock = threading.Lock()
        self.setInputDevice()

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

    def setInputDevice(self):
        pass  

    def clear(self):
        self.stop_client.clear()

    def __listen(self):
        try:
            poller = zmq.Poller()
            poller.register(self.socket, zmq.POLLIN)
            socks = dict(poller.poll(timeout=1000))  # Timeout in milliseconds
            while not self.stop_client.is_set():
                if self.socket in socks and socks[self.socket] == zmq.POLLIN:
                    sender, message = self.socket.recv_multipart()
                    #print(message)
                    data = json.loads(message)
                    self.__interpret(data, sender)
        except:
            pass            
                
    def __audioInput(self):
        while not self.stop_client.is_set():
            if self.muted: continue
            audio_data = self.inputdevice.read(2048)
            encoded_audio_data = base64.b64encode(audio_data).decode("utf-8")
            try:
                self.socket_audio.send_multipart([json.dumps({'type': 'audio', 'data': encoded_audio_data}).encode("utf-8")])
            except:
                pass

    def __audioOutput(self):
        try:
            poller = zmq.Poller()
            poller.register(self.socket_audio, zmq.POLLIN)
            socks = dict(poller.poll(timeout=10))  # Timeout in milliseconds
            while not self.stop_client.is_set():
                if self.socket_audio in socks and socks[self.socket_audio] == zmq.POLLIN:
                    if self.deafen: continue
                    sender, message = self.socket_audio.recv_multipart()
                    data = json.loads(message)
                    decoded_audio_data = base64.b64decode(data['data'])
                    if not sender == self.socket.getsockopt(zmq.IDENTITY) and not sender.decode("utf-8") in self.blockedUsers and not sender.decode("utf-8") in self.mutedUsers:
                        self.outputDevice.write(decoded_audio_data)
                    else:
                        decoded_audio_data = 0
        except:
            pass 

    def __interpret(self, data, sender):
        data_type = data['type']
        if sender.decode("utf-8") in self.blockedUsers: return
        if data_type == 'text':
            print(f"{sender.decode("utf-8")}: {data['data']}")
        if data_type == "serverClosing":
            self.disconnect()
        elif data_type == 'handshake':
            if data['data'] == 'success':
                print(f"[Client] Connected as {self.socket.getsockopt(zmq.IDENTITY)}")
            else:
                print(f"[Client] Connection failed.")
                self.disconnect()
        elif data_type == 'query':
            print(json.loads(data['data']))

    def block(self, user):
        print(f"[Client] User {user} has been blocked.")
        self.blockedUsers.append(user)

    def pardon(self, user):
        print(f"[Client] User {user} has been pardoned.")
        self.blockedUsers.remove(user)

    def mute(self, user):
        print(f"[Client] User {user} has been muted.")
        self.mutedUsers.append(user)

    def unmute(self ,user):
        print(f"[Client] User {user} has been unmuted.")
        self.mutedUsers.remove(user)

    def sendText(self, message = ""):
        self.socket.send_multipart([json.dumps({'type': 'text', 'data': message}).encode("utf-8")])

    def getList(self):
        self.socket.send_multipart([json.dumps({'type': 'query', 'data': 'connected_clients'}).encode("utf-8")])

    def identity(self):
        return f"{self.socket.getsockopt(zmq.IDENTITY)}"

    def disconnect(self):
        self.closing = True
        
        self.socket.send_multipart([json.dumps({'type': 'disconnect'}).encode("utf-8")])

        self.stop_client.set()
        self.socket.close()
        self.context.term()
        self.socket_audio.close()
        self.context_audio.term()
        self.listen_thread.join()
        self.audioInput_thread.join()
        self.audioOutput_thread.join()
        self._isConnected = False
        self.closing = False

    def connect(self, addr="127.0.0.1:5555"):
        
        print(f"Connecting to {addr}")
        self.context = zmq.Context()
        self.context_audio = zmq.Context()
        self.context_wassup = zmq.Context()
        ip, port = addr.split(':')
        self.socket = self.context.socket(zmq.DEALER)
        self.socket.setsockopt(zmq.IDENTITY, self.client_id.encode("utf-8"))
        self.socket.connect(f"tcp://{addr}")

        self.socket_audio = self.context_audio.socket(zmq.DEALER)
        self.socket_audio.setsockopt(zmq.IDENTITY, self.client_id.encode("utf-8"))
        self.socket_audio.connect(f"tcp://{ip}:{int(port)+1}")

        self.stop_client = threading.Event()

        self.socket.send_multipart([json.dumps({"type": "handshake", "username": self.client_id}).encode('utf-8')])
        print("Awaiting Connection...")
        try:
            poller = zmq.Poller()
            poller.register(self.socket, zmq.POLLIN)
            socks = dict(poller.poll(timeout=1000))  # Timeout in milliseconds
            if self.socket in socks and socks[self.socket] == zmq.POLLIN:
                sender, message = self.socket.recv_multipart()
                self.__interpret(json.loads(message), sender)
        except:
            pass

        self.listen_thread = threading.Thread(target=self.__listen)
        self.listen_thread.start()

        self.audioInput_thread = threading.Thread(target=self.__audioInput)
        self.audioInput_thread.start()

        self.audioOutput_thread = threading.Thread(target=self.__audioOutput)
        self.audioOutput_thread.start()