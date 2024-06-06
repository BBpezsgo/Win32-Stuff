using System.Numerics;

namespace Win32;

public abstract class Renderer : IRenderer
{
    public abstract int Width { get; }
    public abstract int Height { get; }
    public Size Size => new(Width, Height);

    public virtual bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
    public virtual bool IsVisible(float x, float y) => IsVisible((int)MathF.Round(x), (int)MathF.Round(y));
    public virtual bool IsVisible(COORD position) => IsVisible(position.X, position.Y);
    public virtual bool IsVisible(POINT position) => IsVisible(position.X, position.Y);
    public virtual bool IsVisible(Vector2 position) => IsVisible((int)MathF.Round(position.X), (int)MathF.Round(position.Y));

    public abstract void Render();

    public abstract void RefreshBufferSize();
}

public abstract class Renderer<TPixel> : Renderer, IRenderer<TPixel>
{
    /// <exception cref="ArgumentOutOfRangeException"/>
    public abstract ref TPixel this[int i] { get; }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public virtual ref TPixel this[int x, int y] => ref this[(y * Width) + x];
    /// <e virtualxception cref="ArgumentOutOfRangeException"/>
    public virtual ref TPixel this[float x, float y] => ref this[((int)MathF.Round(y) * Width) + (int)MathF.Round(x)];
    /// <e virtualxception cref="ArgumentOutOfRangeException"/>
    public virtual ref TPixel this[COORD p] => ref this[(p.Y * Width) + p.X];
    /// <e virtualxception cref="ArgumentOutOfRangeException"/>
    public virtual ref TPixel this[POINT p] => ref this[(p.Y * Width) + p.X];
    /// <e virtualxception cref="ArgumentOutOfRangeException"/>
    public virtual ref TPixel this[Vector2 p] => ref this[((int)MathF.Round(p.Y) * Width) + (int)MathF.Round(p.X)];

    public void Set(int i, TPixel pixel) => this[i] = pixel;

    public virtual void Clear()
        => Fill(default!);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public virtual void Clear(SMALL_RECT rect)
        => Fill(rect, default!);

    public virtual void Fill(TPixel value)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                this[x, y] = value;
            }
        }
    }

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public virtual void Fill(SMALL_RECT rect, TPixel value)
    {
        for (int offsetY = 0; offsetY < rect.Height; offsetY++)
        {
            int y = rect.Y + offsetY;
            if (y >= Height) break;
            if (y < 0) continue;

            for (int offsetX = 0; offsetX < rect.Width; offsetX++)
            {
                int x = rect.X + offsetX;
                if (x < 0) continue;
                if (x >= Width) break;

                this[x, y] = value;
            }
        }
    }

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public void Put(int x, int y, ReadOnlySpan2D<TPixel> data)
        => Put(x, y, data.Span, data.Width, data.Height);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public virtual void Put(int x, int y, ReadOnlySpan<TPixel> data, int dataWidth, int dataHeight)
    {
        for (int offsetY = 0; offsetY < dataHeight; offsetY++)
        {
            if (y + offsetY < 0) continue;
            if (y + offsetY >= Height) break;

            if (y < 0) continue;
            if (y >= dataHeight) break;

            for (int offsetX = 0; offsetX < dataWidth; offsetX++)
            {
                if (x + offsetX < 0) continue;
                if (x + offsetX >= Width) break;

                if (x < 0) continue;
                if (x >= dataHeight) break;

                this[x + offsetX, y + offsetY] = data[x + (y * dataWidth)];
            }
        }
    }
}

public abstract class BufferedRenderer<TPixel> : Renderer<TPixel>, IBufferedRenderer<TPixel>
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
        for (int y = 0; y < rect.Height; y++)
        {
            int actualY = rect.Y + y;
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
