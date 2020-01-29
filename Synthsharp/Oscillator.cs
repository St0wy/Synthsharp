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
    class Oscillator
    {
        private double _gain;
        private int _frequency;
        private SignalGeneratorType _waveType;
        private WaveOut _waveOut;

        public double Gain { get => _gain; set => _gain = value; }
        public int Frequency { get => _frequency; set => _frequency = value; }
        public SignalGeneratorType WaveType { get => _waveType; set => _waveType = value; }
        public WaveOut WaveOut { get => _waveOut; set => _waveOut = value; }

        public Oscillator(double pGain, int pFrequency, WaveOut pWaveOut)
        {
            this.Gain = pGain;
            this.Frequency = pFrequency;
            this.WaveOut = pWaveOut;
        }

        public void CreateSineWave(double pGain, int pFrequency) {
            var sine = new SignalGenerator()
            {
                Gain = pGain,
                Frequency = pFrequency,
                Type = SignalGeneratorType.Sin
            }.Take(TimeSpan.FromSeconds(1));

            WaveOut = new WaveOut();
            WaveOut.Init(sine);
            WaveOut.Play();

        }

        public void StopWave() {
            WaveOut.Stop();
        }
    }
}
