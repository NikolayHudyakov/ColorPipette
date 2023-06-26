using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ColorPipette
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (Process.GetCurrentProcess().ProcessName != "ColorPipette" | Process.GetProcessesByName("ColorPipette").Length != 1)
                return;

            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(App.CurrentDirectory)
                .ConfigureAppConfiguration((host, cfg) => cfg
                    .SetBasePath(App.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
                .ConfigureServices(App.ConfigureServices);
    }
}
