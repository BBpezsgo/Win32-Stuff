using System.Runtime.CompilerServices;

namespace Win32.Gdi32;

[SupportedOSPlatform("windows")]
[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
public readonly struct PaintHandle : IDisposable
{
    readonly unsafe PaintStruct* Paint;
    public DisplayDC DeviceContext { get; }

    unsafe PaintHandle(DisplayDC dc, PaintStruct* paint)
    {
        this.DeviceContext = dc;
        this.Paint = paint;
    }

    public static implicit operator HDC(PaintHandle paintHandle) => paintHandle.DeviceContext;
    public static implicit operator DisplayDC(PaintHandle paintHandle) => paintHandle.DeviceContext;

    /// <inheritdoc/>
    public override string ToString() => DeviceContext.ToString();

    public static unsafe PaintHandle Begin(HWND window, out PaintStruct paint)
    {
        paint = default;
        PaintStruct* paintPtr = (PaintStruct*)Unsafe.AsPointer(ref paint);
        HDC dcHandle = User32.BeginPaint(window, paintPtr);
        return new PaintHandle(new DisplayDC(dcHandle, window), paintPtr);
    }

    public unsafe void Dispose() => _ = User32.EndPaint(DeviceContext, Paint);
}
