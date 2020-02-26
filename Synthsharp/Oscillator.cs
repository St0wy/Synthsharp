using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Synthsharp
{
    public class Oscillator : IDisposable
    {
        private const int DEFAULT_GAIN = 1;
        private const int DEFAULT_FREQUENCY = 440;
        private const SignalGeneratorType DEFAULT_WAVE_TYPE = SignalGeneratorType.Sin;
        private const bool DEFAULT_ENBALED = true;

        private double _gain;
        private int _frequency;
        private SignalGeneratorType _waveType;
        private bool _isEnabled;
        private bool _disposed;
        private WaveOut[] _waveOuts;


        public double Gain { get => _gain; set => _gain = value; }
        public int Frequency { get => _frequency; set => _frequency = value; }
        public SignalGeneratorType WaveType { get => _waveType; set => _waveType = value; }
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

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
            if(IsEnabled)
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
                //Dispose native and managed objects$
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

