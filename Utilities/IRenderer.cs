using System.Numerics;

namespace Win32;

public interface IRenderer
{
    public int Width { get; }
    public int Height { get; }

    public void Render();
    public void RefreshBufferSize();
}

public interface IRenderer<TPixel> : IRenderer, IOnlySetterRenderer<TPixel>
{
    /// <exception cref="ArgumentOutOfRangeException"/>
    public ref TPixel this[int i] { get; }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ref TPixel this[int x, int y] => ref this[(y * Width) + x];

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ref TPixel this[float x, float y] => ref this[((int)MathF.Round(y) * Width) + (int)MathF.Round(x)];

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ref TPixel this[COORD p] => ref this[(p.Y * Width) + p.X];

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ref TPixel this[POINT p] => ref this[(p.Y * Width) + p.X];

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ref TPixel this[Vector2 p] => ref this[((int)MathF.Round(p.Y) * Width) + (int)MathF.Round(p.X)];
}

public interface IOnlySetterRenderer<TPixel> : IRenderer
{
    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Set(int i, TPixel pixel);
}

public interface IBufferedRenderer<TPixel> : IRenderer<TPixel>
{
    public Span<TPixel> Buffer { get; }
}

public static class RendererExtensions
{
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Set<TPixel>(this IOnlySetterRenderer<TPixel> renderer, int x, int y, TPixel pixel) => renderer.Set((y * renderer.Width) + x, pixel);

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Set<TPixel>(this IOnlySetterRenderer<TPixel> renderer, float x, float y, TPixel pixel) => renderer.Set(((int)MathF.Round(y) * renderer.Width) + (int)MathF.Round(x), pixel);

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Set<TPixel>(this IOnlySetterRenderer<TPixel> renderer, COORD p, TPixel pixel) => renderer.Set((p.Y * renderer.Width) + p.X, pixel);

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Set<TPixel>(this IOnlySetterRenderer<TPixel> renderer, POINT p, TPixel pixel) => renderer.Set((p.Y * renderer.Width) + p.X, pixel);

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Set<TPixel>(this IOnlySetterRenderer<TPixel> renderer, Vector2 p, TPixel pixel) => renderer.Set(((int)MathF.Round(p.Y) * renderer.Width) + (int)MathF.Round(p.X), pixel);

    public static bool IsVisible(this IRenderer renderer, int x, int y) => x >= 0 && y >= 0 && x < renderer.Width && y < renderer.Height;
    public static bool IsVisible(this IRenderer renderer, float x, float y) => renderer.IsVisible((int)MathF.Round(x), (int)MathF.Round(y));
    public static bool IsVisible(this IRenderer renderer, COORD position) => renderer.IsVisible(position.X, position.Y);
    public static bool IsVisible(this IRenderer renderer, POINT position) => renderer.IsVisible(position.X, position.Y);
    public static bool IsVisible(this IRenderer renderer, Vector2 position) => renderer.IsVisible((int)MathF.Round(position.X), (int)MathF.Round(position.Y));

    public static void Fill<TPixel>(this IOnlySetterRenderer<TPixel> renderer, TPixel value)
    {
        for (int y = 0; y < renderer.Height; y++)
        {
            for (int x = 0; x < renderer.Width; x++)
            {
                renderer.Set(x, y, value);
            }
        }
    }

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Fill<TPixel>(this IOnlySetterRenderer<TPixel> renderer, SMALL_RECT rect, TPixel value)
    {
        for (int offsetY = 0; offsetY < rect.Height; offsetY++)
        {
            int y = rect.Y + offsetY;
            if (y >= renderer.Height) break;
            if (y < 0) continue;

            for (int offsetX = 0; offsetX < rect.Width; offsetX++)
            {
                int x = rect.X + offsetX;
                if (x < 0) continue;
                if (x >= renderer.Width) break;

                renderer.Set(x, y, value);
            }
        }
    }

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Put<TPixel>(this IOnlySetterRenderer<TPixel> renderer, int x, int y, ReadOnlySpan2D<TPixel> data)
        => renderer.Put(x, y, data.Span, data.Width, data.Height);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Put<TPixel>(this IOnlySetterRenderer<TPixel> renderer, int x, int y, ReadOnlySpan<TPixel> data, int dataWidth, int dataHeight)
    {
        for (int offsetY = 0; offsetY < dataHeight; offsetY++)
        {
            if (y + offsetY < 0) continue;
            if (y + offsetY >= renderer.Height) break;

            if (y < 0) continue;
            if (y >= dataHeight) break;

            for (int offsetX = 0; offsetX < dataWidth; offsetX++)
            {
                if (x + offsetX < 0) continue;
                if (x + offsetX >= renderer.Width) break;

                if (x < 0) continue;
                if (x >= dataHeight) break;

                renderer.Set(x + offsetX, y + offsetY, data[x + (y * dataWidth)]);
            }
        }
    }

    public static void Fill<TPixel>(this IOnlySetterRenderer<TPixel> renderer, Func<int, int, TPixel> mapper)
    {
        for (int y = 0; y < renderer.Height; y++)
        {
            for (int x = 0; x < renderer.Width; x++)
            {
                renderer.Set(x, y, mapper.Invoke(x, y));
            }
        }
    }

    public static void FillNormalized<TPixel>(this IOnlySetterRenderer<TPixel> renderer, Func<float, float, TPixel> mapper)
    {
        for (int y = 0; y < renderer.Height; y++)
        {
            for (int x = 0; x < renderer.Width; x++)
            {
                renderer.Set(x, y, mapper.Invoke((float)x / (float)renderer.Width, (float)y / (float)renderer.Height));
            }
        }
    }

    #region Fill()

    public static void Fill<TPixel>(this IBufferedRenderer<TPixel> renderer, TPixel value)
        => renderer.Buffer.Fill(value);

    public static void Fill<TPixel>(this IBufferedRenderer<TPixel> renderer, SMALL_RECT rect, TPixel value)
        => BufferUtils.Fill(renderer.Buffer, renderer.Width, renderer.Height, rect, value);

    #endregion

    #region Put()

    public static void Put<TPixel>(this IBufferedRenderer<TPixel> renderer, int x, int y, ReadOnlySpan<TPixel> data, int dataWidth, int dataHeight)
        => BufferUtils.Put(renderer.Buffer, renderer.Width, renderer.Height, x, y, data, dataWidth, dataHeight);

    #endregion

    #region Clear()

    public static void Clear<TPixel>(this IBufferedRenderer<TPixel> renderer) => renderer.Buffer.Clear();

    public static void Clear<TPixel>(this IBufferedRenderer<TPixel> renderer, SMALL_RECT rect)
    {
        for (int y = 0; y < rect.Height; y++)
        {
            int actualY = rect.Y + y;
            if (actualY >= renderer.Height) break;
            if (actualY < 0) continue;

            int startIndex = (actualY * renderer.Width) + Math.Max((short)0, rect.Left);
            int endIndex = (actualY * renderer.Width) + Math.Min(renderer.Width - 1, rect.Right);
            int length = Math.Max(0, endIndex - startIndex);

            renderer.Buffer.Slice(startIndex, length).Clear();
        }
    }

    #endregion
}
