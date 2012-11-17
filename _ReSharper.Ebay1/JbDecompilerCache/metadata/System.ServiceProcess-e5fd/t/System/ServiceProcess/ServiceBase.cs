// Type: System.ServiceProcess.ServiceBase
// Assembly: System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.ServiceProcess.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.ServiceProcess
{
    /// <summary>
    /// Provides a base class for a service that will exist as part of a service application. <see cref="T:System.ServiceProcess.ServiceBase"/> must be derived from when creating a new service class.
    /// </summary>
    [InstallerType(typeof (ServiceProcessInstaller))]
    public class ServiceBase : Component
    {
        /// <summary>
        /// Indicates the maximum size for a service name.
        /// </summary>
        public const int MaxNameLength = 80;

        /// <summary>
        /// Requests additional time for a pending operation.
        /// </summary>
        /// <param name="milliseconds">The requested time in milliseconds.</param><exception cref="T:System.InvalidOperationException">The service is not in a pending state.</exception>
        [ComVisible(false)]
        public void RequestAdditionalTime(int milliseconds);

        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.ServiceProcess.ServiceBase"/>.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing);

        /// <summary>
        /// When implemented in a derived class, <see cref="M:System.ServiceProcess.ServiceBase.OnContinue"/> runs when a Continue command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service resumes normal functioning after being paused.
        /// </summary>
        protected virtual void OnContinue();

        /// <summary>
        /// When implemented in a derived class, executes when a Pause command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service pauses.
        /// </summary>
        protected virtual void OnPause();

        /// <summary>
        /// When implemented in a derived class, executes when the computer's power status has changed. This applies to laptop computers when they go into suspended mode, which is not the same as a system shutdown.
        /// </summary>
        /// 
        /// <returns>
        /// When implemented in a derived class, the needs of your application determine what value to return. For example, if a QuerySuspend broadcast status is passed, you could cause your application to reject the query by returning false.
        /// </returns>
        /// <param name="powerStatus">A <see cref="T:System.ServiceProcess.PowerBroadcastStatus"/> that indicates a notification from the system about its power status. </param>
        protected virtual bool OnPowerEvent(PowerBroadcastStatus powerStatus);

        /// <summary>
        /// Executes when a change event is received from a Terminal Server session.
        /// </summary>
        /// <param name="changeDescription">A <see cref="T:System.ServiceProcess.SessionChangeDescription"/> structure that identifies the change type.</param>
        protected virtual void OnSessionChange(SessionChangeDescription changeDescription);

        /// <summary>
        /// When implemented in a derived class, executes when the system is shutting down. Specifies what should occur immediately prior to the system shutting down.
        /// </summary>
        protected virtual void OnShutdown();

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command. </param>
        protected virtual void OnStart(string[] args);

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected virtual void OnStop();

        /// <summary>
        /// When implemented in a derived class, <see cref="M:System.ServiceProcess.ServiceBase.OnCustomCommand(System.Int32)"/> executes when the Service Control Manager (SCM) passes a custom command to the service. Specifies actions to take when a command with the specified parameter value occurs.
        /// </summary>
        /// <param name="command">The command message sent to the service. </param>
        protected virtual void OnCustomCommand(int command);

        /// <summary>
        /// Registers the executable for multiple services with the Service Control Manager (SCM).
        /// </summary>
        /// <param name="services">An array of ServiceBase instances, which indicate services to start. </param><exception cref="T:System.ArgumentException">You did not supply a service to start. The array might be null or empty. </exception><exception cref="T:System.ComponentModel.Win32Exception">You tried to start the service from the command line. </exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlAppDomain"/><IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static void Run(ServiceBase[] services);

        /// <summary>
        /// Registers the executable for a service with the Service Control Manager (SCM).
        /// </summary>
        /// <param name="service">A <see cref="T:System.ServiceProcess.ServiceBase"/> which indicates a service to start. </param><exception cref="T:System.ArgumentException"><paramref name="service"/> is null.</exception><exception cref="T:System.ComponentModel.Win32Exception">You tried to start the service from the command line. </exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlAppDomain"/><IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static void Run(ServiceBase service);

        /// <summary>
        /// Stops the executing service.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void Stop();

        /// <summary>
        /// Registers the command handler and starts the service.
        /// </summary>
        /// <param name="argCount">The number of arguments in the argument array. </param><param name="argPointer">An <see cref="T:System.IntPtr"/> structure that points to an array of arguments.</param><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/><IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComVisible(false)]
        public void ServiceMainCallback(int argCount, IntPtr argPointer);

        /// <summary>
        /// Indicates whether to report Start, Stop, Pause, and Continue commands in the event log.
        /// </summary>
        /// 
        /// <returns>
        /// true to report information in the event log; otherwise, false.
        /// </returns>
        [ServiceProcessDescription("SBAutoLog")]
        [DefaultValue(true)]
        public bool AutoLog { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        set; }

        /// <summary>
        /// Gets or sets the exit code for the service.
        /// </summary>
        /// 
        /// <returns>
        /// The exit code for the service.
        /// </returns>
        [ComVisible(false)]
        public int ExitCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service can handle notifications of computer power status changes.
        /// </summary>
        /// 
        /// <returns>
        /// true if the service handles the computer power status changes indicated in the <see cref="T:System.ServiceProcess.PowerBroadcastStatus"/> class, otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">This property is modified after the service was started. </exception>
        [DefaultValue(false)]
        public bool CanHandlePowerEvent { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the service can handle session change events received from a Terminal Server session.
        /// </summary>
        /// 
        /// <returns>
        /// true if the service can handle Terminal Server session change events; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">This property is modified after the service was started.</exception>
        [ComVisible(false)]
        [DefaultValue(false)]
        public bool CanHandleSessionChangeEvent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service can be paused and resumed.
        /// </summary>
        /// 
        /// <returns>
        /// true if the service can be paused; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The service has already been started. The <see cref="P:System.ServiceProcess.ServiceBase.CanPauseAndContinue"/> property cannot be changed once the service has started. </exception>
        [DefaultValue(false)]
        public bool CanPauseAndContinue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service should be notified when the system is shutting down.
        /// </summary>
        /// 
        /// <returns>
        /// true if the service should be notified when the system is shutting down; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The service has already been started. The <see cref="P:System.ServiceProcess.ServiceBase.CanShutdown"/> property cannot be changed once the service has started. </exception>
        [DefaultValue(false)]
        public bool CanShutdown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service can be stopped once it has started.
        /// </summary>
        /// 
        /// <returns>
        /// true if the service can be stopped and the <see cref="M:System.ServiceProcess.ServiceBase.OnStop"/> method called; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The service has already been started. The <see cref="P:System.ServiceProcess.ServiceBase.CanStop"/> property cannot be changed once the service has started. </exception>
        [DefaultValue(true)]
        public bool CanStop { get; set; }

        /// <summary>
        /// Gets an event log you can use to write notification of service command calls, such as Start and Stop, to the Application event log.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Diagnostics.EventLog"/> instance whose source is registered to the Application log.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/><IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual EventLog EventLog { get; }

        /// <summary>
        /// Gets the service control handle for the service.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.IntPtr"/> structure that contains the service control handle for the service.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected IntPtr ServiceHandle { get; }

        /// <summary>
        /// Gets or sets the short name used to identify the service to the system.
        /// </summary>
        /// 
        /// <returns>
        /// The name of the service.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The service has already been started. The <see cref="P:System.ServiceProcess.ServiceBase.ServiceName"/> property cannot be changed once the service has started. </exception><exception cref="T:System.ArgumentException">The <see cref="P:System.ServiceProcess.ServiceBase.ServiceName"/> property is invalid. </exception>
        [TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [ServiceProcessDescription("SBServiceName")]
        public string ServiceName { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; set; }
    }
}
