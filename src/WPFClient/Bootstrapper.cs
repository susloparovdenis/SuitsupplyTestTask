using System.Windows;

using Caliburn.Micro;

using NLog;
using NLog.Config;
using NLog.Targets;

using SuitsupplyTestTask.WPFClient.MainWindow;

using LogManager = NLog.LogManager;

namespace SuitsupplyTestTask.WPFClient
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
            SetupLogging();
        }

        private void SetupLogging()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget { FileName = "${basedir}/log.txt" };
            config.AddTarget("file", fileTarget);

            var rule1 = new LoggingRule("*", LogLevel.Trace, fileTarget);
            config.LoggingRules.Add(rule1);
            LogManager.Configuration = config;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}