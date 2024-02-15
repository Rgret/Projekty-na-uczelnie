import threading
import os
from server import Server
from client import Client
import audio_devices as ad
import configparser
import time
import sys

global stop_server_thread
stop_server_thread = threading.Event()

global stop_client_thread
stop_client_thread = threading.Event()

global client
client = False
global server
server = False

config = configparser.ConfigParser()

if not os.path.exists("config.conf"):
    print("No config file. Startup config will begin.")
    ad.print_input_devices()
    in_device = input("Choose your input device: ")

    ad.print_output_devices()
    out_device = input("Choose your output device: ")

    config["Options"] = {"input": in_device, "output": out_device}

    with open('config.conf', 'w') as configfile:
        config.write(configfile)
else:
    config.read("config.conf")
    in_device = int(config.get("Options", "input"))
    out_device = int(config.get("Options", "output"))
    print("Config loaded.")

def clear_line():
    sys.stdout.write("\033[F")
    print('\r' + ' ' * (os.get_terminal_size().columns - 1) + '\r', end='', flush=True)

def main():
    os.system('cls' if os.name == 'nt' else 'clear')
    print("Welcome.")
    exit_flag = False
    connected = False
    while not exit_flag:
        user_input = input()

        global stop_server_thread
        global stop_client_thread

        global client
        global server

        clear_line()
        if user_input.startswith("/"):
            command = user_input[1:].split()
            
            if command[0] == "server":
                if command[1] == 'start' and not server:
                    print("Starting server...")
                    if len(command) == 3:
                        server_thread = threading.Thread(target=serverThread, args=(True, command[2]))
                    elif len(command) == 4:
                        server_thread = threading.Thread(target=serverThread, args=(command[2],command[3]))
                    else:
                        server_thread = threading.Thread(target=serverThread, args=(True, '5555'))
                    server_thread.start()

                elif command[1] == "stop":
                    stop_server_thread.set()
                elif command[1] == "list":
                    print(server.connected_clients)
                else:
                    print("Unknown sub-command for '/server'.")

            elif command[0] == "connect" and not client:
                if len(command) == 2:
                    username = command[1]
                    client_thread = threading.Thread(target=clientThread, args=(username,))
                if len(command) == 3:
                    address = command[1]
                    username = command[2]
                    client_thread = threading.Thread(target=clientThread, args=(username, address))

                client_thread.start()
                connected = True
            
            elif command[0] == "client":
                if not client: print("Not connected to a server")
                if client: print(client.identity())

            elif command[0] == "disconnect":
                stop_client_thread.set()
                connected = False

            elif command[0] == "list":
                if server and not client:
                    print(server.connected_clients)
                elif client:
                    client.getList()

            elif command[0] == "mute" and client:
                if len(command) == 2:
                    client.mute(command[1])
                else:
                    client.muted = True if not client.muted else False
            
            elif command[0] == "unumte" and len(command) == 2 and client:
                client.unmute(command[1])

            elif command[0] == "deafen" and client:
                client.deafen = True if not client.deafen else False

            elif command[0] == "block" and client and len(command) == 2:
                client.block(command[1])

            elif command[0] == "pardon" and client and len(command) == 2:
                client.pardon(command[1])

            elif command[0] == "help":
                pass

            elif command[0] == "settings":
                config.read("config.conf")
                print("""
                      [1] Change input device\n
                      [2] Change output device\n""")
                choice = input()
                if choice == '1':
                    in_device = input("Choose your input device: ")
                    config["Options"] = {"input": in_device, "output": out_device}
                    with open('config.conf', 'w') as configfile:
                        config.write(configfile)
                    if client:
                        client.inputdevice = int(in_device)
                elif choice == '2':
                    out_device = input("Choose your output device: ")
                    config["Options"] = {"input": in_device, "output": out_device}
                    with open('config.conf', 'w') as configfile:
                        config.write(configfile)
                    if client:
                        client.outputDevice = int(out_device)

            elif command[0] == "exit":
                exit_flag = True
                print("Exiting...")
                if client:
                    stop_client_thread.set()
                if server:
                    stop_server_thread.set()
                sys.exit()
            else:
                print("Unknown command. Type '/help' for available commands.")
        elif connected:
            client.sendText(user_input)
        else:
            print("")

def serverThread(logging, port):
    global server
    server = Server(logging, port)
    server.connect()
    
    while not stop_server_thread.is_set():
        time.sleep(1)

    server.close()
    del server
    print("Server stopped")
    server = False
    stop_server_thread.clear()

def clientThread(username, address = "127.0.0.1:5555"):
    global client
    client = Client(in_device, out_device, username)
    client.connect(address)

    while not stop_client_thread.is_set() and client._isConnected:
        time.sleep(1)

    client.disconnect()
    print("Disconnected")
    del client
    client = False
    stop_client_thread.clear()

if __name__ == "__main__":
    main()
    print("ex")
