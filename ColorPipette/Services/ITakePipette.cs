using ColorPipette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorPipette.Services
{
    internal interface ITakePipette
    {
        
        public event TakePipetteUpdateEventHandler? UpdateData;
        public void StartTakePipette();
        public void StopTakePipette();
    }

    internal delegate void TakePipetteUpdateEventHandler(TakePipetteData takePipetteData);
}
