using System.Diagnostics;
using System.Globalization;

#pragma warning disable CA1716 // Identifiers should not match keywords

namespace Win32
{
    [SupportedOSPlatform("windows")]
    [DebuggerDisplay($"{{{nameof(DebuggerDisplay)}(),nq}}")]
    public readonly struct Module :
        IEquatable<Module>,
        System.Numerics.IEqualityOperators<Module, Module, bool>
    {
        readonly HANDLE ProcessHandle;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HMODULE ModuleHandle;

        internal Module(HANDLE processHandle, HMODULE moduleHandle)
        {
            ProcessHandle = processHandle;
            ModuleHandle = moduleHandle;
        }

        public static implicit operator HMODULE(Module module) => module.ModuleHandle;

        public static bool operator ==(Module left, Module right) => left.Equals(right);
        public static bool operator !=(Module left, Module right) => !left.Equals(right);

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public string FileName
        {
            get
            {
                const int MaxLength = 128;
                fixed (WCHAR* moduleNamePtr = new string('\0', MaxLength))
                {
                    DWORD length = Kernel32.GetModuleFileNameExW(ProcessHandle, ModuleHandle, moduleNamePtr, MaxLength);
                    if (length == 0) throw WindowsException.Get();
                    return new string(moduleNamePtr, 0, (int)length);
                }
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public string BaseName
        {
            get
            {
                const int MaxLength = 128;
                fixed (WCHAR* moduleNamePtr = new string('\0', MaxLength))
                {
                    DWORD length = Kernel32.GetModuleBaseNameW(ProcessHandle, ModuleHandle, moduleNamePtr, MaxLength);
                    if (length == 0) throw WindowsException.Get();
                    return new string(moduleNamePtr, 0, (int)length);
                }
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public ModuleInfo Info
        {
            get
            {
                ModuleInfo moduleInfo = default;
                if (Kernel32.GetModuleInformation(ProcessHandle, ModuleHandle, &moduleInfo, (uint)sizeof(ModuleInfo)) == 0)
                { throw WindowsException.Get(); }
                return moduleInfo;
            }
        }

        public override bool Equals(object? obj) => obj is Module module && Equals(module);
        public bool Equals(Module other) =>
            ProcessHandle == other.ProcessHandle &&
            ModuleHandle == other.ModuleHandle;
        public override int GetHashCode() => HashCode.Combine(ProcessHandle, ModuleHandle);
        public override string ToString() => "0x" + ModuleHandle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        /// <exception cref="WindowsException"/>
        string DebuggerDisplay() => $"{BaseName} ({ToString()})";
    }
}
