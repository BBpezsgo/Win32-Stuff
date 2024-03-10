namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public sealed class Win32Class : IEquatable<Win32Class?>
{
    readonly HINSTANCE ModuleHandle;
    public string Name { get; }

    Win32Class(string name, HWND moduleHandle)
    {
        Name = name;
        ModuleHandle = moduleHandle;
    }

    /// <exception cref="WindowsException"/>
    public static unsafe Win32Class Register(WNDCLASSEXW* info) => Win32Class.Register(info, out _);

    /// <exception cref="WindowsException"/>
    public static unsafe Win32Class Register(WNDCLASSEXW* info, out ushort id)
    {
        id = User32.RegisterClassExW(info);
        if (id == 0)
        { throw WindowsException.Get(); }
        return new Win32Class(new string(info->ClassName), info->Instance);
    }

    /// <exception cref="WindowsException"/>
    public unsafe void Unregister()
    {
        fixed (WCHAR* classNamePtr = Name)
        {
            if (User32.UnregisterClassW(classNamePtr, ModuleHandle) == 0)
            { throw WindowsException.Get(); }
        }
    }

    /// <exception cref="WindowsException"/>
    public unsafe WNDCLASSEXW GetInfo()
    {
        WNDCLASSEXW result = WNDCLASSEXW.Create();
        fixed (WCHAR* namePtr = Name)
        {
            if (User32.GetClassInfoExW(ModuleHandle, namePtr, &result) == 0)
            { throw WindowsException.Get(); }
        }
        return result;
    }

    /// <inheritdoc/>
    public override string ToString() => Name;
    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as Win32Class);
    /// <inheritdoc/>
    public bool Equals(Win32Class? other) =>
        other is not null &&
        string.Equals(Name, other.Name, StringComparison.Ordinal) &&
        ModuleHandle == other.ModuleHandle;
    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Name, ModuleHandle);
}
