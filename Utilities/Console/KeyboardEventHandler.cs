namespace Win32
{
    public static partial class Keyboard
    {
        static readonly int[] Accumulated = new int[8];

        static int[] Stage1 = new int[8];
        static int[] Stage2 = new int[8];
        static int[] Stage3 = new int[8];

        /// <exception cref="KeyNotFoundException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyPressed(char key) => IsKeyPressed(AsciiToKey[key]);
        /// <param name="key">
        /// See <see cref="VirtualKeyCode"/> for values
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyPressed(ushort key) => BitUtils.GetBit(Accumulated, key) || BitUtils.GetBit(Stage1, key) || BitUtils.GetBit(Stage2, key);

        /// <exception cref="KeyNotFoundException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyHold(char key) => IsKeyHold(AsciiToKey[key]);
        /// <param name="key">
        /// See <see cref="VirtualKeyCode"/> for values
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyHold(ushort key) => BitUtils.GetBit(Stage2, key);

        /// <exception cref="KeyNotFoundException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyDown(char key) => IsKeyDown(AsciiToKey[key]);
        /// <param name="key">
        /// See <see cref="VirtualKeyCode"/> for values
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyDown(ushort key)
        {
            bool stage1 = BitUtils.GetBit(Stage1, key);
            bool stage2 = BitUtils.GetBit(Stage2, key);
            bool stage3 = BitUtils.GetBit(Stage3, key);
            return stage1 && !stage2 && !stage3;
        }

        /// <exception cref="KeyNotFoundException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyUp(char key) => IsKeyUp(AsciiToKey[key]);
        /// <param name="key">
        /// See <see cref="VirtualKeyCode"/> for values
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static bool IsKeyUp(ushort key)
        {
            bool stage1 = BitUtils.GetBit(Stage1, key);
            bool stage2 = BitUtils.GetBit(Stage2, key);
            bool stage3 = BitUtils.GetBit(Stage3, key);
            return !stage1 && !stage2 && stage3;
        }

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void Feed(KeyEvent e) => BitUtils.SetBit(Accumulated, e.VirtualKeyCode, e.IsDown != 0);

        public static void Tick()
        {
            int[] savedStage3 = Stage3;
            Stage3 = Stage2;
            Stage2 = Stage1;
            Stage1 = savedStage3;
            Buffer.BlockCopy(Accumulated, 0, Stage1, 0, 8 * sizeof(int));
        }
    }
}
