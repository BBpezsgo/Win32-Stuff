﻿using System.Runtime.Versioning;
using System.Text;

namespace Win32
{
    public partial struct Ansi
    {
        public const char ESC = '\x1B';
        public const char CSI = '[';
        public const char DCS = 'P';
        public const char OSC = ']';

        const string _ESC = "\x1B";
        const string _CSI = "[";

        #region General ASCII Codes
        /// <summary>
        /// Terminal bell
        /// </summary>
        public const char BEL = '\x07';
        /// <summary>
        /// Backspace
        /// </summary>
        public const char BS = '\x08';
        /// <summary>
        /// Horizontal TAB
        /// </summary>
        public const char HT = '\x09';
        /// <summary>
        /// Linefeed(newline)
        /// </summary>
        public const char LF = '\x0A';
        /// <summary>
        /// Vertical TAB
        /// </summary>
        public const char VT = '\x0B';
        /// <summary>
        /// Formfeed(also: New page NP)
        /// </summary>
        public const char FF = '\x0C';
        /// <summary>
        /// Carriage return
        /// </summary>
        public const char CR = '\x0D';
        /// <summary>
        /// Delete character
        /// </summary>
        public const char DEL = '\x7F';
        #endregion

        #region Cursor Controls
        /// <summary>
        /// moves cursor to home position (0, 0);
        /// </summary>
        public const string ResetCursor = $"{_ESC}{_CSI}H";
        public static string SetCursorPosition(ushort line, ushort column) => $"{ESC}{CSI}{line};{column}H";
        // /// <summary>
        // /// moves cursor to line #, column #
        // /// </summary>
        // public const string = $"{ESC}[{line};{column}f	
        /// <summary>
        /// moves cursor up # lines
        /// </summary>
        public static string MoveCursorUp(int lines) => $"{ESC}{_CSI}{lines}A";
        /// <summary>
        /// moves cursor down # lines
        /// </summary>
        public static string MoveCursorDown(int lines) => $"{ESC}{_CSI}{lines}B";
        /// <summary>
        /// moves cursor right # columns
        /// </summary>
        public static string MoveCursorRight(int columns) => $"{ESC}{_CSI}{columns}C";
        /// <summary>
        /// moves cursor left # columns
        /// </summary>
        public static string MoveCursorLeft(int columns) => $"{ESC}{_CSI}{columns}D";
        /// <summary>
        /// moves cursor to beginning of next line, # lines down
        /// </summary>
        public static string MoveCursorNextLine(int lines) => $"{ESC}{_CSI}{lines}E";
        /// <summary>
        /// moves cursor to beginning of previous line, # lines up
        /// </summary>
        public static string MoveCursorPreviousLine(int lines) => $"{ESC}{_CSI}{lines}F";
        /// <summary>
        /// moves cursor to column #
        /// </summary>
        public static string SetCursorPosColumn(int column) => $"{ESC}{_CSI}{column}G";
        /// <summary>
        /// request cursor position (reports as ESC[#;#R)
        /// </summary>
        public const string ReportCursorPosition = $"{_ESC}{_CSI}6n";
        /// <summary>
        /// moves cursor one line up, scrolling if needed
        /// </summary>
        public const string MoveCursorUpAndScroll = $"{_ESC} M";
        /// <summary>
        /// save cursor position (DEC)
        /// </summary>
        public const string SaveCursorPositionDEC = $"{_ESC} 7";
        /// <summary>
        /// restores the cursor to the last saved position (DEC)
        /// </summary>
        public const string RestoreCursorPositionDEC = $"{_ESC} 8";
        /// <summary>
        /// save cursor position (SCO)
        /// </summary>
        public const string SaveCursorPositionSCO = $"{_ESC}{_CSI}s";
        /// <summary>
        /// restores the cursor to the last saved position (SCO)
        /// </summary>
        public const string RestoreCursorPositionDSCO = $"{_ESC}{_CSI}u";

        #endregion

        #region Erase Functions
        // /// <summary>
        // /// erase in display (same as ESC[0J)
        // /// </summary>
        // public const string EraseInDisplay = $"{_ESC}{_CSI}J";
        /// <summary>
        /// erase from cursor until end of screen
        /// </summary>
        public const string EraseAfterCursor = $"{_ESC}{_CSI}0J";
        /// <summary>
        /// erase from cursor to beginning of screen
        /// </summary>
        public const string EraseUntilCursor = $"{_ESC}{_CSI}1J";
        /// <summary>
        /// erase entire screen
        /// </summary>
        public const string ClearScreen = $"{_ESC}{_CSI}2J";
        /// <summary>
        /// erase saved lines
        /// </summary>
        public const string EraseSavedLines = $"{_ESC}{_CSI}3J";
        /// <summary>
        /// erase in line (same as ESC{_CSI}0K)
        /// </summary>
        public const string EraseSavedLine = $"{_ESC}{_CSI}K";
        /// <summary>
        /// erase from cursor to end of line
        /// </summary>
        public const string EraseLineFromCursor = $"{_ESC}{_CSI}0K";
        /// <summary>
        /// erase start of line to the cursor
        /// </summary>
        public const string EraseLineUntilCursor = $"{_ESC}{_CSI}1K";
        /// <summary>
        /// erase the entire line
        /// </summary>
        public const string EraseLine = $"{_ESC}{_CSI}2K";
        #endregion

        #region Colors / Graphics Mode
        /// <summary>
        /// Set graphics modes for cell, separated by semicolon (;).
        /// </summary>
        public static string SetGraphicsModes(params uint[] cells) => $"{_ESC}{_CSI}{string.Join(';', cells)}m";

        public const string Reset = $"{_ESC}{_CSI}0m";

        public const string BoldSet = $"{_ESC}{_CSI}1m";
        public const string BoldReset = $"{_ESC}{_CSI}22m";

        public const string DimSet = $"{_ESC}{_CSI}2m";
        public const string DimReset = $"{_ESC}{_CSI}22m";

        public const string ItalicSet = $"{_ESC}{_CSI}3m";
        public const string ItalicReset = $"{_ESC}{_CSI}23m";

        public const string UnderlineSet = $"{_ESC}{_CSI}4m";
        public const string UnderlineReset = $"{_ESC}{_CSI}24m";

        public const string BlinkingSet = $"{_ESC}{_CSI}5m";
        public const string BlinkingReset = $"{_ESC}{_CSI}25m";

        public const string InverseSet = $"{_ESC}{_CSI}7m";
        public const string InverseReset = $"{_ESC}{_CSI}27m";

        public const string HiddenSet = $"{_ESC}{_CSI}8m";
        public const string HiddenReset = $"{_ESC}{_CSI}28m";

        public const string StrikethroughSet = $"{_ESC}{_CSI}9m";
        public const string StrikethroughReset = $"{_ESC}{_CSI}29m";
        #endregion

        /// <exception cref="WindowsException"/>
        [SupportedOSPlatform("windows")]
        public static void EnableVirtualTerminalSequences() => ConsoleHandler.OutputFlags |= (uint)0x0004;

        /// <exception cref="WindowsException"/>
        [SupportedOSPlatform("windows")]
        public static void DisableVirtualTerminalSequences() => ConsoleHandler.OutputFlags &= ~(uint)0x0004;

        public static StringBuilder SetGraphics(StringBuilder builder, params uint[] modes)
        {
            builder.Append(ESC);
            builder.Append(CSI);
            builder.AppendJoin(';', modes);
            builder.Append('m');
            return builder;
        }

        public static StringBuilder SetForegroundColor(StringBuilder builder, System.Drawing.Color color)
        {
            builder.Append(ESC);
            builder.Append(CSI);
            builder.Append('3');
            builder.Append('8');
            builder.Append(';');
            builder.Append('2');
            builder.Append(';');
            builder.Append(color.R);
            builder.Append(';');
            builder.Append(color.G);
            builder.Append(';');
            builder.Append(color.B);
            builder.Append('m');
            return builder;
        }

        public static StringBuilder SetBackgroundColor(StringBuilder builder, System.Drawing.Color color)
        {
            builder.Append(ESC);
            builder.Append(CSI);
            builder.Append('4');
            builder.Append('8');
            builder.Append(';');
            builder.Append('2');
            builder.Append(';');
            builder.Append(color.R);
            builder.Append(';');
            builder.Append(color.G);
            builder.Append(';');
            builder.Append(color.B);
            builder.Append('m');
            return builder;
        }

        public static StringBuilder SetForegroundColor(StringBuilder builder, byte colorCode)
        {
            builder.Append(ESC);
            builder.Append(CSI);
            builder.Append('3');
            builder.Append('8');
            builder.Append(';');
            builder.Append('5');
            builder.Append(';');
            builder.Append(colorCode);
            return builder;
        }

        public static StringBuilder SetBackgroundColor(StringBuilder builder, byte colorCode)
        {
            builder.Append(ESC);
            builder.Append(CSI);
            builder.Append('4');
            builder.Append('8');
            builder.Append(';');
            builder.Append('5');
            builder.Append(';');
            builder.Append(colorCode);
            builder.Append('m');
            return builder;
        }
    }

}
