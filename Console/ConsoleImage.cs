using System.Buffers;
using System.Buffers.Text;
using System.Text;

namespace Win32.Console;

public readonly struct ConsoleImage
{
    public readonly short Width;
    public readonly short Height;

    readonly ConsoleChar[] Data;

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ConsoleChar this[int x, int y] => Data[x + (y * Width)];

    public ConsoleImage(ConsoleChar[] value, int width, int height)
    {
        Data = value;
        Width = (short)width;
        Height = (short)height;
    }

    public ConsoleImage(ConsoleChar[] value, int width)
    {
        Data = value;
        Width = (short)width;
        Height = (short)(value.Length / width);
    }

    ConsoleImage(BinaryReader reader)
    {
        Width = reader.ReadInt16();
        Height = reader.ReadInt16();
        int l = Width * Height;
        Data = new ConsoleChar[l];
        for (int i = 0; i < l; i++)
        {
#pragma warning disable IDE0017 // Simplify object initialization
            ConsoleChar c = new();
#pragma warning restore IDE0017
            c.Char = reader.ReadChar();
            c.Attributes = reader.ReadUInt16();
            Data[i] = c;
        }
    }

    void Serialize(BinaryWriter writer)
    {
        writer.Write((Int16)Width);
        writer.Write((Int16)Height);
        int l = Width * Height;
        for (int i = 0; i < l; i++)
        {
            writer.Write((Char)Data[i].Char);
            writer.Write((UInt16)Data[i].Attributes);
        }
    }

    public static ConsoleImage FromBytes(BinaryReader reader) => new(reader);
    public static ConsoleImage FromBytes(byte[] data)
    {
        using MemoryStream memoryStream = new(data, false);
        using BinaryReader reader = new(memoryStream);
        return new ConsoleImage(reader);
    }
    public static ConsoleImage FromBytes(ReadOnlySpan<byte> data)
    {
        using MemoryStream memoryStream = new(data.ToArray(), false);
        using BinaryReader reader = new(memoryStream);
        return new ConsoleImage(reader);
    }
    public static ConsoleImage FromBase64(string text)
    {
        byte[] data = Convert.FromBase64String(text);
        return ConsoleImage.FromBytes(data);
    }
    public static ConsoleImage FromBase64(ReadOnlySpan<byte> utf8)
    {
        int length = Base64.GetMaxDecodedFromUtf8Length(utf8.Length);
        Span<byte> buffer = new byte[length];
        OperationStatus status = Base64.DecodeFromUtf8(utf8, buffer, out _, out int bytesWritten);
        return status switch
        {
            OperationStatus.Done => ConsoleImage.FromBytes(buffer[..bytesWritten]),
            OperationStatus.InvalidData => throw new FormatException("Input Base64 string is not formatted correctly"),
            _ => throw new NotImplementedException(),
        };
    }

    public void ToBytes(BinaryWriter writer) => Serialize(writer);
    public byte[] ToBytes()
    {
        using MemoryStream memoryStream = new();
        using BinaryWriter writer = new(memoryStream);
        Serialize(writer);
        return memoryStream.ToArray();
    }
    public string ToBase64()
    {
        byte[] data = ToBytes();
        return Convert.ToBase64String(data);
    }
    public ReadOnlySpan<byte> ToBase64Utf8()
    {
        byte[] data = ToBytes();
        int length = Base64.GetMaxEncodedToUtf8Length(data.Length);
        Span<byte> buffer = new byte[length];
        OperationStatus status = Base64.EncodeToUtf8(data, buffer, out _, out int bytesWritten);
        return status switch
        {
            OperationStatus.Done => buffer[..bytesWritten],
            OperationStatus.InvalidData => throw new FormatException("Input Base64 string is not formatted correctly"),
            _ => throw new NotImplementedException(),
        };
    }

    public string ToCSharp()
    {
        ReadOnlySpan<byte> data = ToBytes();
        StringBuilder builder = new(data.Length * 4);
        if (true)
        {
            builder.Append($"public static readonly {nameof(ConsoleImage)} ImageData = {nameof(ConsoleImage)}.{nameof(FromBytes)}(\r\n    \"");
            int rowWidth = 0;
            for (int i = 0; i < data.Length; i++)
            {
                // if (rowWidth >= 32)
                // {
                //     builder.Append("\"u8");
                //     if (rowWidth < 38)
                //     { builder.Append(' ', 38 - rowWidth); }
                //     builder.Append("+\r\n    \"");
                //     rowWidth = 0;
                // }

                byte c = data[i];
                if (char.IsAsciiLetterOrDigit((char)c))
                {
                    builder.Append((char)c);
                    rowWidth++;
                }
                else
                {
                    switch (c)
                    {
                        case (byte)'\0':
                            builder.Append(@"\0");
                            rowWidth += 2;
                            break;
                        case (byte)'\n':
                            builder.Append(@"\n");
                            rowWidth += 2;
                            break;
                        case (byte)'\r':
                            builder.Append(@"\r");
                            rowWidth += 2;
                            break;
                        case (byte)'\v':
                            builder.Append(@"\v");
                            rowWidth += 2;
                            break;
                        case (byte)'\t':
                            builder.Append(@"\t");
                            rowWidth += 2;
                            break;
                        default:
#pragma warning disable CA1305
                            builder.Append($"\\x0{Convert.ToString(c, 16).PadLeft(2, '0')}");
#pragma warning restore CA1305
                            rowWidth += 5;
                            break;
                    }
                }
            }
            builder.Append("\"u8);\r\n");
        }
        else
        {
            builder.Append("public static readonly byte[] ImageData = new byte[] {\r\n");
            for (int i = 0; i < data.Length; i++)
            {
                if (i > 0 && i % 16 == 0)
                {
                    builder.Append("\r\n");
                }

#pragma warning disable CA1305
                builder.Append($"0x{Convert.ToString(data[i], 16)}, ");
#pragma warning restore CA1305
            }
            builder.Append("}\r\n");
        }
        return builder.ToString();
    }

    public ReadOnlySpan<ConsoleChar> AsSpan() => new(Data);

    public ConsoleImage Scale(float widthMultiplier, float heightMultiplier)
    {
        int newWidth = (int)(Width * widthMultiplier);
        int newHeight = (int)(Height * heightMultiplier);
        ConsoleChar[] newData = new ConsoleChar[newWidth * newHeight];

        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                newData[x + (y * newWidth)] = Data[(int)(x / widthMultiplier) + ((int)(y / heightMultiplier) * Width)];
            }
        }

        return new ConsoleImage(newData, newWidth, newHeight);
    }
}
