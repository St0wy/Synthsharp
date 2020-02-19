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

        private void BtnSelectDevice_Click(object sender, EventArgs e)
        {

            
        }
    }
}
