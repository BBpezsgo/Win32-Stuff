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
