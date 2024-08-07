﻿namespace Win32.Console;

public delegate void ConsoleEvent<T>(T e);

public static class ConsoleListener
{
#pragma warning disable CA1003 // Use generic event handler instances
    public static event ConsoleEvent<MouseEvent>? MouseEvent;
    public static event ConsoleEvent<KeyEvent>? KeyEvent;
    public static event ConsoleEvent<WindowBufferSizeEvent>? WindowBufferSizeEvent;
#pragma warning restore CA1003 // Use generic event handler instances

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
        Handle = Kernel32.GetStdHandle(StdHandle.Input);

        if (Handle == Kernel32.InvalidHandle)
        { throw WindowsException.Get(); }

        new System.Threading.Thread(ThreadJob) { Name = "ConsoleListener" }.Start();
    }

    /// <exception cref="WindowsException"/>
    [SupportedOSPlatform("windows")]
    static unsafe void ThreadJob()
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

    public static void Stop() => Run = false;
}
