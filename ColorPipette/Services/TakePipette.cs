using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;
using System.Windows.Media;
using System.Threading.Channels;
using System.Windows;
using ColorPipette.Models;

namespace ColorPipette.Services
{
    internal class TakePipette : ITakePipette
    {
        private const int OcrNormal = 32512;
        private const string CursorsPath = @"Resources\Cursors\cursor.cur";

        private Point _point;
        private IntPtr _normalCursor;
        private IntPtr _pipetteCursor;
        private IntPtr _copyNormalCursor;
        private IntPtr _copyPipetteCursor;
        private TakePipetteData _takePipetteData;

        public TakePipette() 
        {
            _takePipetteData = new TakePipetteData();
            _normalCursor = LoadCursor(IntPtr.Zero, OcrNormal);
            _pipetteCursor = LoadCursorFromFile(CursorsPath);
            CopyCursors();
        }

        public event TakePipetteUpdateEventHandler? UpdateData;

        public void StartTakePipette()
        {
            _takePipetteData.TakePipetteFlag = true;

            SetSystemCursor(_copyPipetteCursor, OcrNormal);

            Task.Run(() =>
            {
                while (_takePipetteData.TakePipetteFlag)
                {
                    GetCursorPos(out _point);
                    _takePipetteData.CurrentTop = _point.Y;
                    _takePipetteData.CurrentLeft = _point.X;

                    IntPtr hDC = GetDC(IntPtr.Zero);
                    uint pixel = GetPixel(hDC, _point.X, _point.Y);
                    ReleaseDC(IntPtr.Zero, hDC);

                    var decR = (byte)(pixel & 0x0000FF);
                    var decG = (byte)((pixel & 0x00FF00) >> 8);
                    var decB = (byte)((pixel & 0xFF0000) >> 16);

                    _takePipetteData.ChannelR = decR;
                    _takePipetteData.ChannelG = decG;
                    _takePipetteData.ChannelB = decB;

                    var hexR = BitConverter.ToString(new byte[1] { decR });
                    var hexG = BitConverter.ToString(new byte[1] { decG });
                    var hexB = BitConverter.ToString(new byte[1] { decB });

                    _takePipetteData.ColorHTML = $"#{hexR}{hexG}{hexB}";
                    _takePipetteData.ColorHEX = $"{hexR}, {hexG}, {hexB}";
                    _takePipetteData.ColorDEC = $"{decR}, {decG}, {decB}";

                    var currentColor = Color.FromRgb(decR, decG, decB);

                    var solidColorBrush = new SolidColorBrush(currentColor);
                    solidColorBrush.Freeze();
                    _takePipetteData.CurrentColor = solidColorBrush;

                    UpdateData?.Invoke(_takePipetteData);
                }
            });
        }

        public void StopTakePipette()
        {
            _takePipetteData.TakePipetteFlag = false;
            SetSystemCursor(_copyNormalCursor, OcrNormal);
            CopyCursors();
            Clipboard.SetData(DataFormats.Text, _takePipetteData.ColorHTML);

            UpdateData?.Invoke(_takePipetteData);
        }

        private void CopyCursors()
        {
            _copyNormalCursor = CopyIcon(_normalCursor);
            _copyPipetteCursor = CopyIcon(_pipetteCursor);
        }

        #region ExternMetods
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern uint GetPixel(IntPtr hDC, int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point point);

        [DllImport("user32.dll")]
        static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        private static extern IntPtr CopyIcon(IntPtr pcur);
        #endregion
    }
}
