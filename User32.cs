using System.Runtime.InteropServices;

namespace Win32
{
    public static class User32
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern bool InvalidateRect(
          [In] HWND hWnd,
          [In] Rect* lpRect,
          [In] bool bErase
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern LRESULT DispatchMessageW(
            [In] Message* lpMsg
        );


        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern bool GetMessageW(
              [Out] Message* lpMsg,
              [In, Optional] HWND hWnd,
              [In] uint wMsgFilterMin,
              [In] uint wMsgFilterMax
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern HWND CreateWindowExW(
              [In] DWORD dwExStyle,
              [In, Optional] ushort* lpClassName,
              [In, Optional] ushort* lpWindowName,
              [In] DWORD dwStyle,
              [In] int X,
              [In] int Y,
              [In] int nWidth,
              [In] int nHeight,
              [In, Optional] HWND hWndParent,
              [In, Optional] HMENU hMenu,
              [In, Optional] HINSTANCE hInstance,
              [In, Optional] void* lpParam
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern bool EndPaint(
              [In] HWND hWnd,
              [In] PaintStruct* lpPaint
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern HDC BeginPaint(
              [In] HWND hWnd,
              [Out] PaintStruct* lpPaint
        );


        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern LRESULT DefWindowProcW(
            [In] HWND hWnd,
            [In] uint Msg,
            [In] WPARAM wParam,
            [In] LPARAM lParam
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern LRESULT RegisterClassExW(
            [In] HWND hWnd,
            [In] uint Msg,
            [In] WPARAM wParam,
            [In] LPARAM lParam);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        unsafe public static extern ATOM RegisterClassExW([In] WNDCLASSEXW* unnamedParam1);

    }
}
