using System.Windows;
using System.Windows.Input;
using ColorPipette.Commands;
using System.Windows.Media;
using ColorPipette.Models;
using ColorPipette.Services;

namespace ColorPipette.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly ITakePipette _takePipette;

        #region Field
        private int _channelR;
        private int _channelG;
        private int _channelB;
        private bool _takePipetteFlag;
        private Brush _currentColor = Brushes.Aqua;
        private double _curentTop;
        private double _curentLeft;
        private string _colorHTML = "#000000";
        private string _colorHEX = "00, 00, 00";
        private string _colorDEC = "0, 0, 0";
        #endregion

        public MainWindowViewModel(ITakePipette takePipette)
        {
            _takePipette = takePipette;
            _takePipette.UpdateData += TakePipetteUpdateData;
        }

        #region Properties
        public int ChannelR
        {
            get => _channelR;
            private set => Set(ref _channelR, value);
        }
        public int ChannelG
        {
            get => _channelG;
            private set => Set(ref _channelG, value);
        }
        public int ChannelB
        {
            get => _channelB;
            private set => Set(ref _channelB, value);
        }
        public bool TakePipetteFlag
        {
            get => _takePipetteFlag;
            private set => Set(ref _takePipetteFlag, value);
        }
        public Brush CurrentColor
        {
            get => _currentColor;
            private set => Set(ref _currentColor, value);
        }
        public double CurrentTop
        {
            get => _curentTop;
            private set => Set(ref _curentTop, value);
        }
        public double CurrentLeft
        {
            get => _curentLeft;
            private set => Set(ref _curentLeft, value);
        }
        public string ColorHTML
        {
            get => _colorHTML;
            private set => Set(ref _colorHTML, value);
        }
        public string ColorHEX
        {
            get => _colorHEX;
            private set => Set(ref _colorHEX, value);
        }
        public string ColorDEC
        {
            get => _colorDEC;
            private set => Set(ref _colorDEC, value);
        }
        #endregion

        #region Commands
        public ICommand StartTakePipetteCommand => new RelayCommand(_takePipette.StartTakePipette);
        public ICommand StopTakePipetteCommand => new RelayCommand(_takePipette.StopTakePipette);
        public ICommand ClipBoardHTMLCommand => new RelayCommand(() => Clipboard.SetData(DataFormats.Text, ColorHTML));
        public ICommand ClipBoardHEXCommand => new RelayCommand(() => Clipboard.SetData(DataFormats.Text, ColorHEX));
        public ICommand ClipBoardDECCommand => new RelayCommand(() => Clipboard.SetData(DataFormats.Text, ColorDEC));
        #endregion

        private void TakePipetteUpdateData(TakePipetteData takePipetteData)
        {
            CurrentTop = takePipetteData.CurrentTop;
            CurrentLeft = takePipetteData.CurrentLeft;
            ChannelR = takePipetteData.ChannelR;
            ChannelG = takePipetteData.ChannelG;
            ChannelB = takePipetteData.ChannelB;
            ColorHTML = takePipetteData.ColorHTML;
            ColorHEX = takePipetteData.ColorHEX;
            ColorDEC = takePipetteData.ColorDEC;
            CurrentColor = takePipetteData.CurrentColor;
            TakePipetteFlag = takePipetteData.TakePipetteFlag;
        }
    }
}
