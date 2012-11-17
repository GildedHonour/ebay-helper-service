// Type: System.ServiceProcess.ServiceAccount
// Assembly: System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ServiceProcess.dll

namespace System.ServiceProcess
{
    /// <summary>
    /// Specifies a service's security context, which defines its logon type.
    /// </summary>
    public enum ServiceAccount
    {
        LocalService,
        NetworkService,
        LocalSystem,
        User,
    }
}
