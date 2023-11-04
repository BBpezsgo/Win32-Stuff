namespace Win32
{
    public static partial class Mouse
    {
        struct CompactMouse
        {
            public uint states;

            public bool this[uint button]
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

        public static bool IsPressed(uint key) => Accumulated[key] || Stage1[key] || Stage2[key];
        public static bool IsHold(uint key) => Stage2[key];
        public static bool IsDown(uint key)
        {
            bool stage1 = Stage1[key];
            bool stage2 = Stage2[key];
            bool stage3 = Stage3[key];
            return stage1 && !stage2 && !stage3;
        }
        public static bool IsUp(uint key)
        {
            bool stage1 = Stage1[key];
            bool stage2 = Stage2[key];
            bool stage3 = Stage3[key];
            return !stage1 && !stage2 && stage3;
        }

        public static void Feed(MouseEvent e)
        {
            Accumulated.states = e.ButtonState;
            recordedPosition = e.MousePosition;
        }

        public static void Tick()
        {
            if (Accumulated[MouseButton.Left] && !Stage1[MouseButton.Left])
            { leftPressedAt = recordedPosition; }

            Stage3 = Stage2;
            Stage2 = Stage1;
            Stage1 = Accumulated;
        }
    }
}
