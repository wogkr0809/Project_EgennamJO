using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EgennamJO.Core
{
    public class InspStage : IDisposable
    {
        SAIGEAI _saigeAI;
        
        SAIGEAI _saigeAI; // SaigeAI 인스턴스

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

      
        #region Disposable

        private bool disposed = false; // to detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Dispose unmanaged managed resources.

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

