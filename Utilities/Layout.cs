﻿namespace Win32
{
    public static class Layout
    {
        public static RECT Center(SIZE size, RECT container)
        {
            LONG x = container.X + (container.Width / 2) - (size.Width / 2);
            LONG y = container.Y + (container.Height / 2) - (size.Height / 2);
            return new RECT(x, y, size.Width, size.Height);
        }

        public static SMALL_RECT Center(COORD size, SMALL_RECT container)
        {
            SHORT x = (SHORT)(container.X + (container.Width / 2) - (size.X / 2));
            SHORT y = (SHORT)(container.Y + (container.Height / 2) - (size.Y / 2));
            return new SMALL_RECT(x, y, size.X, size.Y);
        }
    }
}