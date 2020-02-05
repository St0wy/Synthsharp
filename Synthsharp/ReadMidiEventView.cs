using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Midi;

namespace Synthsharp
{
    public partial class ReadMidiEventView : Form
    {
        public string NO_DEVICE_DETECTED_MESSAGE = "Aucun dispositif trouvé"; 
        public ReadMidiEventView()
        {
            InitializeComponent();
        }

        private void ReadMidiEventView_Load(object sender, EventArgs e)
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
        }

        private void btnSelectDevice_Click(object sender, EventArgs e)
        {
            if (cbxDevice.SelectedItem.ToString() != NO_DEVICE_DETECTED_MESSAGE)
            {
                MidiIn m = new MidiIn(cbxDevice.SelectedIndex);
                m.MessageReceived += midiIn_MessageReceived;
                m.ErrorReceived += midiIn_ErrorReceived;
                m.Start();
                btnSelectDevice.Enabled = false;
            }
        }

        private void midiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            string message = "ERROR";
            Invoke((MethodInvoker)delegate { lbxMessage.Items.Add(message); });
            Invoke((MethodInvoker)delegate { lbxMessage.SelectedIndex = lbxMessage.Items.Count - 1; });

        }

        private void midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            string message = string.Empty;
            if (e.MidiEvent is NoteEvent)
            {

                message = string.Format(" Time : {0} NoteName : {1} N° Key {2} ",
                e.Timestamp, (e.MidiEvent as NoteEvent).NoteName, (e.MidiEvent as NoteEvent).NoteNumber);
                
                if ((e.MidiEvent is NoteEvent) && (e.MidiEvent as NoteEvent).NoteName.Substring(0, 1) == "C")
                {
                    message = "Note C";
                }

                
            }
            else if(e.MidiEvent is ControlChangeEvent)
            {
                message = string.Format(" Time : {0} ControllerNumber : {1} ControllerValue {2} ",
                e.Timestamp, (e.MidiEvent as ControlChangeEvent).Controller, (e.MidiEvent as ControlChangeEvent).ControllerValue);

            }
            Invoke((MethodInvoker)delegate { lbxMessage.Items.Add(message); });
            Invoke((MethodInvoker)delegate { lbxMessage.SelectedIndex = lbxMessage.Items.Count - 1; });
        }

    }
}
