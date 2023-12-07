using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Win32.Gdi32
{
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public readonly struct PaintHandle : IDisposable
    {
        readonly DisplayDC dc;
        unsafe readonly PaintStruct* paint;

        public DisplayDC DeviceContext => dc;

        unsafe PaintHandle(DisplayDC dc, PaintStruct* paint)
        {
            this.dc = dc;
            this.paint = paint;
        }

        public static implicit operator HDC(PaintHandle paintHandle) => paintHandle.dc;
        public static implicit operator DisplayDC(PaintHandle paintHandle) => paintHandle.dc;

        public override string ToString() => dc.ToString();

        unsafe public static PaintHandle Begin(HWND window, out PaintStruct paint)
        {
            paint = default;
            PaintStruct* paintPtr = (PaintStruct*)Unsafe.AsPointer(ref paint);
            HDC dcHandle = User32.BeginPaint(window, paintPtr);
            return new PaintHandle(new DisplayDC(dcHandle, window), paintPtr);
        }

        unsafe public void Dispose() => _ = User32.EndPaint(dc, paint);
    }
}
