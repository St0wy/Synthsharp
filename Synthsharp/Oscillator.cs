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
    public class Oscillator
    {
        private const int TIME = 1;
        private const int DEFAULT_GAIN = 1;
        private const int DEFAULT_FREQUENCY = 440;
        private const SignalGeneratorType DEFAULT_SIGNAL_TYPE = SignalGeneratorType.Sin;
        private const bool DEFAULT_ENBALED = true;

        private double _gain;
        private int _frequency;
        private SignalGeneratorType _waveType;
        private WaveOut _waveOut;
        private bool _isEnabled;

        public double Gain { get => _gain; set => _gain = value; }
        public int Frequency { get => _frequency; set => _frequency = value; }
        public SignalGeneratorType WaveType { get => _waveType; set => _waveType = value; }
        public WaveOut WaveOut { get => _waveOut; set => _waveOut = value; }
        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

        public Oscillator(double pGain, int pFrequency, SignalGeneratorType pType, bool isEnabled)
        {
            Gain = pGain;
            Frequency = pFrequency;
            WaveOut = new WaveOut();
            WaveType = pType;
            IsEnabled = isEnabled;
        }

        public Oscillator() : this(DEFAULT_GAIN, DEFAULT_FREQUENCY, DEFAULT_SIGNAL_TYPE, DEFAULT_ENBALED)
        {
            
        }

        public void CreateWave()
        {
            if (IsEnabled)
            {
                var signal = new SignalGenerator()
                {
                    Gain = Gain,
                    Frequency = Frequency,
                    Type = WaveType
                }.Take(TimeSpan.FromSeconds(TIME));

                WaveOut = new WaveOut();
                WaveOut.Init(signal);
                WaveOut.Play();
            }
        }

        public void StopWave()
        {
            WaveOut.Stop();
        }
    }
}

