global using STARTUPINFOW = Win32.StartupInfo;

namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct StartupInfo
{
    public DWORD StructSize;

    readonly unsafe WCHAR* Reserved;

    public unsafe WCHAR* Desktop;
    public unsafe WCHAR* Title;
    public DWORD X;
    public DWORD Y;
    public DWORD XSize;
    public DWORD YSize;
    public DWORD XCountChars;
    public DWORD YCountChars;
    public DWORD FillAttribute;
    public DWORD Flags;
    public WORD ShowWindow;

    readonly WORD Reserved2;
    readonly unsafe BYTE* Reserved3;

    public HANDLE StdInput;
    public HANDLE StdOutput;
    public HANDLE StdError;
}
