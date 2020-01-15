using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synthsharp
{
    class Oscillator
    {
        private const int SAMPLE_RATE = 44100;
        enum SelectedWave { Sine = 0, Square = 1, Triangle = 2, Sawtooth = 3, Noise = 4 };

        short[] _wave;
        float _frequency;
        Random _rdm;
        short _tmpSample;
        int _samplesPerWaveLength;
        short _ampStep;
        short _amplitude;

        public short[] Wave { get => _wave; set => _wave = value; }
        public float Frequency { get => _frequency; set => _frequency = value; }
        public Random Rdm { get => _rdm; set => _rdm = value; }
        public short TmpSample { get => _tmpSample; set => _tmpSample = value; }
        public int SamplesPerWaveLength { get => _samplesPerWaveLength; set => _samplesPerWaveLength = value; }
        public short AmpStep { get => _ampStep; set => _ampStep = value; }
        public short Amplitude { get => _amplitude; set => _amplitude = value; }

        public Oscillator(short[] pWave, float pFrequency, Random pRdm, short pTmpSample, int pSamplesPerWaveLength, short pAmpStep, short pAmplitude)
        {
            this._wave = pWave;
            this._frequency = pFrequency;
            this._rdm = pRdm;
            this._tmpSample = pTmpSample;
            this._samplesPerWaveLength = pSamplesPerWaveLength;
            this._ampStep = pAmpStep;
            this._amplitude = pAmplitude;
        }

        public void ConstructWave(int index) {
            /* Algorithms are available at this address : 
             * https://blogs.msdn.microsoft.com/dawate/2009/06/25/intro-to-audio-programming-part-4-algorithms-for-different-sound-waves-in-c/ */

            /* 
            short.MaxValue is the Amplitude 
            ((Math.PI * 2 * frequency) / SAMPLE_RATE) is the t (angular frequency)
            i is itself (time unit) 
            */
            switch (index)
            {
                case (int)SelectedWave.Sine:
                    /* Sine wave */
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        Wave[i] = Convert.ToInt16(Amplitude * Math.Sin(((Math.PI * 2 * Frequency) / SAMPLE_RATE) * i));
                    }
                    break;
                case (int)SelectedWave.Square:
                    /* Square wave */
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        Wave[i] = Convert.ToInt16(Amplitude * Math.Sign(Math.Sin(((Math.PI * 2 * Frequency) / SAMPLE_RATE) * i)));
                    }
                    break;
                case (int)SelectedWave.Sawtooth:
                    /* Triangle wave */
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        /* Used to reset the slope */
                        if (Math.Abs(TmpSample + AmpStep) > Amplitude)
                        {
                            AmpStep = (short)-AmpStep;
                        }
                        TmpSample += AmpStep;
                        Wave[i] = TmpSample;
                    }
                    break;
                case (int)SelectedWave.Triangle:
                    /* Sawtooth wave */
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        for (int j = 0; j < SamplesPerWaveLength && i < SAMPLE_RATE; j++)
                        {
                            TmpSample += AmpStep;
                            Wave[i++] = Convert.ToInt16(TmpSample);
                        }
                        i--;
                    }
                    break;
                case (int)SelectedWave.Noise:
                    /* Noise wave */
                    for (int i = 0; i < SAMPLE_RATE; i++)
                    {
                        Wave[i] = (short)Rdm.Next(-Amplitude, Amplitude);
                    }
                    break;
                default:
                    break;

            }
        }
    }
}
