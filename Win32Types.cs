#pragma warning disable IDE0005

global using BOOLEAN = System.Boolean;
global using BOOL = System.Int32;
global using ATOM = System.UInt16;
global using HBRUSH = nint;
global using HCURSOR = nint;
global using HDC = nint;
global using HICON = nint;
global using HINSTANCE = nint;
global using HMENU = nint;
global using HWND = nint;
global using LPARAM = nint;
global using WPARAM = nuint;
global using LRESULT = nint;
global using HRESULT = System.Int32;
global using HANDLE = nint;
global using HLOCAL = nint;
global using FLOAT = System.Single;
global using DOUBLE = System.Double;
global using HBITMAP = nint;
global using HACCEL = nint;
global using HGDIOBJ = nint;
global using SIZE_T = nuint;
global using HGLOBAL = nint;
global using HRGN = nint;
global using HPEN = nint;
global using HMONITOR = nint;
global using HMODULE = nint;

// Integer types

global using LARGE_INTEGER = System.UInt64;

global using BYTE = System.Byte;

global using DWORD = System.UInt32;
global using WORD = System.UInt16;

global using INT = System.Int32;
global using INT8 = System.SByte;
global using INT16 = System.Int16;
global using INT32 = System.Int32;
global using INT64 = System.Int64;

global using UINT = System.UInt32;
global using UINT8 = System.Byte;
global using UINT16 = System.UInt16;
global using UINT32 = System.UInt32;
global using UINT64 = System.UInt64;

global using SHORT = System.Int16;

global using LONG = System.Int32;
global using LONGLONG = System.Int64;
global using ULONG = System.UInt32;
global using ULONGLONG = System.UInt64;

// Boolean Type

global using static Win32.Constants;

// Pointer Precision Types

global using DWORD_PTR = nint;
global using INT_PTR = nint;
global using LONG_PTR = nint;
global using ULONG_PTR = nuint;
global using UINT_PTR = nuint;

// Characters

global using WCHAR = System.Char;
global using CHAR = System.Byte;
global using UCHAR = System.Byte;

// Other
global using IID = System.Guid;
global using REFIID = System.Guid;
global using REFCLSID = System.Guid;
global using ACCESS_MASK = System.UInt32;
global using unsafe FARPROC = delegate* unmanaged<nint>;

#region MCI

global using MCIERROR = System.UInt32;
global using MCIDEVICEID = System.UInt32;

#endregion

#region OTHER

global using VARTYPE = ushort;
global using USHORT = ushort;

global using OLECHAR = char;
global using unsafe BSTR = char*;
global using unsafe LPBSTR = char**;

global using VARIANT_BOOL = short;
global using DATE = double;
global using SCODE = int;

#endregion

namespace Win32;

public static class Constants
{
    public const BOOL FALSE = 0;
    public const BOOL TRUE = 1;

    public static readonly IID CLSID_WICImagingFactory = new(0x317d06e8, 0x5f24, 0x433d, 0xbd, 0xf7, 0x79, 0xce, 0x68, 0xd8, 0xab, 0xc2);
    public static readonly IID BHID_ThumbnailHandler = new(0x7b2e650a, 0x8e20, 0x4f4a, 0xb0, 0x9e, 0x65, 0x97, 0xaf, 0xc7, 0x2f, 0xb0);
}
