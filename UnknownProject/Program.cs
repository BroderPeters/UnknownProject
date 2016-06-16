using log4net;
using log4net.Config;
using System;
using UnknownProject.DependencyInjection;

namespace UnknownProject
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += LogUnhandledException;
            Log.Debug("Initialise DependencyInjection");
            var container = DependencyInjectionContainer.Get();
            using (container.BeginScope())
            {
                var game = container.GetInstance<UnknownProjectGame>();
                Log.Debug("Starting UnknownProjectGame");
                game.Run();
            }
        }

        private static void LogUnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal((Exception)e.ExceptionObject);
        }
    }
}
