namespace Win32;

public static class BitmapFont
{
    public static BitmapFont<Console.ConsoleChar> FromConsoleImage(Console.ConsoleImage image, int charWidth, int charHeight)
        => new(image.AsSpan(), image.Width, image.Height, charWidth, charHeight);

    public static BitmapFont<TPixel> FromConsoleImage<TPixel>(Console.ConsoleImage image, int charWidth, int charHeight, Func<Console.ConsoleChar, TPixel> converter)
        => BitmapFont.FromAny(image.AsSpan(), image.Width, image.Height, charWidth, charHeight, converter);

    public static BitmapFont<TPixel> FromAny<T, TPixel>(ReadOnlySpan<T> buffer, int width, int height, int charWidth, int charHeight, Func<T, TPixel> converter)
    {
        Span<TPixel> result = new TPixel[buffer.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = converter.Invoke(buffer[i]);
        }
        return new BitmapFont<TPixel>(result, width, height, charWidth, charHeight);
    }
}

public readonly ref struct BitmapFont<TPixel>
{
    public readonly ReadOnlySpan<TPixel> Buffer;

    public readonly int Width;
    public readonly int Height;

    public readonly int CharWidth;
    public readonly int CharHeight;

    public BitmapFont(ReadOnlySpan<TPixel> buffer, int width, int height, int charWidth, int charHeight)
    {
        Buffer = buffer;

        Width = width;
        Height = height;

        CharWidth = charWidth;
        CharHeight = charHeight;
    }
}
