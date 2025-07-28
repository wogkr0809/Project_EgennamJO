using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EgennamJO.Core
{
    internal class InspStage : IDisposable
    {
        SAIGEAI _saigeAI;

        public InspStage() { }

        public SAIGEAI AIModule
        {
            get
            {
                if (_saigeAI is null)
                    _saigeAI = new SAIGEAI();
                return _saigeAI;
            }
        }

        public bool Initialize()
        {
            return true;
        }
        public void UpdateDisplay(Bitmap bitmap)
        {

            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDisplay(bitmap);
            }
        }
        public Bitmap GetCurrentImage()
        {
            Bitmap bitmap = null;
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                bitmap = cameraForm.GetDisplayImage();
            }
            return bitmap;
        }
        private bool disposed = false; 
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}