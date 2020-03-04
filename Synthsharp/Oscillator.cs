/**
 * @file Oscillator.cs
 * @author Gawen Ackermann (gawen.ackrm@eduge.ch), Jonathan Borel-Jaquet (jonathon.brljq@eduge.ch), Fabian Huber (fabian.hbr@eduge.ch)
 * @brief File of the Oscillator class.
 * @version 1.0
 * @date 04.03.2020
 * 
 * @copyright CFPT (c) 2020
 * 
 */
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;

namespace Synthsharp
{
    /// <summary>
    /// An class that can play a note with a certain frequency.
    /// </summary>
    public class Oscillator : IDisposable
    {
        private const int DEFAULT_GAIN = 1;
        private const int DEFAULT_FREQUENCY = 440;
        private const SignalGeneratorType DEFAULT_WAVE_TYPE = SignalGeneratorType.Sin;
        private const bool DEFAULT_ENBALED = true;

        private bool _disposed;
        private readonly WaveOut[] _waveOuts;

        public double Gain { get; set; }
        public int Frequency { get; set; }
        public SignalGeneratorType WaveType { get; set; }
        public bool IsEnabled { get; set; }

        public Oscillator(double pGain, int pFrequency, SignalGeneratorType pType, bool isEnabled)
        {
            Gain = pGain;
            Frequency = pFrequency;
            WaveType = pType;
            IsEnabled = isEnabled;
            _disposed = false;

            _waveOuts = new WaveOut[MIDIPlayer.MAX_MIDI_NOTES];
            for (int i = 0; i < _waveOuts.Length; i++)
            {
                _waveOuts[i] = new WaveOut();
            }
        }

        public Oscillator() : this(DEFAULT_GAIN, DEFAULT_FREQUENCY, DEFAULT_WAVE_TYPE, DEFAULT_ENBALED)
        {
        }

        public void Play(int noteNumber)
        {
            if (IsEnabled)
            {
                var signal = new SignalGenerator()
                {
                    Gain = Gain,
                    Frequency = Frequency,
                    Type = WaveType
                };

                _waveOuts[noteNumber].Init(signal);
                _waveOuts[noteNumber].Play();
            }
        }

        public void Stop(int noteNumber)
        {
            if (IsEnabled)
                _waveOuts[noteNumber].Stop();
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
                foreach (WaveOut waveOut in _waveOuts)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                }
            }
            //Dispose unmanaged objects

            _disposed = true;
        }
    }
}