using System.Numerics;

namespace Win32
{
    public partial interface IRenderer<T>
    {
        public short Width { get; }
        public short Height { get; }

        public SmallSize Size => new(Width, Height);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref T this[int i] { get; }

        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref T this[int x, int y] => ref this[(y * Width) + x];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref T this[float x, float y] => ref this[((int)MathF.Round(y) * Width) + (int)MathF.Round(x)];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref T this[COORD p] => ref this[(p.Y * Width) + p.X];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref T this[POINT p] => ref this[(p.Y * Width) + p.X];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref T this[Vector2 p] => ref this[((int)MathF.Round(p.Y) * Width) + (int)MathF.Round(p.X)];

        public bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
        public bool IsVisible(float x, float y) => IsVisible((int)MathF.Round(x), (int)MathF.Round(y));
        public bool IsVisible(COORD position) => IsVisible(position.X, position.Y);
        public bool IsVisible(POINT position) => IsVisible(position.X, position.Y);
        public bool IsVisible(Vector2 position) => IsVisible((int)MathF.Round(position.X), (int)MathF.Round(position.Y));

        public void Render();

        public void ClearBuffer();

        public void RefreshBufferSize();

        public void Clear(SMALL_RECT rect);
        public void Fill(T value);
        public void Fill(SMALL_RECT rect, T value);

        public void Image(COORD position, T[] image, int width, int height)
        {
            for (int y_ = 0; y_ < height; y_++)
            {
                for (int x_ = 0; x_ < width; x_++)
                {
                    Coord p = new Coord(x_, y_) + position;
                    if (!IsVisible(p)) continue;
                    this[p] = image[x_ + (y_ * width)];
                }
            }
        }

        public void Image(COORD position, T[] image, int width, int height, int widthRatio, int heightRatio)
        {
            for (int y_ = 0; y_ < height * heightRatio; y_++)
            {
                for (int x_ = 0; x_ < width * widthRatio; x_++)
                {
                    Coord p = new Coord(x_, y_) + position;
                    if (!IsVisible(p)) continue;
                    this[p] = image[(x_ / widthRatio) + ((y_ / heightRatio) * width)];
                }
            }
        }
    }
}
