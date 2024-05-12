using System.Buffers;
using Win32.Gdi32;

namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public sealed unsafe class WindowRenderer : BufferedRenderer<uint>, IDisposable
{
    public override short Width => (short)BufferWidth;
    public override short Height => (short)BufferHeight;

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

        fixed (byte* classNamePtr = "edit"u8)
        {
            Form = new Form(
                    "Bruh",
                    windowWidth, windowHeight,
                    styles: WindowStyles.VISIBLE | WindowStyles.CAPTION | WindowStyles.CLIPSIBLINGS | WindowStyles.CLIPCHILDREN,
                    exStyles: WindowStylesEx.APPWINDOW | WindowStylesEx.WINDOWEDGE
                );

            // (Window)Win32.LowLevel.User32.CreateWindowExA(
            // Win32.LowLevel.WindowStyles.EX_APPWINDOW | Win32.LowLevel.WindowStyles.EX_WINDOWEDGE,
            // classNamePtr,
            // null,
            // Win32.LowLevel.WindowStyles.VISIBLE | Win32.LowLevel.WindowStyles.CAPTION | Win32.LowLevel.WindowStyles.CLIPSIBLINGS | Win32.LowLevel.WindowStyles.CLIPCHILDREN,
            // 0, 0,
            // WindowWidth, WindowHeight);
        }

        DC = Form.GetClientDC();

        MemoryBuffer = MemoryPool<uint>.Shared.Rent(width * height);
        BufferWidth = width;
        BufferHeight = height;
    }

    public bool Tick()
    {
        Form.HandleNextEvent();
        return Form != 0;
    }

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
