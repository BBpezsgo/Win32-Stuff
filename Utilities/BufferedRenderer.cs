namespace Win32
{
    public abstract class BufferedRenderer<TPixel> : Renderer<TPixel>
    {
        public abstract Span<TPixel> Buffer { get; }

        #region Fill()

        public override void Fill(TPixel value) => Buffer.Fill(value);

        public override void Fill(SMALL_RECT rect, TPixel value)
        {
            int rectHeight = rect.Height;
            int rectWidth = rect.Width;
            int rectX = rect.X;
            int rectY = rect.Y;

            for (int offsetY = 0; offsetY < rectHeight; offsetY++)
            {
                int y = rectY + offsetY;
                if (y >= Height) break;
                if (y < 0) continue;

                FillRow(rectX, y, rectWidth, value);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void FillRow(int x, int y, int length, TPixel data)
        {
            int startIndex = (y * Width) + Math.Max(0, x);
            int endIndex = (y * Width) + Math.Min(Width - 1, x + length);

            if (startIndex >= endIndex) return;

            length = Math.Max(0, endIndex - startIndex);

            Buffer.Slice(startIndex, length).Fill(data);
        }

        #endregion

        #region Put()

        public override void Put(int x, int y, ReadOnlySpan<TPixel> data, int dataWidth, int dataHeight)
        {
            int height = Height;

            for (int offsetY = 0; offsetY < dataHeight; offsetY++)
            {
                if (y + offsetY < 0) continue;
                if (y + offsetY >= height) break;

                ReadOnlySpan<TPixel> row = data.Slice(offsetY * dataWidth, dataWidth);
                PutRow(x, y + offsetY, row);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void PutRow(int x, int y, ReadOnlySpan<TPixel> data)
        {
            ReadOnlySpan<TPixel> source = data;
            int width = Width;

            if (x < 0)
            {
                if (-x >= data.Length) return;
                source = source[-x..];
                x = 0;
            }

            if (x + source.Length > width)
            {
                if (width - x <= 0) return;
                source = source[..(width - x)];
            }

            Span<TPixel> destination = Buffer[(x + (y * width))..];

            source.CopyTo(destination);
        }

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
