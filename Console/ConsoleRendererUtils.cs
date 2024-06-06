﻿using Win32.Console;

namespace Win32;

public static partial class RendererUtils
{
    #region Bitfield

    public static void Bitfield(this IOnlySetterRenderer<ConsoleChar> renderer, Coord position, int[] bitfield)
        => renderer.Bitfield(position, bitfield, new ConsoleChar('1', CharColor.BrightBlue, CharColor.Black), new ConsoleChar('1', CharColor.Blue, CharColor.Black));

    public static void Bitfield(this IOnlySetterRenderer<ConsoleChar> renderer, Coord position, int bitfield)
        => renderer.Bitfield(position, bitfield, new ConsoleChar('1', CharColor.BrightBlue, CharColor.Black), new ConsoleChar('1', CharColor.Blue, CharColor.Black));

    #endregion
}
