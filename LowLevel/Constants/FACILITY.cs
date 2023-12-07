namespace Win32.LowLevel
{
    public static class Facility
    {
        // The default facility code.
        public const int NULL = 0;
        // The source of the error code is an RPC subsystem.
        public const int RPC = 1;
        // The source of the error code is a COM Dispatch.
        public const int DISPATCH = 2;
        // The source of the error code is OLE Storage.
        public const int STORAGE = 3;
        // The source of the error code is COM/OLE Interface management.
        public const int ITF = 4;
        // This region is reserved to map undecorated error codes into HRESULTs.
        public const int WIN32 = 7;
        // The source of the error code is the Windows subsystem.
        public const int WINDOWS = 8;
        // The source of the error code is the Security API layer.
        public const int SECURITY = 9;
        // The source of the error code is the Security API layer.
        public const int SSPI = 9;
        // The source of the error code is the control mechanism.
        public const int CONTROL = 10;
        // The source of the error code is a certificate client or server?
        public const int CERT = 11;
        // The source of the error code is Wininet related.
        public const int INTERNET = 12;
        // The source of the error code is the Windows Media Server.
        public const int MEDIASERVER = 13;
        // The source of the error code is the Microsoft Message Queue.
        public const int MSMQ = 14;
        // The source of the error code is the Setup API.
        public const int SETUPAPI = 15;
        // The source of the error code is the Smart-card subsystem.
        public const int SCARD = 16;
        // The source of the error code is COM+.
        public const int COMPLUS = 17;
        // The source of the error code is the Microsoft agent.
        public const int AAF = 18;
        // The source of the error code is .NET CLR.
        public const int URT = 19;
        // The source of the error code is the audit collection service.
        public const int ACS = 20;
        // The source of the error code is Direct Play.
        public const int DPLAY = 21;
        // The source of the error code is the ubiquitous memoryintrospection service.
        public const int UMI = 22;
        // The source of the error code is Side-by-side servicing.
        public const int SXS = 23;
        // The error code is specific to Windows CE.
        public const int WINDOWS_CE = 24;
        // The source of the error code is HTTP support.
        public const int HTTP = 25;
        // The source of the error code is common Logging support.
        public const int USERMODE_COMMONLOG = 26;
        // The source of the error code is the user mode filter manager.
        public const int USERMODE_FILTER_MANAGER = 31;
        // The source of the error code is background copy control
        public const int BACKGROUNDCOPY = 32;
        // The source of the error code is configuration services.
        public const int CONFIGURATION = 33;
        // The source of the error code is state management services.
        public const int STATE_MANAGEMENT = 34;
        // The source of the error code is the Microsoft Identity Server.
        public const int METADIRECTORY = 35;
        // The source of the error code is a Windows update.
        public const int WINDOWSUPDATE = 36;
        // The source of the error code is Active Directory.
        public const int DIRECTORYSERVICE = 37;
        // The source of the error code is the graphics drivers.
        public const int GRAPHICS = 38;
        // The source of the error code is the user Shell.
        public const int SHELL = 39;
        // The source of the error code is the Trusted Platform Module services.
        public const int TPM_SERVICES = 40;
        // The source of the error code is the Trusted Platform Module applications.
        public const int TPM_SOFTWARE = 41;
        // The source of the error code is Performance Logs and Alerts
        public const int PLA = 48;
        // The source of the error code is Full volume encryption.
        public const int FVE = 49;
        // The source of the error code is the Firewall Platform.
        public const int FWP = 50;
        // The source of the error code is the Windows Resource Manager.
        public const int WINRM = 51;
        // The source of the error code is the Network Driver Interface.
        public const int NDIS = 52;
        // The source of the error code is the Usermode Hypervisor components.
        public const int USERMODE_HYPERVISOR = 53;
        // The source of the error code is the Configuration Management Infrastructure.
        public const int CMI = 54;
        // The source of the error code is the user mode virtualization subsystem.
        public const int USERMODE_VIRTUALIZATION = 55;
        // The source of the error code is  the user mode volume manager
        public const int USERMODE_VOLMGR = 56;
        // The source of the error code is the Boot Configuration Database.
        public const int BCD = 57;
        // The source of the error code is user mode virtual hard disk support.
        public const int USERMODE_VHD = 58;
        // The source of the error code is System Diagnostics.
        public const int SDIAG = 60;
        // The source of the error code is the Web Services.
        public const int WEBSERVICES = 61;
        // The source of the error code is a Windows Defender component.
        public const int WINDOWS_DEFENDER = 80;
        // The source of the error code is the open connectivity service.
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
