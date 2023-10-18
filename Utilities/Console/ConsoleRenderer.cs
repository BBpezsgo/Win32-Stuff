using Microsoft.Win32.SafeHandles;

namespace Win32.Utilities
{
    public partial class ConsoleRenderer
    {
        protected readonly SafeFileHandle Handle;

        public short Width => bufferWidth;
        public short Height => bufferHeight;

        public COORD Rect => new(bufferWidth, bufferHeight);

        public int Size => bufferWidth * bufferHeight;

        protected short bufferWidth;
        protected short bufferHeight;

        protected CharInfo[] ConsoleBuffer;
        protected SmallRect ConsoleRect;

        public ref CharInfo this[int i] => ref ConsoleBuffer[i];
        public ref CharInfo this[int x, int y] => ref ConsoleBuffer[(y * bufferWidth) + x];

        public ConsoleRenderer(SafeFileHandle handle, short bufferWidth, short bufferHeight)
        {
            Handle = handle;
            this.bufferWidth = bufferWidth;
            this.bufferHeight = bufferHeight;
            ConsoleBuffer = new CharInfo[this.bufferWidth * this.bufferHeight];
            for (int i = 0; i < ConsoleBuffer.Length; i++)
            {
                ConsoleBuffer[i].Char = ' ';
            }
            ConsoleRect = new SmallRect() { Left = 0, Top = 0, Right = this.bufferWidth, Bottom = this.bufferHeight };
        }

        public bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < bufferWidth && y < bufferHeight;

        public void Render()
        {
            if (Handle.IsInvalid)
            {
                System.Diagnostics.Debug.WriteLine("Console handle is invalid");
                return;
            }

            if (Handle.IsClosed)
            { return; }

            if (Kernel32.WriteConsoleOutputW(
                Handle,
                ConsoleBuffer,
                new Coord(bufferWidth, bufferHeight),
                new Coord(0, 0),
                ref ConsoleRect) == 0)
            { throw WindowsException.Get(); }
        }

        public virtual void ClearBuffer() => Array.Clear(ConsoleBuffer);
    }
}
