using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Midi;

namespace Synthsharp
{
    class MIDIPlayer : IDisposable
    {
        private const string NOTE_C = "C";
        private const string MIDI_IN_MESSAGE_ERROR = "ERROR";
        private const int A_FREQUENCY = 440;  //the A (la) note is 440hz
        private const int MIDI_NOTES_SIZE = 127;

        private Oscillator _o1;
        private Oscillator _o2;
        private Oscillator _o3;
        private int _deviceIndex;
        private MidiIn _device;
        private string _inputDetails;
        private float[] _midiNotes;

        public Oscillator O1 { get => _o1; private set => _o1 = value; }
        public Oscillator O2 { get => _o2; private set => _o2 = value; }
        public Oscillator O3 { get => _o3; private set => _o3 = value; }
        public int DeviceIndex { get => _deviceIndex; private set => _deviceIndex = value; }
        public MidiIn Device { get => _device; private set => _device = value; }
        public string InputDetails { get => _inputDetails; set => _inputDetails = value; }

        public MIDIPlayer(int pDeviceIndex, Oscillator o1, Oscillator o2, Oscillator o3)
        {
            DeviceIndex = pDeviceIndex;
            Device = new MidiIn(DeviceIndex);
            Device.MessageReceived += MidiIn_MessageReceived;
            Device.ErrorReceived += MidiIn_ErrorReceived;
            Device.Start();

            O1 = o1;
            O2 = o2;
            O3 = o3;

            _midiNotes = ComputeMidiNotes();
        }

        private void MidiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            InputDetails = MIDI_IN_MESSAGE_ERROR;
        }

        private void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            if (e.MidiEvent is NoteEvent ne)
            {
                InputDetails = string.Format(" Time : {0} NoteName : {1} N° Key {2} ",
                e.Timestamp, ne.NoteName, ne.NoteNumber);

                O1.Frequency = (int)_midiNotes[ne.NoteNumber];
                O1.CreateWave();

                O2.Frequency = (int)_midiNotes[ne.NoteNumber];
                O2.CreateWave();

                O3.Frequency = (int)_midiNotes[ne.NoteNumber];
                O3.CreateWave();
            }
            else if (e.MidiEvent is ControlChangeEvent)
            {
                ControlChangeEvent cce = (e.MidiEvent as ControlChangeEvent);
                InputDetails = string.Format(" Time : {0} ControllerNumber : {1} ControllerValue {2} ",
                e.Timestamp, cce.Controller, cce.ControllerValue);

            }
        }

        private float[] ComputeMidiNotes()
        {
            float[] midiNotes = new float[MIDI_NOTES_SIZE];
            for (int i = 0; i < MIDI_NOTES_SIZE; i++)
            {
                midiNotes[i] = (A_FREQUENCY / 32) * (2 ^ ((i - 9) / 12));
            }

            return midiNotes;
        }

        public void Dispose()
        {
            Device.Stop();
            Device.Dispose();
        }
    }
}
