using NAudio.Wave;
using NAudio.Wave.SampleProviders;
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


namespace Synthsharp
{

    public partial class SynthView : Form
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        private const int DEFAULT_FREQUENCY = 440;
        private const int FREQUENCY_FOR_C2_NOTE = 65;
        private const int FREQUENCY_FOR_C3_NOTE = 131;
        private const int FREQUENCY_FOR_C4_NOTE = 262;
        private const int FREQUENCY_FOR_C5_NOTE = 523;
        private const int FREQUENCY_FOR_C6_NOTE = 1047;
        private const int FREQUENCY_FOR_C7_NOTE = 2093;
        private const int FREQUENCY_FOR_C8_NOTE = 4186;

        private const int OSCILLATOR_1 = 1;
        private const int OSCILLATOR_2 = 2;
        private const int OSCILLATOR_3 = 3;

        private const char SINE_CODE = 'S';
        private const char SQUARE_CODE = 'Q';
        private const char SAWTOOTH_CODE = 'A';
        private const char TRIANGLE_CODE = 'T';
        private const char NOISE_CODE = 'N';

        Oscillator o1;
        Oscillator o2;
        Oscillator o3;

        List<Button> Oscillator1Buttons;
        List<Button> Oscillator2Buttons;
        List<Button> Oscillator3Buttons;

        int frequency;

        public SynthView()
        {
            InitializeComponent();
            KeyPreview = true;
            o1 = new Oscillator();
            o2 = new Oscillator();
            o3 = new Oscillator();

            Oscillator1Buttons = new List<Button>()
            {
                btnNoise1,
                btnSawTooth1,
                btnSine1,
                btnSquare1,
                btnTriangle1
            };
            Oscillator2Buttons = new List<Button>()
            {
                btnNoise2,
                btnSawTooth2,
                btnSine2,
                btnSquare2,
                btnTriangle2
            };
            Oscillator3Buttons = new List<Button>()
            {
                btnNoise3,
                btnSawTooth3,
                btnSine3,
                btnSquare3,
                btnTriangle3
            };
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

        private void SynthView_KeyUp(object sender, KeyEventArgs e)
        {
            o1.StopWave();
            o2.StopWave();
            o3.StopWave();
        }

        private void SynthView_KeyDown(object sender, KeyEventArgs e)
        {
            frequency = DEFAULT_FREQUENCY;

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

            if (o1 != null)
            {
                o1.CreateWave(1, frequency);
            }
            if (o2 != null)
            {
                o2.CreateWave(1, frequency);
            }
            if (o3 != null)
            {
                o3.CreateWave(1, frequency);
            }

        }

        private void BtnWaveForm_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                //Disable the button we clicked on
                btn.Enabled = false;

                //The tag should look like this: 1S  ->  Oscillator 1, sine waveform
                string tag = btn.Tag.ToString();
                //The first char of the tag is the oscillator id
                int oscillatorId = (int)Char.GetNumericValue(tag[0]);
                //The second char of the tag is the type of waveform
                char waveType = tag[1];

                if (oscillatorId == OSCILLATOR_1)
                {
                    ChangeWaveType(ref o1, waveType);
                    ManageButtonActivation(Oscillator1Buttons, btn);
                }
                else if (oscillatorId == OSCILLATOR_2)
                {
                    ChangeWaveType(ref o2, waveType);
                    ManageButtonActivation(Oscillator2Buttons, btn);
                }
                else if (oscillatorId == OSCILLATOR_3)
                {
                    ChangeWaveType(ref o3, waveType);
                    ManageButtonActivation(Oscillator3Buttons, btn);
                }
            }
        }

        private void ChangeWaveType(ref Oscillator oscillator, char waveType)
        {
            //Waveform management
            if (waveType == SINE_CODE)
            {
                oscillator.WaveType = SignalGeneratorType.Sin;
            }
            else if (waveType == SQUARE_CODE)
            {
                oscillator.WaveType = SignalGeneratorType.Square;
            }
            else if (waveType == SAWTOOTH_CODE)
            {
                oscillator.WaveType = SignalGeneratorType.SawTooth;
            }
            else if (waveType == TRIANGLE_CODE)
            {
                oscillator.WaveType = SignalGeneratorType.Triangle;
            }
            else if (waveType == NOISE_CODE)
            {
                oscillator.WaveType = SignalGeneratorType.White;
            }
        }

        private void ManageButtonActivation(List<Button> buttons, Button sender)
        {
            foreach (Button button in buttons)
            {
                if(button != sender)
                {
                    button.Enabled = true;
                }
            }
        }
    }
}
