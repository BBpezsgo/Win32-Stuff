using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    [DebuggerDisplay($"{{{nameof(DebuggerDisplay)}(),nq}}")]
    public readonly struct Thread :
        IDisposable,
        IEquatable<Thread>,
        System.Numerics.IEqualityOperators<Thread, Thread, bool>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HANDLE _handle;

        Thread(HANDLE handle) => _handle = handle;

        public void Dispose() => _ = Kernel32.CloseHandle(_handle);

        public static explicit operator Thread(HANDLE handle) => new(handle);
        public static implicit operator HANDLE(Thread handle) => handle._handle;

        public static bool operator ==(Thread a, Thread b) => a.Equals(b);
        public static bool operator !=(Thread a, Thread b) => !a.Equals(b);

        /// <exception cref="WindowsException"/>
        public void WaitFor(uint timeout = uint.MaxValue)
        {
            DWORD result = Kernel32.WaitForSingleObject(_handle, timeout);
            if (result == 0xFFFFFFFF)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public void Terminate(DWORD exitCode)
        {
            if (Kernel32.TerminateThread(_handle, exitCode) == FALSE)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="HResultException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public string Description
        {
            get
            {
                WCHAR* description = null;
                HResult result = Kernel32.GetThreadDescription(_handle, &description);
                result.Throw();
                return new string(description);
            }
            set
            {
                fixed (WCHAR* description = value)
                {
                    HResult result = Kernel32.SetThreadDescription(_handle, description);
                    result.Throw();
                }
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public DWORD ProcessId
        {
            get
            {
                DWORD processId = Kernel32.GetProcessIdOfThread(_handle);
                if (processId == 0)
                { throw WindowsException.Get(); }
                return processId;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public DWORD Id
        {
            get
            {
                DWORD processId = Kernel32.GetThreadId(_handle);
                if (processId == 0)
                { throw WindowsException.Get(); }
                return processId;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public ProcessorNumber ThreadIdealProcessor
        {
            get
            {
                ProcessorNumber result = default;
                if (Kernel32.GetThreadIdealProcessorEx(_handle, &result) == FALSE)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public GroupAffinity ThreadGroupAffinity
        {
            get
            {
                GroupAffinity result = default;
                if (Kernel32.GetThreadGroupAffinity(_handle, &result) == FALSE)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        public static Thread Open(ThreadAccessRights accessRights, DWORD threadId)
        {
            HANDLE handle = Kernel32.OpenThread((DWORD)accessRights, FALSE, threadId);
            if (handle == HANDLE.Zero)
            { throw WindowsException.Get(); }
            return new Thread(handle);
        }

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        string DebuggerDisplay() => ToString();
        public override bool Equals(object? obj) => obj is Thread handle && Equals(handle);
        public bool Equals(Thread other) => _handle == other._handle;
        public override int GetHashCode() => _handle.GetHashCode();
    }
}
