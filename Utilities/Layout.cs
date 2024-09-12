namespace Win32;

public static class Layout
{
    public static int Center(ReadOnlySpan<char> text, int container) => Center(text.Length, container);
    public static int Center(int size, int container) => (container / 2) - (size / 2);

    public static RECT Center(SIZE size, RECT container)
    {
        LONG x = container.X + Center(size.Width, container.Width);
        LONG y = container.Y + Center(size.Height, container.Height);
        return new RECT(x, y, size.Width, size.Height);
    }

    public static SMALL_RECT Center(Console.SmallSize size, SMALL_RECT container)
    {
        SHORT x = (SHORT)(container.X + Center(size.Width, container.Width));
        SHORT y = (SHORT)(container.Y + Center(size.Height, container.Height));
        return new SMALL_RECT(x, y, size.Width, size.Height);
    }

    public static RECT Center(ReadOnlySpan<char> text, RECT container)
    {
        LONG x = container.X + Center(text, container.Width);
        LONG y = container.Y + (container.Height / 2);
        return new RECT(x, y, text.Length, 1);
    }

    public static SMALL_RECT Center(ReadOnlySpan<char> text, SMALL_RECT container)
    {
        SHORT x = (SHORT)(container.X + Center(text, container.Width));
        SHORT y = (SHORT)(container.Y + (container.Height / 2));
        return new SMALL_RECT(x, y, text.Length, 1);
    }
}
