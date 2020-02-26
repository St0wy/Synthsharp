﻿using NAudio.Midi;
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

        Oscillator o1;
        Oscillator o2;
        Oscillator o3;

        readonly List<Button> Oscillator1Buttons;
        readonly List<Button> Oscillator2Buttons;
        readonly List<Button> Oscillator3Buttons;

        MIDIPlayer midiPlayer;
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

        private void UpdateView()
        {

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
            ManageButtonActivation(buttons, oscillator);
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

        private void ManageButtonActivation(List<Button> buttons, Oscillator oscillator)
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

        private void ChkOnOscillator_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkBox = sender as CheckBox;
            int tag = int.Parse(checkBox.Tag.ToString());
            if(tag == OSCILLATOR_1)
            {
                //TODO Handle view desactivation
            }
            else if (tag == OSCILLATOR_2)
            {

            }
            else if (tag == OSCILLATOR_3)
            {

            }

            o1.IsEnabled = chkOnOscillator1.Checked;
        }

        private void ChkOnOscillator1_CheckedChanged(object sender, EventArgs e)
        {
            o1.IsEnabled = chkOnOscillator1.Checked;
        }

        private void ChkOnOscillator2_CheckedChanged(object sender, EventArgs e)
        {
            o2.IsEnabled = chkOnOscillator2.Checked;
        }

        private void ChkOnOscillator3_CheckedChanged(object sender, EventArgs e)
        {
            o3.IsEnabled = chkOnOscillator3.Checked;
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

            ManageButtonActivation(Oscillator1Buttons, o1);
            ManageButtonActivation(Oscillator2Buttons, o2);
            ManageButtonActivation(Oscillator3Buttons, o3);
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
    }
}
