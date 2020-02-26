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
        public const int MAX_MIDI_NOTES = 128;

        private int _deviceIndex;
        private MidiIn _device;
        private readonly double[] _midiNotes;
        private bool _disposed;

        public Oscillator O1 { get; private set; }
        public Oscillator O2 { get; private set; }
        public Oscillator O3 { get; private set; }
        public int DeviceIndex { get => _deviceIndex; private set => _deviceIndex = value; }
        public MidiIn Device { get => _device; private set => _device = value; }

        public MIDIPlayer(int pDeviceIndex, Oscillator o1, Oscillator o2, Oscillator o3)
        {
            DeviceIndex = pDeviceIndex;
            Device = new MidiIn(DeviceIndex);
            
            O1 = o1;
            O2 = o2;
            O3 = o3;

            _midiNotes = ComputeMidiNotes();

            _disposed = false;
        }

        public void Start()
        {
            Device.MessageReceived += MidiIn_MessageReceived;
            Device.ErrorReceived += MidiIn_ErrorReceived;
            Device.Start();
        }

        private void MidiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            Debug.Print(MIDI_IN_MESSAGE_ERROR);
        }

        private void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            if (e.MidiEvent is NoteEvent ne)
            {
                int noteNumber = ne.NoteNumber;
                Debug.Print($"CommandCode: {ne.CommandCode}");
                if(ne.CommandCode == MidiCommandCode.NoteOn)
                {
                    Debug.Print($"noteNumber: {noteNumber}");
                    int frequency = (int)_midiNotes[noteNumber];

                    O1.Frequency = frequency;
                    O1.Play(noteNumber);

                    O2.Frequency = frequency;
                    O2.Play(noteNumber);

                    O3.Frequency = frequency;
                    O3.Play(noteNumber);
                }
                else if (ne.CommandCode == MidiCommandCode.NoteOff)
                {
                    O1.Stop(noteNumber);
                    O2.Stop(noteNumber);
                    O3.Stop(noteNumber);
                }
                
            }
            else if (e.MidiEvent is ControlChangeEvent cce)
            {
                //ControlChangeEvent
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
            double[] midiNotes = new double[MAX_MIDI_NOTES];
            for (int i = 0; i < MAX_MIDI_NOTES; i++)
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
            if (_disposed)
                return;

            if (disposing)
            {
                //Dispose native and managed objects
                Device.Stop();
                Device.Dispose();
            }
            //Dispose unmanaged objects

            _disposed = true;
        }
    }
}
