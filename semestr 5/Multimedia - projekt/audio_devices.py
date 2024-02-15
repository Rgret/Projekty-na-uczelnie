import pyaudio
import sounddevice as sd

def print_input_devices():
    p = pyaudio.PyAudio()

    print("Available input devices:")
    for i in range(p.get_device_count()):
        device_info = p.get_device_info_by_index(i)
        if device_info['maxInputChannels'] > 0:
            print(f"Input Device {i}: {device_info['name']}")

    p.terminate()

def print_output_devices():
    p = pyaudio.PyAudio()

    print("\nAvailable output devices:")
    print(sd.query_devices(kind='output'))
    
    p.terminate()