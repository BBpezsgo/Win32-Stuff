namespace Win32
{
    public abstract class BufferedRenderer<TPixel> : Renderer<TPixel>
    {
        public abstract Span<TPixel> Buffer { get; }

        #region Fill()

        public override void Fill(TPixel value) => Buffer.Fill(value);

        public override void Fill(SMALL_RECT rect, TPixel value)
            => BufferUtils.Fill(Buffer, Width, Height, rect, value);

        #endregion

        #region Put()

        public override void Put(int x, int y, ReadOnlySpan<TPixel> data, int dataWidth, int dataHeight)
            => BufferUtils.Put(Buffer, Width, Height, x, y, data, dataWidth, dataHeight);

        #endregion

        #region Clear()

        public override void Clear() => Buffer.Clear();

        public override void Clear(SMALL_RECT rect)
        {
            for (int _y = 0; _y < rect.Height; _y++)
            {
                int actualY = rect.Y + _y;
                if (actualY >= Height) break;
                if (actualY < 0) continue;

                int startIndex = (actualY * Width) + Math.Max((short)0, rect.Left);
                int endIndex = (actualY * Width) + Math.Min(Width - 1, rect.Right);
                int length = Math.Max(0, endIndex - startIndex);

                Buffer.Slice(startIndex, length).Clear();
            }
        }

        #endregion
    }
}
