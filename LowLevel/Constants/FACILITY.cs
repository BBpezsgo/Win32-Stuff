namespace Win32.LowLevel
{
    public static class Facility
    {
        /// <summary>
        /// The default facility code.
        /// </summary>
        /// <remarks>
        /// For broadly applicable common status codes such as <see cref="HResult.S_OK"/>.
        /// </remarks>
        public const int NULL = 0;
        /// <summary>
        /// The source of the error code is an RPC subsystem.
        /// </summary>
        /// <remarks>
        /// For status codes returned from remote procedure calls.
        /// </remarks>
        public const int RPC = 1;
        /// <summary>
        /// The source of the error code is a COM Dispatch.
        /// </summary>
        /// <remarks>
        /// For late-binding <c>IDispatch</c> interface errors.
        /// </remarks>
        public const int DISPATCH = 2;
        /// <summary>
        /// The source of the error code is OLE Storage.
        /// </summary>
        /// <remarks>
        /// For status codes returned from <c>IStorage</c> or <c>IStream</c> method calls
        /// relating to structured storage. Status codes whose
        /// code (lower 16 bits) value is in the range of MS-DOS error
        /// codes (that is, less than 256) have the same meaning as the
        /// corresponding MS-DOS error.
        /// </remarks>
        public const int STORAGE = 3;
        /// <summary>
        /// The source of the error code is COM/OLE Interface management.
        /// </summary>
        /// <remarks>
        /// For most status codes returned from interface methods.
        /// The actual meaning of the error is defined by the interface.
        /// That is, two <c>HRESULT</c>s with exactly the same 32-bit value
        /// returned from two different interfaces might have different meanings.
        /// </remarks>
        public const int ITF = 4;
        /// <summary>
        /// This region is reserved to map undecorated error codes into <c>HRESULT</c>s.
        /// </summary>
        /// <remarks>
        /// Used to provide a means of handling error codes from functions
        /// in the Windows API as an <c>HRESULT</c>. Error codes in 16-bit OLE
        /// that duplicated system error codes have also been
        /// changed to <see cref="WIN32"/>.
        /// </remarks>
        public const int WIN32 = 7;
        /// <summary>
        /// The source of the error code is the Windows subsystem.
        /// </summary>
        /// <remarks>
        /// Used for additional error codes from Microsoft-defined interfaces.
        /// </remarks>
        public const int WINDOWS = 8;
        /// <summary>
        /// The source of the error code is the Security API layer.
        /// </summary>
        public const int SECURITY = 9;
        /// <summary>
        /// The source of the error code is the Security API layer.
        /// </summary>
        public const int SSPI = 9;
        /// <summary>
        /// The source of the error code is the control mechanism.
        /// </summary>
        public const int CONTROL = 10;
        /// <summary>
        /// The source of the error code is a certificate client or server?
        /// </summary>
        public const int CERT = 11;
        /// <summary>
        /// The source of the error code is Wininet related.
        /// </summary>
        public const int INTERNET = 12;
        /// <summary>
        /// The source of the error code is the Windows Media Server.
        /// </summary>
        public const int MEDIASERVER = 13;
        /// <summary>
        /// The source of the error code is the Microsoft Message Queue.
        /// </summary>
        public const int MSMQ = 14;
        /// <summary>
        /// The source of the error code is the Setup API.
        /// </summary>
        public const int SETUPAPI = 15;
        /// <summary>
        /// The source of the error code is the Smart-card subsystem.
        /// </summary>
        public const int SCARD = 16;
        /// <summary>
        /// The source of the error code is COM+.
        /// </summary>
        public const int COMPLUS = 17;
        /// <summary>
        /// The source of the error code is the Microsoft agent.
        /// </summary>
        public const int AAF = 18;
        /// <summary>
        /// The source of the error code is .NET CLR.
        /// </summary>
        public const int URT = 19;
        /// <summary>
        /// The source of the error code is the audit collection service.
        /// </summary>
        public const int ACS = 20;
        /// <summary>
        /// The source of the error code is Direct Play.
        /// </summary>
        public const int DPLAY = 21;
        /// <summary>
        /// The source of the error code is the ubiquitous memoryintrospection service.
        /// </summary>
        public const int UMI = 22;
        /// <summary>
        /// The source of the error code is Side-by-side servicing.
        /// </summary>
        public const int SXS = 23;
        /// <summary>
        /// The error code is specific to Windows CE.
        /// </summary>
        public const int WINDOWS_CE = 24;
        /// <summary>
        /// The source of the error code is HTTP support.
        /// </summary>
        public const int HTTP = 25;
        /// <summary>
        /// The source of the error code is common Logging support.
        /// </summary>
        public const int USERMODE_COMMONLOG = 26;
        /// <summary>
        /// The source of the error code is the user mode filter manager.
        /// </summary>
        public const int USERMODE_FILTER_MANAGER = 31;
        /// <summary>
        /// The source of the error code is background copy control
        /// </summary>
        public const int BACKGROUNDCOPY = 32;
        /// <summary>
        /// The source of the error code is configuration services.
        /// </summary>
        public const int CONFIGURATION = 33;
        /// <summary>
        /// The source of the error code is state management services.
        /// </summary>
        public const int STATE_MANAGEMENT = 34;
        /// <summary>
        /// The source of the error code is the Microsoft Identity Server.
        /// </summary>
        public const int METADIRECTORY = 35;
        /// <summary>
        /// The source of the error code is a Windows update.
        /// </summary>
        public const int WINDOWSUPDATE = 36;
        /// <summary>
        /// The source of the error code is Active Directory.
        /// </summary>
        public const int DIRECTORYSERVICE = 37;
        /// <summary>
        /// The source of the error code is the graphics drivers.
        /// </summary>
        public const int GRAPHICS = 38;
        /// <summary>
        /// The source of the error code is the user Shell.
        /// </summary>
        public const int SHELL = 39;
        /// <summary>
        /// The source of the error code is the Trusted Platform Module services.
        /// </summary>
        public const int TPM_SERVICES = 40;
        /// <summary>
        /// The source of the error code is the Trusted Platform Module applications.
        /// </summary>
        public const int TPM_SOFTWARE = 41;
        /// <summary>
        /// The source of the error code is Performance Logs and Alerts
        /// </summary>
        public const int PLA = 48;
        /// <summary>
        /// The source of the error code is Full volume encryption.
        /// </summary>
        public const int FVE = 49;
        /// <summary>
        /// The source of the error code is the Firewall Platform.
        /// </summary>
        public const int FWP = 50;
        /// <summary>
        /// The source of the error code is the Windows Resource Manager.
        /// </summary>
        public const int WINRM = 51;
        /// <summary>
        /// The source of the error code is the Network Driver Interface.
        /// </summary>
        public const int NDIS = 52;
        /// <summary>
        /// The source of the error code is the Usermode Hypervisor components.
        /// </summary>
        public const int USERMODE_HYPERVISOR = 53;
        /// <summary>
        /// The source of the error code is the Configuration Management Infrastructure.
        /// </summary>
        public const int CMI = 54;
        /// <summary>
        /// The source of the error code is the user mode virtualization subsystem.
        /// </summary>
        public const int USERMODE_VIRTUALIZATION = 55;
        /// <summary>
        /// The source of the error code is  the user mode volume manager
        /// </summary>
        public const int USERMODE_VOLMGR = 56;
        /// <summary>
        /// The source of the error code is the Boot Configuration Database.
        /// </summary>
        public const int BCD = 57;
        /// <summary>
        /// The source of the error code is user mode virtual hard disk support.
        /// </summary>
        public const int USERMODE_VHD = 58;
        /// <summary>
        /// The source of the error code is System Diagnostics.
        /// </summary>
        public const int SDIAG = 60;
        /// <summary>
        /// The source of the error code is the Web Services.
        /// </summary>
        public const int WEBSERVICES = 61;
        /// <summary>
        /// The source of the error code is a Windows Defender component.
        /// </summary>
        public const int WINDOWS_DEFENDER = 80;
        /// <summary>
        /// The source of the error code is the open connectivity service.
        /// </summary>
        public const int OPC = 81;

        public static string? ToString(int code) => code switch
        {
            Facility.RPC => "an RPC subsystem",
            Facility.DISPATCH => "a COM Dispatch",
            Facility.STORAGE => "OLE Storage",
            Facility.ITF => "COM/OLE Interface management",
            Facility.WINDOWS => "Windows subsystem",
            Facility.SECURITY => "Security API layer",
            Facility.CONTROL => "control mechanism",
            Facility.CERT => "a certificate client or server",
            Facility.INTERNET => "Wininet related",
            Facility.MEDIASERVER => "Windows Media Server",
            Facility.MSMQ => "Microsoft Message Queue",
            Facility.SETUPAPI => "Setup API",
            Facility.SCARD => "Smart-card subsystem",
            Facility.COMPLUS => "COM+",
            Facility.AAF => "Microsoft agent",
            Facility.URT => ".NET CLR",
            Facility.ACS => "audit collection service",
            Facility.DPLAY => "Direct Play",
            Facility.UMI => "ubiquitous memoryintrospection service",
            Facility.SXS => "Side-by-side servicing",
            Facility.WINDOWS_CE => "CE",
            Facility.HTTP => "HTTP support",
            Facility.USERMODE_COMMONLOG => "common Logging support",
            Facility.USERMODE_FILTER_MANAGER => "user mode filter manager",
            Facility.BACKGROUNDCOPY => "background copy control",
            Facility.CONFIGURATION => "configuration services",
            Facility.STATE_MANAGEMENT => "state management services",
            Facility.METADIRECTORY => "Microsoft Identity Server",
            Facility.WINDOWSUPDATE => "Windows update",
            Facility.DIRECTORYSERVICE => "Active Directory",
            Facility.GRAPHICS => "graphics drivers",
            Facility.SHELL => "user Shell",
            Facility.TPM_SERVICES => "Trusted Platform Module services",
            Facility.TPM_SOFTWARE => "Trusted Platform Module applications",
            Facility.PLA => "Performance Logs and Alert",
            Facility.FVE => "Full volume encryption",
            Facility.FWP => "Firewall Platform",
            Facility.WINRM => "Windows Resource Manager",
            Facility.NDIS => "Network Driver Interface",
            Facility.USERMODE_HYPERVISOR => "Usermode Hypervisor components",
            Facility.CMI => "Configuration Management Infrastructure",
            Facility.USERMODE_VIRTUALIZATION => "user mode virtualization subsystem",
            Facility.USERMODE_VOLMGR => "user mode volume manage",
            Facility.BCD => "Boot Configuration Database",
            Facility.USERMODE_VHD => "mode virtual hard disk support",
            Facility.SDIAG => "System Diagnostics",
            Facility.WEBSERVICES => "Web Services",
            Facility.WINDOWS_DEFENDER => "a Windows Defender component",
            Facility.OPC => "open connectivity service",
            _ => null,
        };
    }
}
