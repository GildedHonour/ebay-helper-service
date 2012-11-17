using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Ebay1Service
{
    [RunInstaller(true)]
    public class EBayServiceInstaller: Installer
    {
        public EBayServiceInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();
            
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.DisplayName = EBayService.ServicePrettyName;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = EBayService.ServicePrettyName;
            serviceInstaller.Description = "Updates and creates new fixed price items on your eBay account.";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
