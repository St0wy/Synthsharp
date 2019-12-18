using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Sanford.Multimedia;
using Sanford.Multimedia.Midi;
using System.Threading;
using System.Runtime.InteropServices;

namespace Synthsharp
{
    public class MidiHandler
    {
        private InputDevice inputDevice;
        private SynchronizationContext synchronizationContext;
        private OutputStream outputStream;

        public MidiHandler(int deviceId)
        {
            inputDevice = new InputDevice(deviceId);
            inputDevice.ChannelMessageReceived += HandleChannelMessageReceived;
            inputDevice.Error += new EventHandler<ErrorEventArgs>(HandleInputDeviceError);
            inputDevice.StartRecording();
            synchronizationContext = SynchronizationContext.Current;
            outputStream = new OutputStream(0);
            outputStream.StartPlaying();
            GCHandle inputHandle = GCHandle.Alloc(inputDevice);
            GCHandle outputHandle = GCHandle.Alloc(outputStream);
            MidiDevice.Connect(GCHandle.ToIntPtr(inputHandle), GCHandle.ToIntPtr(outputHandle));
        }

        private void HandleChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            //inputDevice.
            //outputStream.Write(new MidiEvent())
            //MidiEvent
            synchronizationContext.Post(delegate (object dummy)
            {
                Trace.TraceInformation(
                e.Message.Command.ToString() + '\t' + '\t' +
                e.Message.MidiChannel.ToString() + '\t' +
                e.Message.Data1.ToString() + '\t' +
                e.Message.Data2.ToString());
            }, null);
            
        }

        private void HandleInputDeviceError(object sender, ErrorEventArgs e)
        {
            Trace.TraceError(e.Error.Message);
        }

        ~MidiHandler()
        {
            inputDevice.Close();
            inputDevice = null;
        }


    }
}
