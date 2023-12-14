using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public struct ModuleEntry :
        IEquatable<ModuleEntry>,
        System.Numerics.IEqualityOperators<ModuleEntry, ModuleEntry, bool>
    {
        const int MAX_MODULE_NAME32 = 255;
        const int MAX_PATH = 260;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly DWORD StructSize;
        /// <summary>
        /// This member is no longer used, and is always set to one.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly DWORD ModuleId;
        /// <summary>
        /// The identifier of the process whose modules are to be examined.
        /// </summary>
        public readonly DWORD ProcessId;
        /// <summary>
        /// The load count of the module, which is not generally meaningful, and usually equal to 0xFFFF.
        /// </summary>
        public readonly DWORD LoadCount;
        /// <summary>
        /// The load count of the module (same as LoadCount), which is not generally meaningful,
        /// and usually equal to 0xFFFF.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly DWORD ProcCountUsage;
        /// <summary>
        /// The base address of the module in the context of the owning process.
        /// </summary>
        unsafe public readonly BYTE* ModuleBaseAddress;
        /// <summary>
        /// The size of the module, in bytes.
        /// </summary>
        public readonly DWORD ModuleSize;
        /// <summary>
        /// A handle to the module in the context of the owning process.
        /// </summary>
        public readonly HMODULE ModuleHandle;
        unsafe public fixed WCHAR ModulePtr[MAX_MODULE_NAME32 + 1];
        unsafe public fixed WCHAR ExePathPtr[MAX_PATH];

        unsafe public string Module
        {
            get
            {
                fixed (WCHAR* modulePtr = ModulePtr)
                { return new string(modulePtr); }
            }
        }

        unsafe public string ExePath
        {
            get
            {
                fixed (WCHAR* exePathPtr = ExePathPtr)
                { return new string(exePathPtr); }
            }
        }

        ModuleEntry(DWORD structSize) : this() => StructSize = structSize;

        public static unsafe ModuleEntry Create() => new((DWORD)sizeof(ModuleEntry));

        public override readonly string ToString() => "0x" + ModuleHandle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        string GetDebuggerDisplay() => $"{Module} ({ToString()})";

        public override readonly bool Equals(object? obj) => obj is ModuleEntry other && Equals(other);
        public readonly bool Equals(ModuleEntry other) =>
            ProcessId == other.ProcessId &&
            ModuleHandle == other.ModuleHandle;
        public override readonly int GetHashCode() => HashCode.Combine(ProcessId, ModuleHandle);

        public static bool operator ==(ModuleEntry left, ModuleEntry right) => left.Equals(right);
        public static bool operator !=(ModuleEntry left, ModuleEntry right) => !left.Equals(right);
    }
}
