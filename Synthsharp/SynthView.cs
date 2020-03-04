/**
 * @file SynthView.cs
 * @author Gawen Ackermann (gawen.ackrm@eduge.ch), Jonathan Borel-Jaquet (jonathon.brljq@eduge.ch), Fabian Huber (fabian.hbr@eduge.ch)
 * @brief File of the SynthView class.
 * @version 1.0
 * @date 04.03.2020
 * 
 * @copyright CFPT (c) 2020
 * 
 */
using NAudio.Midi;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Synthsharp
{
    /// <summary>
    /// View of the Synthsharp project.
    /// </summary>
    public partial class SynthView : Form
    {
        private const int OSCILLATOR_1 = 1;
        private const int OSCILLATOR_2 = 2;
        private const int OSCILLATOR_3 = 3;

        private const char SINE_CODE = 'S';
        private const char SQUARE_CODE = 'Q';
        private const char SAWTOOTH_CODE = 'A';
        private const char WHITE_CODE = 'N';
        private const char TRIANGLE_CODE = 'T';

        private const string NO_DEVICE_DETECTED_MESSAGE = "Aucun dispositif trouvé";

        private const int DEFAULT_MIDI_DEVICE_ID = 0;

        private Oscillator o1;
        private Oscillator o2;
        private Oscillator o3;

        private readonly List<Button> Oscillator1Buttons;
        private readonly List<Button> Oscillator2Buttons;
        private readonly List<Button> Oscillator3Buttons;

        private MIDIPlayer midiPlayer;

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

            //If there is a midi device, we create a new midi player
            if (MidiIn.NumberOfDevices > 0)
            {
                midiPlayer = new MIDIPlayer(DEFAULT_MIDI_DEVICE_ID, o1, o2, o3);
            }
        }

        /// <summary>
        /// Initialize the combobox values
        /// </summary>
        /// <param name="cbs">Array of ComboBox</param>
        /// <param name="dataCbx">Elements of the ComboBox</param>
        public static void InitializeComboBox(ComboBox[] cbs, string[] dataCbx)
        {
            foreach (ComboBox combobox in cbs)
            {
                combobox.Items.AddRange(dataCbx);
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
                SignalGeneratorType signalGeneratorType = GetSignalGeneratorType(tag[1]);

                if (oscillatorId == OSCILLATOR_1)
                {
                    ChangeWaveType(ref o1, Oscillator1Buttons, signalGeneratorType);
                }
                else if (oscillatorId == OSCILLATOR_2)
                {
                    ChangeWaveType(ref o2, Oscillator2Buttons, signalGeneratorType);
                }
                else if (oscillatorId == OSCILLATOR_3)
                {
                    ChangeWaveType(ref o3, Oscillator3Buttons, signalGeneratorType);
                }
            }
        }

        private void ChangeWaveType(ref Oscillator oscillator, List<Button> buttons, SignalGeneratorType signalGeneratorType)
        {
            oscillator.WaveType = signalGeneratorType;
            ManageButtonsActivation(buttons, oscillator);
        }

        private SignalGeneratorType GetSignalGeneratorType(char waveCode)
        {
            SignalGeneratorType signalGeneratorType = SignalGeneratorType.Sin;
            if (waveCode == SQUARE_CODE)
            {
                signalGeneratorType = SignalGeneratorType.Square;
            }
            else if (waveCode == SAWTOOTH_CODE)
            {
                signalGeneratorType = SignalGeneratorType.SawTooth;
            }
            else if (waveCode == TRIANGLE_CODE)
            {
                signalGeneratorType = SignalGeneratorType.Triangle;
            }
            else if (waveCode == WHITE_CODE)
            {
                signalGeneratorType = SignalGeneratorType.White;
            }

            return signalGeneratorType;
        }

        private void ManageButtonsActivation(List<Button> buttons, Oscillator oscillator)
        {
            foreach (Button button in buttons)
            {
                //The tag should look like this: 1S  ->  Oscillator 1, sine waveform
                string tag = button.Tag.ToString();
                //The second char of the tag is the type of waveform
                SignalGeneratorType signalGeneratorType = GetSignalGeneratorType(tag[1]);

                button.Enabled = signalGeneratorType != oscillator.WaveType;
            }
        }

        private void ManageEnabledActivation(List<Button> buttons, ref TrackBar trb, ref Oscillator oscillator, bool enabled)
        {
            oscillator.IsEnabled = enabled;
            foreach (Button btn in buttons)
            {
                btn.Enabled = enabled;
            }
            trb.Enabled = enabled;

            if (enabled)
            {
                ManageButtonsActivation(buttons, oscillator);
            }
        }

        private void ChkOnOscillator_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is CheckBox))
                return;

            CheckBox checkBox = sender as CheckBox;
            int tag = int.Parse(checkBox.Tag.ToString());
            if (tag == OSCILLATOR_1)
            {
                ManageEnabledActivation(Oscillator1Buttons, ref trbOscillator1, ref o1, checkBox.Checked);
            }
            else if (tag == OSCILLATOR_2)
            {
                ManageEnabledActivation(Oscillator2Buttons, ref trbOscillator2, ref o2, checkBox.Checked);
            }
            else if (tag == OSCILLATOR_3)
            {
                ManageEnabledActivation(Oscillator3Buttons, ref trbOscillator3, ref o3, checkBox.Checked);
            }
        }

        private void SynthView_Load(object sender, EventArgs e)
        {
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                cbxDevice.Items.Add(MidiIn.DeviceInfo(device).ProductName);
            }
            if (cbxDevice.Items.Count == 0)
            {
                cbxDevice.Items.Add(NO_DEVICE_DETECTED_MESSAGE);
            }
            cbxDevice.SelectedIndex = 0;

            if (midiPlayer != null)
            {
                midiPlayer.Start();
            }

            ManageButtonsActivation(Oscillator1Buttons, o1);
            ManageButtonsActivation(Oscillator2Buttons, o2);
            ManageButtonsActivation(Oscillator3Buttons, o3);
        }

        private void CbxDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If there is a midi device, we create a new midi player
            if (MidiIn.NumberOfDevices > 0)
            {
                if (cbxDevice.SelectedItem.ToString() != NO_DEVICE_DETECTED_MESSAGE)
                {
                    //The midi player must be diposed before being reconstructed to release the midi device
                    if (midiPlayer != null)
                    {
                        midiPlayer.Dispose();
                    }
                    midiPlayer = new MIDIPlayer(cbxDevice.SelectedIndex, o1, o2, o3);
                }
            }
        }

        private void TrbOscillator1_Scroll(object sender, EventArgs e)
        {
            o1.Gain = (double)trbOscillator1.Value / trbOscillator1.Maximum;
        }

        private void TrbOscillator2_Scroll(object sender, EventArgs e)
        {
            o1.Gain = (double)trbOscillator2.Value / trbOscillator2.Maximum;
        }

        private void TrbOscillator3_Scroll(object sender, EventArgs e)
        {
            o1.Gain = (double)trbOscillator3.Value / trbOscillator3.Maximum;
        }
    }
}