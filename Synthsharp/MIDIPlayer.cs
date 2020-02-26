using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Midi;

namespace Synthsharp
{
    class MIDIPlayer : IDisposable
    {
        private const string MIDI_IN_MESSAGE_ERROR = "ERROR";
        private const double A_FREQUENCY = 440.0;  //the A (la) note is 440hz
        private const int MIDI_NOTES_SIZE = 127;

        private bool disposed;
        private int _deviceIndex;
        private MidiIn _device;
        private string _inputDetails;
        private readonly double[] _midiNotes;

        public Oscillator O1 { get; private set; }
        public Oscillator O2 { get; private set; }
        public Oscillator O3 { get; private set; }
        public int DeviceIndex { get => _deviceIndex; private set => _deviceIndex = value; }
        public MidiIn Device { get => _device; private set => _device = value; }
        public string InputDetails { get => _inputDetails; set => _inputDetails = value; }

        public MIDIPlayer(int pDeviceIndex, Oscillator o1, Oscillator o2, Oscillator o3)
        {
            DeviceIndex = pDeviceIndex;
            Device = new MidiIn(DeviceIndex);
            
            O1 = o1;
            O2 = o2;
            O3 = o3;

            _midiNotes = ComputeMidiNotes();

            disposed = false;
        }

        public void Start()
        {
            Device.MessageReceived += MidiIn_MessageReceived;
            Device.ErrorReceived += MidiIn_ErrorReceived;
            Device.Start();
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
                int frequency = (int)_midiNotes[ne.NoteNumber];
                Debug.Print($"Frequency: {frequency}, NoteNumber: {ne.NoteNumber}");

                O1.Frequency = frequency;
                O1.Play();

                O2.Frequency = (int)_midiNotes[ne.NoteNumber];
                O2.Play();

                O3.Frequency = (int)_midiNotes[ne.NoteNumber];
                O3.Play();
            }
            else if (e.MidiEvent is ControlChangeEvent cce)
            {
                InputDetails = string.Format(" Time : {0} ControllerNumber : {1} ControllerValue {2} ",
                e.Timestamp, cce.Controller, cce.ControllerValue);

            }
        }

        /// <summary>
        /// Computes the frequency for the note number.
        /// Pass the note number as the index of the array.
        /// 
        /// This piece of code is from: http://subsynth.sourceforge.net/midinote2freq.html
        /// </summary>
        /// <returns></returns>
        private double[] ComputeMidiNotes()
        {
            double[] midiNotes = new double[MIDI_NOTES_SIZE];
            for (int i = 0; i < MIDI_NOTES_SIZE; i++)
            {
                midiNotes[i] = (A_FREQUENCY / 32.0) * Math.Pow(2.0, ((i - 9.0) / 12.0));
            }

            return midiNotes;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //Dispose native and managed objects
                Device.Stop();
                Device.Dispose();
            }
            //Dispose unmanaged objects

            disposed = true;
        }
    }
}
