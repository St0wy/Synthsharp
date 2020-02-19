using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Midi;

namespace Synthsharp
{
    class MIDIManagement
    {
        private const string NOTE_C = "C";
        private const string MIDI_IN_MESSAGE_ERROR = "ERROR";
        private int _deviceIndex;
        private MidiIn _device;
        private string _inputDetails;
        public int DeviceIndex { get => _deviceIndex; private set => _deviceIndex = value; }
        public MidiIn Device { get => _device; private set => _device = value; }
        public string InputDetails { get => _inputDetails; set => _inputDetails = value; }

        public MIDIManagement(int pDeviceIndex)
        {
            DeviceIndex = pDeviceIndex;
        }
        public void Load()
        {
            Device = new MidiIn(DeviceIndex);
            Device.MessageReceived += midiIn_MessageReceived;
            Device.ErrorReceived += midiIn_ErrorReceived;
            Device.Start();
        }
        private void midiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            InputDetails = MIDI_IN_MESSAGE_ERROR;
        }
        private void midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {    
            if (e.MidiEvent is NoteEvent)
            {
                NoteEvent ne = (e.MidiEvent as NoteEvent);
                string note = ne.NoteName.Substring(0, 1);
                string octave = ne.NoteName.Substring(1, 1);
                InputDetails = string.Format(" Time : {0} NoteName : {1} N° Key {2} ",
                e.Timestamp, ne.NoteName, ne.NoteNumber);                
                if (note == NOTE_C)
                {
                    InputDetails = note + octave;
                }
            }
            else if (e.MidiEvent is ControlChangeEvent)
            {
                ControlChangeEvent cce = (e.MidiEvent as ControlChangeEvent);
                InputDetails = string.Format(" Time : {0} ControllerNumber : {1} ControllerValue {2} ",
                e.Timestamp, cce.Controller, cce.ControllerValue);

            }
        }
    }
}
