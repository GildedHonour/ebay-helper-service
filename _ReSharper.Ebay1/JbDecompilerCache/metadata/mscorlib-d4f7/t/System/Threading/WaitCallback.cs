// Type: System.Threading.WaitCallback
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading
{
    /// <summary>
    /// Represents a callback method to be executed by a thread pool thread.
    /// </summary>
    /// <param name="state">An object containing information to be used by the callback method. </param><filterpriority>2</filterpriority>
    [ComVisible(true)]
    public delegate void WaitCallback(object state);
}
