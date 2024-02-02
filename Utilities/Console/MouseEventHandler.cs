using System.Diagnostics;

namespace Win32
{
    public static partial class Mouse
    {
        struct CompactMouse
        {
            public uint states;

            public bool this[DWORD button]
            {
                readonly get => (states & button) != 0;
                set
                {
                    states ^= states & button;
                    states |= button;
                }
            }

            public static explicit operator uint(CompactMouse v) => v.states;
            public static explicit operator CompactMouse(uint v) => new() { states = v };
        }

        static CompactMouse Accumulated;

        static CompactMouse Stage1;
        static CompactMouse Stage2;
        static CompactMouse Stage3;

        static COORD recordedConsolePosition;
        static COORD leftPressedAt;

        static int scroll;
        static int scrollDelta;

        static bool wasUsed;

        public static COORD RecordedConsolePosition => recordedConsolePosition;
        public static COORD LeftPressedAt => leftPressedAt;
        public static bool WasUsed => wasUsed;
        public static int ScrollDelta => scrollDelta;

        public static bool IsPressed(MouseButton button) => Accumulated[(uint)button] || Stage1[(uint)button] || Stage2[(uint)button];
        public static bool IsHold(MouseButton button) => Stage2[(uint)button];
        public static bool IsDown(MouseButton button) => Stage1[(uint)button] && !Stage2[(uint)button] && !Stage3[(uint)button];
        public static bool IsUp(MouseButton button) => !Stage1[(uint)button] && !Stage2[(uint)button] && Stage3[(uint)button];

        public static void Use() => wasUsed = true;

        public static void Feed(MouseEvent e)
        {
            Accumulated.states = e.ButtonState;
            recordedConsolePosition = e.MousePosition;

            if (e.EventFlags == MouseEventFlags.MouseWheeled)
            {
                if (e.Scroll > 0)
                { scroll++; }
                else
                { scroll--; }
            }
        }

        public static void Tick()
        {
            wasUsed = false;

            if (Accumulated[(DWORD)MouseButton.Left] && !Stage1[(DWORD)MouseButton.Left])
            { leftPressedAt = recordedConsolePosition; }

            Stage3 = Stage2;
            Stage2 = Stage1;
            Stage1 = Accumulated;

            scrollDelta = scroll;
            scroll = 0;
        }
    }
}
