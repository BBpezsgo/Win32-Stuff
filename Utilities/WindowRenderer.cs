using System.Buffers;
using Win32.Console;
using Win32.Gdi32;

namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public sealed unsafe class WindowRenderer : BufferedRenderer<uint>, IOnlySetterRenderer<GdiColor>, IDisposable
{
    public override int Width => (short)BufferWidth;
    public override int Height => (short)BufferHeight;

    public int WindowWidth { get; }
    public int WindowHeight { get; }

    public Form Form { get; }
    public override Span<uint> Buffer => MemoryBuffer.Memory.Span;

    public override ref uint this[int i] => ref MemoryBuffer.Memory.Span[i];

    readonly BitmapInfo BitmapInfo;
    int BufferWidth;
    int BufferHeight;
    IMemoryOwner<uint> MemoryBuffer;
    bool IsDisposed;
    readonly DC DC;

    public WindowRenderer(int width, int height, int windowWidth, int windowHeight)
    {
        BufferWidth = width;
        BufferHeight = height;
        WindowWidth = windowWidth;
        WindowHeight = windowHeight;

        BitmapInfoHeader bitmapInfoHeader = BitmapInfoHeader.Create();
        bitmapInfoHeader.Width = BufferWidth;
        bitmapInfoHeader.Height = -BufferHeight;
        bitmapInfoHeader.Planes = 1;
        bitmapInfoHeader.BitCount = 32;
        bitmapInfoHeader.Compression = BitmapCompression.RGB;

        BitmapInfo = new BitmapInfo()
        {
            Header = bitmapInfoHeader,
            Colors = default,
        };

        Form = new Form(
            "Bruh",
            windowWidth, windowHeight
        );

        DC = Form.GetClientDC();

        MemoryBuffer = MemoryPool<uint>.Shared.Rent(width * height);
        BufferWidth = width;
        BufferHeight = height;
    }

    public void Set(int i, GdiColor pixel) => this[i] = pixel;

    public override void Render()
    {
        fixed (BitmapInfo* pBmi = &BitmapInfo)
        {
            using MemoryHandle bufferPtr = MemoryBuffer.Memory.Pin();
            DC.StretchDIBits(0, 0, WindowWidth, WindowHeight, 0, 0, BufferWidth, BufferHeight, bufferPtr.Pointer, pBmi, 0, 0x00CC0020);
        }
    }

    void Dispose(bool disposing)
    {
        if (IsDisposed) return;

        if (disposing)
        {
            if (Form != 0)
            {
                DC.Dispose();
                Form.Dispose();
            }
            MemoryBuffer.Dispose();
        }

        IsDisposed = true;
    }
    ~WindowRenderer()
    { Dispose(disposing: false); }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public override void RefreshBufferSize()
    {
        int newWidth = WindowWidth;
        int newHeight = WindowHeight;

        if (newWidth == BufferWidth && newHeight == BufferHeight) return;

        MemoryBuffer.Dispose();

        MemoryBuffer = MemoryPool<uint>.Shared.Rent(newWidth * newHeight);
        BufferWidth = newWidth;
        BufferHeight = newHeight;
    }
}
