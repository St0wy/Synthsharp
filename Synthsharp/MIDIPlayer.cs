/**
 * @file MIDIPlayer.cs
 * @author Gawen Ackermann (gawen.ackrm@eduge.ch), Jonathan Borel-Jaquet (jonathon.brljq@eduge.ch), Fabian Huber (fabian.hbr@eduge.ch)
 * @brief File of the MIDIPlayer class.
 * @version 1.0
 * @date 04.03.2020
 * 
 * @copyright CFPT (c) 2020
 * 
 */
using NAudio.Midi;
using System;
using System.Diagnostics;

namespace Synthsharp
{
    /// <summary>
    /// A class that can recieve midi events and play notes with the Oscillator class.
    /// </summary>
    public class MIDIPlayer : IDisposable
    {
        private const string MIDI_IN_MESSAGE_ERROR = "ERROR";
        private const double A_FREQUENCY = 440.0;  //the A (la) note is 440hz
        public const int MAX_MIDI_NOTES = 128;

        private readonly double[] _midiNotes;
        private bool _disposed;

        public Oscillator O1 { get; private set; }
        public Oscillator O2 { get; private set; }
        public Oscillator O3 { get; private set; }
        public int DeviceIndex { get; private set; }
        public MidiIn Device { get; private set; }

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

        /// <summary>
        /// Starts listening to midi events.
        /// </summary>
        public void Start()
        {
            Device.MessageReceived += MidiIn_MessageReceived;
            Device.ErrorReceived += MidiIn_ErrorReceived;
            Device.Start();
        }

        /// <summary>
        /// Method called when the device recieves an error. It prints an error message in the debug window.
        /// </summary>
        private void MidiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            Debug.Print(MIDI_IN_MESSAGE_ERROR);
        }

        /// <summary>
        /// Method called when the device recieves a midi event. It plays the according note on the oscillators.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            if (e.MidiEvent is NoteEvent ne)
            {
                int noteNumber = ne.NoteNumber;
                Debug.Print($"CommandCode: {ne.CommandCode}");
                if (ne.CommandCode == MidiCommandCode.NoteOn)
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
        }

        /// <summary>
        /// Computes the frequency for the note number.
        /// Pass the note number as the index of the array.
        ///
        /// This piece of code is from: http://subsynth.sourceforge.net/midinote2freq.html
        /// </summary>
        /// <returns>Returns an array where the key is the note number and the value is the frequency.</returns>
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