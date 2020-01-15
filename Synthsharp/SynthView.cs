using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Commons.Music.Midi;

namespace Synthsharp
{
    public partial class SynthView : Form
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        private const float DEFAULT_FREQUENCY = 440f;

        public SynthView()
        {
            InitializeComponent();
        }

        private void SynthView_KeyPress(object sender, KeyPressEventArgs e)
        {
            short[] wave = new short[SAMPLE_RATE];
            float frequency = DEFAULT_FREQUENCY;
            /* Algorithms are available at this address : https://blogs.msdn.microsoft.com/dawate/2009/06/25/intro-to-audio-programming-part-4-algorithms-for-different-sound-waves-in-c/ */

            /* 
            short.MaxValue is the Amplitude 
            ((Math.PI * 2 * frequency) / SAMPLE_RATE) is the t (angular frequency)
            i is itself (time unit) 
            */

            /* Sine wave */
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                /* 
                 Equation used 
                 Sample = Amplitude * sin(t*i)
                */

                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i));
            }
            /* Square wave */
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                /* 
                Equation used
                Sample = Amplitude * sgn(sin(t * i)) 
                */
                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sign(Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i)));
            }

            short tmpSample = -short.MaxValue;
            int samplesPerWaveLength = (int)(SAMPLE_RATE / frequency);
            short ampStep = (short)((short.MaxValue * 2) / samplesPerWaveLength);

            /* Triangle wave */
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                /* Used to reset the slope */
                if (Math.Abs(tmpSample + ampStep) > short.MaxValue)
                {
                    ampStep = (short)-ampStep;
                }
                tmpSample += ampStep;
                wave[i] = tmpSample;
            }
            /* Sawtooth wave */
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                for (int j = 0; j < samplesPerWaveLength && i < SAMPLE_RATE; j++)
                {
                    tmpSample += ampStep;
                    wave[i++] = Convert.ToInt16(tmpSample);
                }
                i--;
            }

            new SoundPlayer(CreateWave(wave)).Play();
        }
        /// <summary>
        /// Create a sound in wave format
        /// Encoding options for a .wav
        ///     description available at this address : http://soundfile.sapp.org/doc/WaveFormat/
        /// </summary>
        /// <param name="binaryWriter"></param>
        /// <returns>The MemoryStream encoded</returns>
        public MemoryStream CreateWave(short[] wave)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

            short sizeOfShort = sizeof(short);
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeOfShort];

            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeOfShort);

            short blockAlign = BITS_PER_SAMPLE / 8;
            int subChunckTwoSize = SAMPLE_RATE * blockAlign;

            binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
            binaryWriter.Write(36 + subChunckTwoSize);
            binaryWriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
            binaryWriter.Write(16);
            binaryWriter.Write((short)1);
            binaryWriter.Write((short)1);
            binaryWriter.Write(SAMPLE_RATE);
            binaryWriter.Write(SAMPLE_RATE * blockAlign);
            binaryWriter.Write(blockAlign);
            binaryWriter.Write(BITS_PER_SAMPLE);
            binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });
            binaryWriter.Write(subChunckTwoSize);
            binaryWriter.Write(binaryWave);
            memoryStream.Position = 0;
            return memoryStream;
        }

        private void SynthView_Load(object sender, EventArgs e)
        {

            string port = null;
            IMidiAccess access = MidiAccessManager.Default;
            IMidiPortDetails iport = access.Inputs.FirstOrDefault(i => i.Id == port) ?? access.Inputs.Last();
            IMidiInput input = access.OpenInputAsync(iport.Id).Result;
            
            input.MessageReceived += MessageRecieved;
        }

        private void MessageRecieved(object obj, MidiReceivedEventArgs e)
        {

        }
    }
}
