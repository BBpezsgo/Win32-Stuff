namespace Win32
{
    public static class Layout
    {
        public static RECT Center(SIZE size, RECT container)
        {
            int x = container.X + (container.Width / 2) - (size.Width / 2);
            int y = container.Y + (container.Height / 2) - (size.Height / 2);
            return new RECT(x, y, size.Width, size.Height);
        }
    }
}
