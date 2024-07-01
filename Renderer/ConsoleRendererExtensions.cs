namespace Win32.Console;

public static class ConsoleRendererExtensions
{
    #region Bitfield

    public static void Bitfield(this IOnlySetterRenderer<ConsoleChar> renderer, Coord position, ReadOnlySpan<int> bitfield)
        => renderer.Bitfield(position, bitfield, new ConsoleChar('1', CharColor.BrightBlue, CharColor.Black), new ConsoleChar('1', CharColor.Blue, CharColor.Black));

    public static void Bitfield(this IOnlySetterRenderer<ConsoleChar> renderer, Coord position, int bitfield)
        => renderer.Bitfield(position, bitfield, new ConsoleChar('1', CharColor.BrightBlue, CharColor.Black), new ConsoleChar('1', CharColor.Blue, CharColor.Black));

    public static void Bitfield(this IOnlySetterRenderer<ConsoleChar> renderer, Coord position, ReadOnlySpan<uint> bitfield)
        => renderer.Bitfield(position, bitfield, new ConsoleChar('1', CharColor.BrightBlue, CharColor.Black), new ConsoleChar('1', CharColor.Blue, CharColor.Black));

    public static void Bitfield(this IOnlySetterRenderer<ConsoleChar> renderer, Coord position, uint bitfield)
        => renderer.Bitfield(position, bitfield, new ConsoleChar('1', CharColor.BrightBlue, CharColor.Black), new ConsoleChar('1', CharColor.Blue, CharColor.Black));

    #endregion
}
