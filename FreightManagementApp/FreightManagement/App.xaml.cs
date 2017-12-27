using NLog;
using System.Windows;

namespace FreightManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            _logger.Info("App started!");
        }

        private static Logger _logger = LogManager.GetCurrentClassLogger();
    }
}