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
        private readonly string[] DEFAULT_COMBO_BOX = { "Sine", "Square", "Saw", "Triangle", "Noise" };

        public SynthView()
        {
            InitializeComponent();
        }


        private void SynthView_Load(object sender, EventArgs e)
        {
            InitializeComboBox(new ComboBox[] { cbxOscillator1, cbxOscillator2, cbxOscillator3 }, DEFAULT_COMBO_BOX);
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

        private void SynthView_KeyDown(object sender, KeyEventArgs e)
        {
            // this.KeyPreview = true; /* Very important to prevent focus on controls */
            int frequency = DEFAULT_FREQUENCY;

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
            //  Oscillator o = new Oscillator(1, frequency, );
            //  o.CreateSineWave(1, frequency);
        }
    }
}
