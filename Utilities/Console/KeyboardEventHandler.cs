namespace Win32
{
    public static partial class Keyboard
    {
        static readonly int[] Accumulated = new int[8];
        static readonly int[] ClearAccumulated = new int[8];

        static int[] Pressing = new int[8];
        static int[] Holding = new int[8];
        static int[] Releasing = new int[8];
        static readonly int[] Active = new int[8];

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyPressed(char key) => IsKeyPressed(ToVK(key).Key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyPressed(VirtualKeyCode key) =>
            BitUtils.GetBit(Accumulated, (int)key) ||
            BitUtils.GetBit(Pressing, (int)key) ||
            BitUtils.GetBit(Holding, (int)key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyHold(char key) => IsKeyHold(ToVK(key).Key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyHold(VirtualKeyCode key) => BitUtils.GetBit(Holding, (int)key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyDown(char key) => IsKeyDown(ToVK(key).Key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyDown(VirtualKeyCode key) =>
            BitUtils.GetBit(Pressing, (int)key) &&
            !BitUtils.GetBit(Holding, (int)key) &&
            !BitUtils.GetBit(Releasing, (int)key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyUp(char key) => IsKeyUp(ToVK(key).Key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyUp(VirtualKeyCode key) =>
            !BitUtils.GetBit(Pressing, (int)key) &&
            !BitUtils.GetBit(Holding, (int)key) &&
            BitUtils.GetBit(Releasing, (int)key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsActive(char key) => IsActive(ToVK(key).Key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsActive(VirtualKeyCode key) => BitUtils.GetBit(Active, (int)key);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void Feed(KeyEvent e)
        {
            BitUtils.SetBit(Accumulated, (int)e.VirtualKeyCode, e.IsDown != 0);
            BitUtils.SetBit(ClearAccumulated, (int)e.VirtualKeyCode, e.IsDown != 0);
        }

        public static void Tick()
        {
            int[] savedStage3 = Releasing;
            Releasing = Holding;
            Holding = Pressing;
            Pressing = savedStage3;
            Buffer.BlockCopy(Accumulated, 0, Pressing, 0, 8 * sizeof(int));

            Buffer.BlockCopy(ClearAccumulated, 0, Active, 0, 8 * sizeof(int));
            Array.Clear(ClearAccumulated);
        }
    }
}
