global using BOOLEAN = System.Boolean;
global using BOOL = System.Int32;
global using ATOM = System.UInt16;
global using HBRUSH = System.IntPtr;
global using HCURSOR = System.IntPtr;
global using HDC = System.IntPtr;
global using HICON = System.IntPtr;
global using HINSTANCE = System.IntPtr;
global using HMENU = System.IntPtr;
global using HWND = System.IntPtr;
global using LPARAM = System.IntPtr;
global using WPARAM = System.UIntPtr;
global using LRESULT = System.IntPtr;
global using HRESULT = System.Int32;
global using HANDLE = System.IntPtr;
global using HLOCAL = System.IntPtr;
global using FLOAT = System.Single;
global using DOUBLE = System.Double;
global using HBITMAP = System.IntPtr;
global using HACCEL = System.IntPtr;
global using HGDIOBJ = System.IntPtr;
global using SIZE_T = System.UIntPtr;
global using HGLOBAL = System.IntPtr;
global using HRGN = System.IntPtr;
global using HPEN = System.IntPtr;
global using HMONITOR = System.IntPtr;

// Integer types

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

global using static Constants;

// Pointer Precision Types

global using DWORD_PTR = System.IntPtr;
global using INT_PTR = System.IntPtr;
global using LONG_PTR = System.IntPtr;
global using ULONG_PTR = System.UIntPtr;
global using UINT_PTR = System.UIntPtr;

// Characters

global using WCHAR = System.Char;
global using CHAR = System.Byte;

// Structures

global using RECT = Win32.Rect;
global using POINT = Win32.Point;
global using POINTL = Win32.Point;
global using CURSORINFO = Win32.CursorInfo;
global using GUITHREADINFO = Win32.GuiThreadInfo;
global using PBRANGE = Win32.ProgressBarRange;
global using STARTUPINFOW = Win32.StartupInfo;
global using CREATESTRUCT = Win32.CreateStruct;
global using WNDCLASSEXW = Win32.WindowClassEx;
global using COORD = Win32.Coord;
global using SIZE = Win32.Size;
global using TITLEBARINFO = Win32.TitleBarInfo;
global using MENUITEMINFOW = Win32.MenuItemInfo;
global using MENUINFO = Win32.MenuInfo;
global using MENUBARINFO = Win32.MenuBarInfo;
global using TPMPARAMS = Win32.TpmParams;
global using SMALL_RECT = Win32.SmallRect;

global using Win32.LowLevel;

public static class Constants
{
    public const BOOL FALSE = 0;
    public const BOOL TRUE = 1;
}
