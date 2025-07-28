using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EgennamJO.Core
{
    internal class Global : IDisposable
    {
        private static readonly Lazy<Global> _instance = new Lazy<Global>(() => new Global());

        public static Global inst
        {
            get
            {
                return _instance.Value;
            }
        }

        private InspStage _stage = new InspStage();

        public InspStage inspStage
        {
            get { return _stage; }
        
        }
        public Global()
        {

        }
        public void Initialize()
        {
           _stage.Initialize();
        }
        public void Dispose()
        {
            _stage.Dispose();
        }

    }
}
