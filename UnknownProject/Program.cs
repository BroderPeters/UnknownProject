using System;
using UnknownProject.DependencyInjection;

namespace UnknownProject
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = DependencyInjectionContainer.Get();
            using (container.BeginScope())
            {
                var game = container.GetInstance<UnknownProjectGame>();
                game.Run();
            }
        }
    }
}
