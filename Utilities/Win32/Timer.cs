namespace Win32.Utilities
{
    public readonly struct Timer : IDisposable
    {
        static readonly Dictionary<UINT_PTR, Timer> TimerIds = new();

        readonly UINT_PTR Id;
        readonly HWND Window;
        readonly Action Callback;

        Timer(UINT_PTR id, HWND window, Action callback)
        {
            Id = id;
            Window = window;
            Callback = callback;
        }

        /// <exception cref="NotWindowsException"/>
        static UINT_PTR GenerateId()
        {
            uint result = 1;
            int endlessSafe = int.MaxValue - 1;
            while (TimerIds.ContainsKey((UINT_PTR)result))
            {
                result++;
                if (--endlessSafe <= 0)
                { throw new NotWindowsException($"Failed to generate timer id"); }
            }
            if (result == 0)
            { throw new NotWindowsException($"Failed to generate timer id"); }
            return (UINT_PTR)result;
        }

        /// <exception cref="NotWindowsException"/>
        static void TimerCallback(HWND window, uint _1, UINT_PTR timerId, uint _2)
        {
            if (!TimerIds.TryGetValue(timerId, out Timer timer))
            { throw new NotWindowsException($"Timer with id {timerId} not found"); }
            timer.Callback?.Invoke();
        }

        /// <exception cref="WindowsException"/>
        unsafe public static Timer Create(HWND window, uint timeoutMs, Action callback)
        {
            UINT_PTR id = GenerateId();
            UINT_PTR result = User32.SetTimer(window, id, timeoutMs, &TimerCallback);
            if (result == UINT_PTR.Zero)
            { throw WindowsException.Get(); }
            Timer timer = new(id, window, callback);
            TimerIds.Add(id, timer);
            return timer;
        }

        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            if (User32.KillTimer(Window, Id) == 0)
            { throw WindowsException.Get(); }
            TimerIds.Remove(Id);
        }
    }
}
