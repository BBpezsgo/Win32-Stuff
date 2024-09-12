using System.Buffers;
using Win32.Console;
using Win32.Gdi32;

namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public sealed unsafe class WindowRenderer : IRenderer<uint>, IOnlySetterRenderer<GdiColor>, IDisposable
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public int WindowWidth { get; }
    public int WindowHeight { get; }

    public Form Form { get; }
    public Memory<uint> Buffer => _buffer.Memory;

    public ref uint this[int i] => ref _buffer.Memory.Span[i];

    readonly BitmapInfo BitmapInfo;
    IMemoryOwner<uint> _buffer;
    bool IsDisposed;
    readonly DC DC;

    public WindowRenderer(int width, int height, int windowWidth, int windowHeight)
    {
        Width = width;
        Height = height;
        WindowWidth = windowWidth;
        WindowHeight = windowHeight;

        BitmapInfoHeader bitmapInfoHeader = BitmapInfoHeader.Create();
        bitmapInfoHeader.Width = Width;
        bitmapInfoHeader.Height = -Height;
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

        _buffer = MemoryPool<uint>.Shared.Rent(width * height);
        Width = width;
        Height = height;
    }

    public void Set(int i, GdiColor pixel) => Buffer.Span[i] = pixel;
    public void Set(int i, uint pixel) => Buffer.Span[i] = pixel;

    public void Render()
    {
        fixed (BitmapInfo* pBmi = &BitmapInfo)
        {
            using MemoryHandle bufferPtr = _buffer.Memory.Pin();
            DC.StretchDIBits(0, 0, WindowWidth, WindowHeight, 0, 0, Width, Height, bufferPtr.Pointer, pBmi, 0, 0x00CC0020);
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
            _buffer.Dispose();
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

    public void RefreshBufferSize()
    {
        int newWidth = WindowWidth;
        int newHeight = WindowHeight;

        if (newWidth == Width && newHeight == Height) return;

        _buffer.Dispose();

        _buffer = MemoryPool<uint>.Shared.Rent(newWidth * newHeight);
        Width = newWidth;
        Height = newHeight;
    }
}
