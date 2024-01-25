namespace Win32
{
    public delegate void ConsoleEvent<T>(T e);

    public static class ConsoleListener
    {
        public static event ConsoleEvent<MouseEvent>? MouseEvent;
        public static event ConsoleEvent<KeyEvent>? KeyEvent;
        public static event ConsoleEvent<WindowBufferSizeEvent>? WindowBufferSizeEvent;

        static bool Run;
        static HANDLE Handle;

        const int MaxRecordReads = 1;

        /// <exception cref="WindowsException"/>
        /// <exception cref="OutOfMemoryException"/>
        [SupportedOSPlatform("windows")]
        public static void Start()
        {
            if (Run) return;

            Run = true;
            Handle = Kernel32.GetStdHandle(StdHandle.STD_INPUT_HANDLE);

            if (Handle == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(); }

            new System.Threading.Thread(ThreadJob) { Name = "ConsoleListener" }.Start();
        }

        /// <exception cref="WindowsException"/>
        [SupportedOSPlatform("windows")]
        static void ThreadJob()
        {
            InputEvent[] records = new InputEvent[MaxRecordReads];

            while (Run)
            {
                uint numRead = 0;

                Array.Clear(records);

                if (Kernel32.ReadConsoleInput(Handle, records, MaxRecordReads, ref numRead) == 0)
                { throw WindowsException.Get(); }

                if (!Run) break;

                for (int i = 0; i < numRead; i++)
                {
                    switch (records[i].EventType)
                    {
                        case EventType.Mouse:
                            MouseEvent?.Invoke(records[i].MouseEvent);
                            break;
                        case EventType.Key:
                            KeyEvent?.Invoke(records[i].KeyEvent);
                            break;
                        case EventType.WindowBufferSize:
                            WindowBufferSizeEvent?.Invoke(records[i].WindowBufferSizeEvent);
                            break;
                    }
                }
            }

            {
                uint numWritten = 0;
                _ = Kernel32.WriteConsoleInput(Handle, records, 1, ref numWritten);
                Console.CursorVisible = true;
            }
        }

        public static void Stop() => Run = false;
    }
}
