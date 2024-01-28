using System.Buffers;
using System.Buffers.Text;

namespace Win32
{
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
            writer.Write(Width);
            writer.Write(Height);
            int l = Width * Height;
            for (int i = 0; i < l; i++)
            {
                writer.Write(Data[i].Char);
                writer.Write(Data[i].Attributes);
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
                OperationStatus.InvalidData => throw new FormatException($"Input Base64 string is not formatted correctly"),
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
                OperationStatus.InvalidData => throw new FormatException($"Input Base64 string is not formatted correctly"),
                _ => throw new NotImplementedException(),
            };
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
}
