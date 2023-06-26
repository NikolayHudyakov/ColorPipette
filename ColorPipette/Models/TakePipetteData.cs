using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorPipette.Models
{
    internal class TakePipetteData
    {
        public int ChannelR { get; set; }
        public int ChannelG { get; set; }
        public int ChannelB { get; set; }
        public bool TakePipetteFlag { get; set; }
        public Brush CurrentColor { get; set; } = Brushes.Aqua;
        public double CurrentTop { get; set; }
        public double CurrentLeft { get; set; }
        public string ColorHTML { get; set; } = "#000000";
        public string ColorHEX { get; set; } = "00, 00, 00";
        public string ColorDEC { get; set; } = "0, 0, 0";
    }
}
