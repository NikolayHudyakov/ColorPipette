using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ColorPipette.Commands
{
    internal class CloseWindowCommand :Command
    {
        public override void Execute(object? parameter) => (parameter as Window)?.Close();

        public override bool CanExecute(object? parameter) => parameter is Window;
    }
}
