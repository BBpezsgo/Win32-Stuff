using System.Buffers;
using Win32.Gdi32;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public unsafe class WindowRenderer : BufferedRenderer<uint>, IDisposable
    {
        readonly unsafe BitmapInfo BitmapInfo;

        public override short Width => (short)BufferWidth;
        public override short Height => (short)BufferHeight;

        int BufferWidth;
        int BufferHeight;
        IMemoryOwner<uint> _buffer;

        public readonly int WindowWidth;
        public readonly int WindowHeight;

        bool IsDisposed;

        public readonly Form Form;
        
        readonly DC DeviceContext;

        public override Span<uint> Buffer => _buffer.Memory.Span;

        public override ref uint this[int i] => ref _buffer.Memory.Span[i];

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

            _buffer = MemoryPool<uint>.Shared.Rent(width * height);
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
                using MemoryHandle bufferPtr = _buffer.Memory.Pin();
                DeviceContext.StretchDIBits(0, 0, WindowWidth, WindowHeight, 0, 0, BufferWidth, BufferHeight, bufferPtr.Pointer, pBmi, 0, 0x00CC0020);
            }
        }

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

        public override void RefreshBufferSize()
        {
            int newWidth = WindowWidth;
            int newHeight = WindowHeight;

            if (newWidth == BufferWidth && newHeight == BufferHeight) return;

            _buffer.Dispose();

            _buffer = MemoryPool<uint>.Shared.Rent(newWidth * newHeight);
            BufferWidth = newWidth;
            BufferHeight = newHeight;
        }
    }
}
