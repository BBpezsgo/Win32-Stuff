using System.Drawing;
using System.Text;

namespace Win32
{
    public class AnsiBuilder
    {
        protected readonly StringBuilder Builder;

        public int Length
        {
            get => Builder.Length;
            set => Builder.Length = value;
        }

        Color currentFgColor;
        Color currentBgColor;
        bool currentBold;
        bool currentUnderline;
        bool currentItalics;

        public Color ForegroundColor
        {
            get => currentFgColor;
            set
            {
                if (currentFgColor == value && Builder.Length > 0) return;
                Ansi.SetForegroundColor(Builder, value);
                currentFgColor = value;
            }
        }

        public Color BackgroundColor
        {
            get => currentBgColor;
            set
            {
                if (currentBgColor == value && Builder.Length > 0) return;
                Ansi.SetBackgroundColor(Builder, value);
                currentBgColor = value;
            }
        }

        public bool Bold
        {
            get => currentBold;
            set
            {
                if (currentBold == value && Builder.Length > 0) return;
                Builder.Append(value ? Ansi.BoldSet : Ansi.BoldReset);
                currentBold = value;
            }
        }

        public bool Underline
        {
            get => currentUnderline;
            set
            {
                if (currentUnderline == value && Builder.Length > 0) return;
                Builder.Append(value ? Ansi.UnderlineSet : Ansi.UnderlineReset);
                currentUnderline = value;
            }
        }

        public bool Italics
        {
            get => currentItalics;
            set
            {
                if (currentItalics == value && Builder.Length > 0) return;
                Builder.Append(value ? Ansi.ItalicSet : Ansi.ItalicReset);
                currentItalics = value;
            }
        }

        public char this[int index]
        {
            get => Builder[index];
            set => Builder[index] = value;
        }

        public AnsiBuilder() : this(new StringBuilder()) { }
        public AnsiBuilder(int capacity) : this(new StringBuilder(capacity)) { }
        public AnsiBuilder(StringBuilder builder)
        {
            Builder = builder;
            currentFgColor = Color.Black;
            currentBgColor = Color.Black;
            currentBold = false;
            currentUnderline = false;
            currentItalics = false;
        }

        public static explicit operator StringBuilder(AnsiBuilder ansiBuilder) => ansiBuilder.Builder;
        public static explicit operator AnsiBuilder(StringBuilder stringBuilder) => new(stringBuilder);

        public override string ToString() => Builder.ToString();

        public void ResetStyle()
        {
            currentFgColor = Color.Black;
            currentBgColor = Color.Black;
            currentBold = false;
            currentUnderline = false;
            currentItalics = false;
            Builder.Append(Ansi.Reset);
        }

        public AnsiBuilder Clear()
        {
            Builder.Clear();
            return this;
        }

        public AnsiBuilder AppendLine()
        {
            Builder.AppendLine();
            return this;
        }
        public AnsiBuilder AppendLine(string? value)
        {
            Append(value);
            return AppendLine();
        }

        #region Append

        public AnsiBuilder Append(char value, int repeatCount)
        {
            Builder.Append(value, repeatCount);
            return this;
        }

        public AnsiBuilder Append(string? value)
        {
            Builder.Append(value);
            return this;
        }

        public AnsiBuilder Append(StringBuilder? value)
        {
            Builder.Append(value);
            return this;
        }

        public AnsiBuilder Append(char value)
        {
            Builder.Append(value);
            return this;
        }

        public AnsiBuilder Append(char[]? value)
        {
            Builder.Append(value);
            return this;
        }

        public AnsiBuilder Append(ReadOnlySpan<char> value)
        {
            Builder.Append(value);
            return this;
        }

        public AnsiBuilder Append(ReadOnlyMemory<char> value) => Append(value.Span);

        public AnsiBuilder Append(object? value)
        {
            if (value == null) return this;
            return Append(value.ToString());
        }

        #endregion
    }
}
