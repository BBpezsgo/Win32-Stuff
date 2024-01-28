using System.Buffers;
using Win32.Gdi32;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public unsafe class WindowRenderer : IDisposable
    {
        readonly unsafe BitmapInfo BitmapInfo;

        public readonly int Width;
        public readonly int Height;

        readonly int WindowWidth;
        readonly int WindowHeight;

        bool IsDisposed;

        public readonly Form Form;
        readonly DC DeviceContext;
        readonly IMemoryOwner<uint> Buffer;

        public uint this[int x, int y]
        {
            get => Buffer.Memory.Span[x + (y * Width)];
            set => Buffer.Memory.Span[x + (y * Width)] = value;
        }

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

            fixed (byte* classNamePtr = "edit"u8)
            {
                Form = new Form(
                        "Bruh",
                        windowWidth, windowHeight,
                        styles: WindowStyles.VISIBLE | WindowStyles.CAPTION | WindowStyles.CLIPSIBLINGS | WindowStyles.CLIPCHILDREN,
                        exStyles: WindowStyles.EX_APPWINDOW | WindowStyles.EX_WINDOWEDGE
                    );

                // (Window)Win32.LowLevel.User32.CreateWindowExA(
                // Win32.LowLevel.WindowStyles.EX_APPWINDOW | Win32.LowLevel.WindowStyles.EX_WINDOWEDGE,
                // classNamePtr,
                // null,
                // Win32.LowLevel.WindowStyles.VISIBLE | Win32.LowLevel.WindowStyles.CAPTION | Win32.LowLevel.WindowStyles.CLIPSIBLINGS | Win32.LowLevel.WindowStyles.CLIPCHILDREN,
                // 0, 0,
                // WindowWidth, WindowHeight);
            }

            DeviceContext = Form.GetClientDC();

            Buffer = MemoryPool<uint>.Shared.Rent(width * height);
            Width = width;
            Height = height;
        }

        public bool Tick()
        {
            Form.HandleNextEvent();
            return Form != 0;
        }

        public void Render()
        {
            fixed (BitmapInfo* pBmi = &BitmapInfo)
            {
                using MemoryHandle bufferPtr = Buffer.Memory.Pin();
                DeviceContext.StretchDIBits(0, 0, WindowWidth, WindowHeight, 0, 0, Width, Height, bufferPtr.Pointer, pBmi, 0, 0x00CC0020);
            }
        }

        public void Fill(uint color) => Buffer.Memory.Span.Fill(color);

        void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                if (Form != 0)
                {
                    DeviceContext.Dispose();
                    Form.Dispose();
                }
                Buffer.Dispose();
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
    }
}
