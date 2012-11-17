using System.ServiceProcess;

namespace Ebay1Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] servicesToRun = new ServiceBase[] 
                                              { 
                                                  new EBayService() 
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}
