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
        readonly HANDLE Handle;

        Thread(HANDLE handle) => Handle = handle;

        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            if (Kernel32.CloseHandle(Handle) == FALSE)
            { throw WindowsException.Get(); }
        }

        public static explicit operator Thread(HANDLE handle) => new(handle);
        public static implicit operator HANDLE(Thread handle) => handle.Handle;

        public static bool operator ==(Thread a, Thread b) => a.Equals(b);
        public static bool operator !=(Thread a, Thread b) => !a.Equals(b);

        /// <exception cref="WindowsException"/>
        public void WaitFor(uint timeout = uint.MaxValue)
        {
            DWORD result = Kernel32.WaitForSingleObject(Handle, timeout);
            if (result == 0xFFFFFFFF)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public void Terminate(DWORD exitCode)
        {
            if (Kernel32.TerminateThread(Handle, exitCode) == FALSE)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="HResultException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe string Description
        {
            get
            {
                WCHAR* description = null;
                HResult result = Kernel32.GetThreadDescription(Handle, &description);
                result.Throw();
                return new string(description);
            }
            set
            {
                fixed (WCHAR* description = value)
                {
                    HResult result = Kernel32.SetThreadDescription(Handle, description);
                    result.Throw();
                }
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe DWORD ProcessId
        {
            get
            {
                DWORD processId = Kernel32.GetProcessIdOfThread(Handle);
                if (processId == 0)
                { throw WindowsException.Get(); }
                return processId;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe DWORD Id
        {
            get
            {
                DWORD processId = Kernel32.GetThreadId(Handle);
                if (processId == 0)
                { throw WindowsException.Get(); }
                return processId;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe ProcessorNumber ThreadIdealProcessor
        {
            get
            {
                ProcessorNumber result = default;
                if (Kernel32.GetThreadIdealProcessorEx(Handle, &result) == FALSE)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe GroupAffinity ThreadGroupAffinity
        {
            get
            {
                GroupAffinity result = default;
                if (Kernel32.GetThreadGroupAffinity(Handle, &result) == FALSE)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe DWORD ExitCode
        {
            get
            {
                DWORD exitCode = default;
                if (Kernel32.GetExitCodeThread(Handle, &exitCode) == FALSE)
                { throw WindowsException.Get(); }
                return exitCode;
            }
        }

        public static Thread Open(ThreadAccessRights accessRights, DWORD threadId)
        {
            HANDLE handle = Kernel32.OpenThread((DWORD)accessRights, FALSE, threadId);
            if (handle == HANDLE.Zero)
            { throw WindowsException.Get(); }
            return new Thread(handle);
        }

        public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        string DebuggerDisplay() => ToString();
        public override bool Equals(object? obj) => obj is Thread handle && Equals(handle);
        public bool Equals(Thread other) => Handle == other.Handle;
        public override int GetHashCode() => Handle.GetHashCode();
    }
}
