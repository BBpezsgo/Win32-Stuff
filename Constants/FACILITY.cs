namespace Win32
{
    public static class FACILITY
    {
        // The default facility code.
        public const int FACILITY_NULL = 0;
        // The source of the error code is an RPC subsystem.
        public const int FACILITY_RPC = 1;
        // The source of the error code is a COM Dispatch.
        public const int FACILITY_DISPATCH = 2;
        // The source of the error code is OLE Storage.
        public const int FACILITY_STORAGE = 3;
        // The source of the error code is COM/OLE Interface management.
        public const int FACILITY_ITF = 4;
        // This region is reserved to map undecorated error codes into HRESULTs.
        public const int FACILITY_WIN32 = 7;
        // The source of the error code is the Windows subsystem.
        public const int FACILITY_WINDOWS = 8;
        // The source of the error code is the Security API layer.
        public const int FACILITY_SECURITY = 9;
        // The source of the error code is the Security API layer.
        public const int FACILITY_SSPI = 9;
        // The source of the error code is the control mechanism.
        public const int FACILITY_CONTROL = 10;
        // The source of the error code is a certificate client or server?
        public const int FACILITY_CERT = 11;
        // The source of the error code is Wininet related.
        public const int FACILITY_INTERNET = 12;
        // The source of the error code is the Windows Media Server.
        public const int FACILITY_MEDIASERVER = 13;
        // The source of the error code is the Microsoft Message Queue.
        public const int FACILITY_MSMQ = 14;
        // The source of the error code is the Setup API.
        public const int FACILITY_SETUPAPI = 15;
        // The source of the error code is the Smart-card subsystem.
        public const int FACILITY_SCARD = 16;
        // The source of the error code is COM+.
        public const int FACILITY_COMPLUS = 17;
        // The source of the error code is the Microsoft agent.
        public const int FACILITY_AAF = 18;
        // The source of the error code is .NET CLR.
        public const int FACILITY_URT = 19;
        // The source of the error code is the audit collection service.
        public const int FACILITY_ACS = 20;
        // The source of the error code is Direct Play.
        public const int FACILITY_DPLAY = 21;
        // The source of the error code is the ubiquitous memoryintrospection service.
        public const int FACILITY_UMI = 22;
        // The source of the error code is Side-by-side servicing.
        public const int FACILITY_SXS = 23;
        // The error code is specific to Windows CE.
        public const int FACILITY_WINDOWS_CE = 24;
        // The source of the error code is HTTP support.
        public const int FACILITY_HTTP = 25;
        // The source of the error code is common Logging support.
        public const int FACILITY_USERMODE_COMMONLOG = 26;
        // The source of the error code is the user mode filter manager.
        public const int FACILITY_USERMODE_FILTER_MANAGER = 31;
        // The source of the error code is background copy control
        public const int FACILITY_BACKGROUNDCOPY = 32;
        // The source of the error code is configuration services.
        public const int FACILITY_CONFIGURATION = 33;
        // The source of the error code is state management services.
        public const int FACILITY_STATE_MANAGEMENT = 34;
        // The source of the error code is the Microsoft Identity Server.
        public const int FACILITY_METADIRECTORY = 35;
        // The source of the error code is a Windows update.
        public const int FACILITY_WINDOWSUPDATE = 36;
        // The source of the error code is Active Directory.
        public const int FACILITY_DIRECTORYSERVICE = 37;
        // The source of the error code is the graphics drivers.
        public const int FACILITY_GRAPHICS = 38;
        // The source of the error code is the user Shell.
        public const int FACILITY_SHELL = 39;
        // The source of the error code is the Trusted Platform Module services.
        public const int FACILITY_TPM_SERVICES = 40;
        // The source of the error code is the Trusted Platform Module applications.
        public const int FACILITY_TPM_SOFTWARE = 41;
        // The source of the error code is Performance Logs and Alerts
        public const int FACILITY_PLA = 48;
        // The source of the error code is Full volume encryption.
        public const int FACILITY_FVE = 49;
        // The source of the error code is the Firewall Platform.
        public const int FACILITY_FWP = 50;
        // The source of the error code is the Windows Resource Manager.
        public const int FACILITY_WINRM = 51;
        // The source of the error code is the Network Driver Interface.
        public const int FACILITY_NDIS = 52;
        // The source of the error code is the Usermode Hypervisor components.
        public const int FACILITY_USERMODE_HYPERVISOR = 53;
        // The source of the error code is the Configuration Management Infrastructure.
        public const int FACILITY_CMI = 54;
        // The source of the error code is the user mode virtualization subsystem.
        public const int FACILITY_USERMODE_VIRTUALIZATION = 55;
        // The source of the error code is  the user mode volume manager
        public const int FACILITY_USERMODE_VOLMGR = 56;
        // The source of the error code is the Boot Configuration Database.
        public const int FACILITY_BCD = 57;
        // The source of the error code is user mode virtual hard disk support.
        public const int FACILITY_USERMODE_VHD = 58;
        // The source of the error code is System Diagnostics.
        public const int FACILITY_SDIAG = 60;
        // The source of the error code is the Web Services.
        public const int FACILITY_WEBSERVICES = 61;
        // The source of the error code is a Windows Defender component.
        public const int FACILITY_WINDOWS_DEFENDER = 80;
        // The source of the error code is the open connectivity service.
        public const int FACILITY_OPC = 81;

        public static string? ToString(int code) => code switch
        {
            FACILITY.FACILITY_RPC => "an RPC subsystem",
            FACILITY.FACILITY_DISPATCH => "a COM Dispatch",
            FACILITY.FACILITY_STORAGE => "OLE Storage",
            FACILITY.FACILITY_ITF => "COM/OLE Interface management",
            FACILITY.FACILITY_WINDOWS => "Windows subsystem",
            FACILITY.FACILITY_SECURITY => "Security API layer",
            FACILITY.FACILITY_CONTROL => "control mechanism",
            FACILITY.FACILITY_CERT => "a certificate client or server",
            FACILITY.FACILITY_INTERNET => "Wininet related",
            FACILITY.FACILITY_MEDIASERVER => "Windows Media Server",
            FACILITY.FACILITY_MSMQ => "Microsoft Message Queue",
            FACILITY.FACILITY_SETUPAPI => "Setup API",
            FACILITY.FACILITY_SCARD => "Smart-card subsystem",
            FACILITY.FACILITY_COMPLUS => "COM+",
            FACILITY.FACILITY_AAF => "Microsoft agent",
            FACILITY.FACILITY_URT => ".NET CLR",
            FACILITY.FACILITY_ACS => "audit collection service",
            FACILITY.FACILITY_DPLAY => "Direct Play",
            FACILITY.FACILITY_UMI => "ubiquitous memoryintrospection service",
            FACILITY.FACILITY_SXS => "Side-by-side servicing",
            FACILITY.FACILITY_WINDOWS_CE => "CE",
            FACILITY.FACILITY_HTTP => "HTTP support",
            FACILITY.FACILITY_USERMODE_COMMONLOG => "common Logging support",
            FACILITY.FACILITY_USERMODE_FILTER_MANAGER => "user mode filter manager",
            FACILITY.FACILITY_BACKGROUNDCOPY => "background copy control",
            FACILITY.FACILITY_CONFIGURATION => "configuration services",
            FACILITY.FACILITY_STATE_MANAGEMENT => "state management services",
            FACILITY.FACILITY_METADIRECTORY => "Microsoft Identity Server",
            FACILITY.FACILITY_WINDOWSUPDATE => "Windows update",
            FACILITY.FACILITY_DIRECTORYSERVICE => "Active Directory",
            FACILITY.FACILITY_GRAPHICS => "graphics drivers",
            FACILITY.FACILITY_SHELL => "user Shell",
            FACILITY.FACILITY_TPM_SERVICES => "Trusted Platform Module services",
            FACILITY.FACILITY_TPM_SOFTWARE => "Trusted Platform Module applications",
            FACILITY.FACILITY_PLA => "Performance Logs and Alert",
            FACILITY.FACILITY_FVE => "Full volume encryption",
            FACILITY.FACILITY_FWP => "Firewall Platform",
            FACILITY.FACILITY_WINRM => "Windows Resource Manager",
            FACILITY.FACILITY_NDIS => "Network Driver Interface",
            FACILITY.FACILITY_USERMODE_HYPERVISOR => "Usermode Hypervisor components",
            FACILITY.FACILITY_CMI => "Configuration Management Infrastructure",
            FACILITY.FACILITY_USERMODE_VIRTUALIZATION => "user mode virtualization subsystem",
            FACILITY.FACILITY_USERMODE_VOLMGR => "user mode volume manage",
            FACILITY.FACILITY_BCD => "Boot Configuration Database",
            FACILITY.FACILITY_USERMODE_VHD => "mode virtual hard disk support",
            FACILITY.FACILITY_SDIAG => "System Diagnostics",
            FACILITY.FACILITY_WEBSERVICES => "Web Services",
            FACILITY.FACILITY_WINDOWS_DEFENDER => "a Windows Defender component",
            FACILITY.FACILITY_OPC => "open connectivity service",
            _ => null,
        };
    }
}
