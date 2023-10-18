namespace Win32.Utilities
{
    public static class Mouse
    {
        public static POINT Position
        {
            get
            {
                if (User32.GetCursorPos(out POINT point) == 0)
                { throw WindowsException.Get(); }
                return point;
            }
            set
            {
                if (User32.SetCursorPos(value.X, value.Y) == 0)
                { throw WindowsException.Get(); }
            }
        }
    }
}