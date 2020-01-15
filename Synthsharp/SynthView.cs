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
        enum SelectedWave { Sine = 0, Square = 1, Triangle = 2, Sawtooth = 3, Noise = 4 };
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        private const float DEFAULT_FREQUENCY = 440f;
        private const float FREQUENCY_FOR_C2_NOTE = 65.4f;
        private const float FREQUENCY_FOR_C3_NOTE = 130.8f;
        private const float FREQUENCY_FOR_C4_NOTE = 261.6f;
        private const float FREQUENCY_FOR_C5_NOTE = 523.2f;
        private const float FREQUENCY_FOR_C6_NOTE = 1046.5f;
        private const float FREQUENCY_FOR_C7_NOTE = 2093f;
        private const float FREQUENCY_FOR_C8_NOTE = 4186f;
        private readonly string[] DEFAULT_COMBO_BOX = { "Sine", "Square", "Saw", "Triangle", "Noise" };

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
            InitializeComboBox(new ComboBox[] { cbxOscillator1, cbxOscillator2, cbxOscillator3 }, DEFAULT_COMBO_BOX);
            this.KeyPreview = true; /* Very important to prevent focus on controls */
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

        /// <summary>
        /// Initialize the combobox values
        /// </summary>
        /// <param name="cbs">Array of ComboBox</param>
        /// <param name="dataCbx">Elements of the ComboBox</param>
        public void InitializeComboBox(ComboBox[] cbs, string[] dataCbx)
        {
            foreach (ComboBox combobox in cbs)
            {
                combobox.Items.AddRange(dataCbx);
            }
        }

        private void cbxOscillator1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SynthView_KeyDown(object sender, KeyEventArgs e)
        {

            short[] wave = new short[SAMPLE_RATE];
            float frequency = DEFAULT_FREQUENCY;

            Random rdm = new Random();

            short tmpSample = -short.MaxValue;
            int samplesPerWaveLength = (int)(SAMPLE_RATE / frequency);
            short ampStep = (short)((short.MaxValue * 2) / samplesPerWaveLength);
            short amplitude = (short)trbOscillator1.Value;

            /* Frenquencies available at this address : https://en.wikipedia.org/wiki/Piano_key_frequencies (using C2 to C8)*/
            switch (e.KeyCode)
            {
                case Keys.Y:
                    frequency = FREQUENCY_FOR_C2_NOTE;
                    break;
                case Keys.X:
                    frequency = FREQUENCY_FOR_C3_NOTE;
                    break;
                case Keys.C:
                    frequency = FREQUENCY_FOR_C4_NOTE;
                    break;
                case Keys.V:
                    frequency = FREQUENCY_FOR_C5_NOTE;
                    break;
                case Keys.B:
                    frequency = FREQUENCY_FOR_C6_NOTE;
                    break;
                case Keys.H:
                    frequency = FREQUENCY_FOR_C7_NOTE;
                    break;
                case Keys.M:
                    frequency = FREQUENCY_FOR_C8_NOTE;
                    break;
                default:
                    break;
            }
            Oscillator o = new Oscillator(wave, frequency, rdm, tmpSample, samplesPerWaveLength, ampStep, amplitude);

            int index = cbxOscillator1.SelectedIndex;
            o.ConstructWave(index);
            //new SoundPlayer(createWave(wave)).Play();
        }
    }
}
