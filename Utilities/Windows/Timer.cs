namespace Win32
{
    public readonly struct Timer : IDisposable
    {
        static readonly Dictionary<UINT_PTR, Timer> TimerIds = new();

        readonly UINT_PTR Id;
        readonly HWND Window;
        readonly Action Callback;

        /// <exception cref="WindowsException"/>
        /// <exception cref="GeneralException"/>
        unsafe public Timer(HWND window, uint timeoutMs, Action callback)
        {
            UINT_PTR id = GenerateId();
            UINT_PTR result = User32.SetTimer(window, id, timeoutMs, &TimerCallback);

            if (result == UINT_PTR.Zero)
            { throw WindowsException.Get(); }

            this.Id = id;
            this.Window = window;
            this.Callback = callback;

            TimerIds.Add(id, this);
        }

        /// <exception cref="GeneralException"/>
        static UINT_PTR GenerateId()
        {
            UINT_PTR result = 1;
            int endlessSafe = int.MaxValue - 1;
            while (TimerIds.ContainsKey(result))
            {
                result++;
                if (--endlessSafe <= 0)
                { throw new GeneralException($"Failed to generate timer id"); }
            }
            if (result == 0)
            { throw new GeneralException($"Failed to generate timer id"); }
            return result;
        }

        /// <exception cref="GeneralException"/>
        static void TimerCallback(HWND window, uint _1, UINT_PTR timerId, uint _2)
        {
            if (!TimerIds.TryGetValue(timerId, out Timer timer))
            { throw new GeneralException($"Timer with id {timerId} not found"); }
            timer.Callback?.Invoke();
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
