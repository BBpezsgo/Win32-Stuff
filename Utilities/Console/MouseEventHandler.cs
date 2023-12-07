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

        static COORD recordedPosition;
        static COORD leftPressedAt;

        public static COORD RecordedPosition => recordedPosition;
        public static COORD LeftPressedAt => leftPressedAt;

        public static bool IsPressed(MouseButton button) => Accumulated[(uint)button] || Stage1[(uint)button] || Stage2[(uint)button];
        public static bool IsHold(MouseButton button) => Stage2[(uint)button];
        public static bool IsDown(MouseButton button) => Stage1[(uint)button] && !Stage2[(uint)button] && !Stage3[(uint)button];
        public static bool IsUp(MouseButton button) => !Stage1[(uint)button] && !Stage2[(uint)button] && Stage3[(uint)button];

        public static void Feed(MouseEvent e)
        {
            Accumulated.states = e.ButtonState;
            recordedPosition = e.MousePosition;
        }

        public static void Tick()
        {
            if (Accumulated[(DWORD)MouseButton.Left] && !Stage1[(DWORD)MouseButton.Left])
            { leftPressedAt = recordedPosition; }

            Stage3 = Stage2;
            Stage2 = Stage1;
            Stage1 = Accumulated;
        }
    }
}
