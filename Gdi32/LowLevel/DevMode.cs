namespace Win32.Gdi32;

[StructLayout(LayoutKind.Sequential)]
public struct DevMode
{
    const int CCHDEVICENAME = 32;
    const int CCHFORMNAME = 32;

    [StructLayout(LayoutKind.Sequential)]
    public struct DUMMYSTRUCT
    {
        public short dmOrientation;
        public short dmPaperSize;
        public short dmPaperLength;
        public short dmPaperWidth;
        public short dmScale;
        public short dmCopies;
        public short dmDefaultSource;
        public short dmPrintQuality;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DUMMYSTRUCT2
    {
        public POINTL dmPosition;
        public DWORD dmDisplayOrientation;
        public DWORD dmDisplayFixedOutput;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DUMMYUNION
    {
        public DUMMYSTRUCT DUMMYSTRUCTNAME;
        public POINTL dmPosition;
        public DUMMYSTRUCT2 DUMMYSTRUCTNAME2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DUMMYUNION2
    {
        public DWORD dmDisplayFlags;
        public DWORD dmNup;
    }

    public unsafe fixed WCHAR dmDeviceName[CCHDEVICENAME];
    public WORD dmSpecVersion;
    public WORD dmDriverVersion;
    public WORD dmSize;
    public WORD dmDriverExtra;
    public DWORD dmFields;
    public DUMMYUNION DUMMYUNIONNAME;
    public short dmColor;
    public short dmDuplex;
    public short dmYResolution;
    public short dmTTOption;
    public short dmCollate;
    public unsafe fixed WCHAR dmFormName[CCHFORMNAME];
    public WORD dmLogPixels;
    public DWORD dmBitsPerPel;
    public DWORD dmPelsWidth;
    public DWORD dmPelsHeight;
    public DUMMYUNION2 DUMMYUNIONNAME2;
    public DWORD dmDisplayFrequency;
    public DWORD dmICMMethod;
    public DWORD dmICMIntent;
    public DWORD dmMediaType;
    public DWORD dmDitherType;
    public DWORD dmReserved1;
    public DWORD dmReserved2;
    public DWORD dmPanningWidth;
    public DWORD dmPanningHeight;
}
