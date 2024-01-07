using System.Diagnostics;

namespace Win32
{
    public static partial class Mouse
    {
        /// <exception cref="WindowsException"/>
        [SupportedOSPlatform("windows")]
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static Window? CapturedBy
        {
            get
            {
                HWND handle = User32.GetCapture();
                if (handle == HWND.Zero)
                { return null; }
                return (Window)handle;
            }
            set
            {
                if (value is null)
                {
                    if (User32.ReleaseCapture() == FALSE)
                    { throw WindowsException.Get(); }
                }
                else
                {
                    HWND _ = User32.SetCapture(value.Handle);
                }
            }
        }

        /// <exception cref="WindowsException"/>
        [SupportedOSPlatform("windows")]
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public static UINT DoubleClickTime
        {
            get => User32.GetDoubleClickTime();
            set
            {
                if (User32.SetDoubleClickTime(value) == FALSE)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        [SupportedOSPlatform("windows")]
        public static POINT ScreenPosition
        {
            get
            {
                if (User32.GetCursorPos(out POINT point) == 0)
                { throw WindowsException.Get(); }
                return point;
            }
            set
            {
                if (User32.SetCursorPos(value.X, value.Y) == 0)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        [SupportedOSPlatform("windows")]
        public static unsafe Span<MouseMovePoint> GetMovePoints(MouseMovePoint lppt, int count, bool highResolution = false)
        {
            MouseMovePoint[] buffer = new MouseMovePoint[count];

            int n;
            fixed (MouseMovePoint* bufferPtr = buffer)
            {
                n = User32.GetMouseMovePointsEx(
                    (uint)sizeof(MouseMovePoint),
                    &lppt,
                    bufferPtr,
                    count,
                    highResolution ? (uint)2 : (uint)1);
            }

            if (n == -1)
            { throw WindowsException.Get(); }

            return buffer.AsSpan(0, n);
        }
    }
}