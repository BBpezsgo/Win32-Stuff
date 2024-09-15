namespace Win32.Console;

public delegate void ConsoleEvent<T>(T e);

public static class ConsoleListener
{
    public static event ConsoleEvent<MouseEvent>? MouseEvent;
    public static event ConsoleEvent<KeyEvent>? KeyEvent;
    public static event ConsoleEvent<WindowBufferSizeEvent>? WindowBufferSizeEvent;

    static bool Run;
    static HANDLE Handle;

    static COORD _lastSize;

    const int MaxRecordReads = 1;

    /// <exception cref="WindowsException"/>
    /// <exception cref="OutOfMemoryException"/>
    public static void Start()
    {
        if (Run) return;

        Run = true;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Handle = Kernel32.GetStdHandle(StdHandle.Input);

            if (Handle == Kernel32.InvalidHandle)
            { throw WindowsException.Get(); }
        }
        else
        {
            _lastSize = new COORD(System.Console.WindowWidth, System.Console.WindowHeight);
        }

        new System.Threading.Thread(ThreadJob) { Name = "ConsoleListener" }.Start();
    }

    /// <exception cref="WindowsException"/>
    static unsafe void ThreadJob()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            InputEvent[] records = new InputEvent[MaxRecordReads];

            while (Run)
            {
                uint numRead = 0;

                Array.Clear(records);

                fixed (InputEvent* recordsPtr = records)
                {
                    if (Kernel32.ReadConsoleInputW(Handle, recordsPtr, MaxRecordReads, out numRead) == 0)
                    { throw WindowsException.Get(); }
                }

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
                fixed (InputEvent* recordsPtr = records)
                {
                    _ = Kernel32.WriteConsoleInputW(Handle, recordsPtr, 1, out numWritten);
                }
                System.Console.CursorVisible = true;
            }
        }
        else
        {
            while (Run)
            {
                ConsoleKeyInfo key = System.Console.ReadKey();
                if (!Run) break;

                COORD size = new(System.Console.WindowWidth, System.Console.WindowHeight);

                if (size != _lastSize)
                {
                    _lastSize = size;
                    WindowBufferSizeEvent?.Invoke(new WindowBufferSizeEvent(size));
                }

                KeyEvent?.Invoke((KeyEvent)key);
            }
        }
    }

    public static void Stop() => Run = false;
}
