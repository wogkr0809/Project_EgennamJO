using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvCameraControl;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace Project_EgennamJO.Grab
{
    struct GrabUserBuffer
    {
        private byte[] _imageBuffer;
        private IntPtr _imageBufferPtr;
        private GCHandle _imageHandle;

        public byte[] ImageBuffer
        {
            get
            {
                return _imageBuffer;
            }
            set
            {
                _imageBuffer = value;
            }
        }
        public IntPtr imageBufferPtr
        {
            get
            {
                return _imageBufferPtr;
            }
            set
            {
                _imageBufferPtr = value;
            }
        }
        public GCHandle ImageHandle
        {
            get
            {
                return _imageHandle;
            }
            set
            {
                _imageHandle = value;
            }
        }
    }
    internal class HikRobotCam : IDisposable
    {
        public delegate void GrabEventHandler<T>(object sender, T obj = null) where T : class;
        public event GrabEventHandler<object> GrabCompleted;
        public event GrabEventHandler<object> TransferCompleted;

        protected GrabUserBuffer[] _userImageBuffer = null;

        public int BufferIndeex { get; set; } = 0;
        internal bool HardwareTrigger { get; set; } = false;
        internal bool IncreaseBufferIndex { get; set; } = false;

        private IDevice _device = null; 

        void FrameGrabeEventHandler(object sender, FrameGrabbedEventArgs e)
        {
            Console.WriteLine("Get one frame : Width[{0}], Height [{1}], ImageSize [{2}],FrameNum[{3}]\", e.FrameOut.Image.Width, e.FrameOut.Image.Height, e.FrameOut.Image.ImageSize, e.FrameOut.FrameNum);");

            IFrameOut frameOut = e.FrameOut;

            OnGrabCompleted(BufferIndex);
        }
        
    }
}