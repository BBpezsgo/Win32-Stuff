using System.Globalization;

namespace Win32;

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
    public unsafe string FileName
    {
        get
        {
            const int maxLength = 128;
            fixed (WCHAR* moduleNamePtr = new string('\0', maxLength))
            {
                DWORD length = Kernel32.GetModuleFileNameExW(ProcessHandle, ModuleHandle, moduleNamePtr, maxLength);
                if (length == 0) throw WindowsException.Get();
                return new string(moduleNamePtr, 0, (int)length);
            }
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe string BaseName
    {
        get
        {
            const int maxLength = 128;
            fixed (WCHAR* moduleNamePtr = new string('\0', maxLength))
            {
                DWORD length = Kernel32.GetModuleBaseNameW(ProcessHandle, ModuleHandle, moduleNamePtr, maxLength);
                if (length == 0) throw WindowsException.Get();
                return new string(moduleNamePtr, 0, (int)length);
            }
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe ModuleInfo Info
    {
        get
        {
            if (Kernel32.GetModuleInformation(ProcessHandle, ModuleHandle, out ModuleInfo moduleInfo, (uint)sizeof(ModuleInfo)) == 0)
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
