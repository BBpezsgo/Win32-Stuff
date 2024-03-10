namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class WindowPropertiesContainer : IEquatable<WindowPropertiesContainer?>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    readonly HWND Handle;

    public WindowPropertiesContainer(HWND handle) => Handle = handle;

    /// <exception cref="WindowsException"/>
    public unsafe HANDLE this[string property]
    {
        get { fixed (WCHAR* propertyPtr = property) return this[propertyPtr]; }
        set { fixed (WCHAR* propertyPtr = property) this[propertyPtr] = value; }
    }

    /// <exception cref="WindowsException"/>
    public unsafe HANDLE this[WCHAR* property]
    {
        get
        {
            HANDLE value = User32.GetPropW(Handle, property);
            if (value == HANDLE.Zero)
            { throw new KeyNotFoundException($"The specified key (\"{new string(property)}\") not found in the collection"); }
            return value;
        }
        set
        {
            BOOL result = User32.SetPropW(Handle, property, value);
            if (result == FALSE)
            { throw WindowsException.Get(); }
        }
    }

    public unsafe bool Remove(string property)
    {
        HANDLE result;
        fixed (WCHAR* propertyPtr = property)
        { result = User32.RemovePropW(Handle, propertyPtr); }
        return result != HANDLE.Zero;
    }

    static unsafe BOOL ToDictionaryProc(HWND handle, WCHAR* property, HANDLE value, ULONG_PTR lParam)
    {
        GCHandle gcHandle = GCHandle.FromIntPtr((nint)lParam);
        if (!gcHandle.IsAllocated)
        { return FALSE; }
        object? obj = gcHandle.Target;
        if (obj == null)
        { return FALSE; }

        Dictionary<string, HANDLE> childList = (Dictionary<string, HANDLE>)obj;
        childList.Add(new string(property), value);

        return TRUE;
    }

    public unsafe Dictionary<string, HANDLE> ToDictionary()
    {
        Dictionary<string, HANDLE> result = new();
        GCHandle handle = GCHandle.Alloc(result, GCHandleType.Weak);
        _ = User32.EnumPropsExW(Handle, &ToDictionaryProc, GCHandle.ToIntPtr(handle));
        handle.Free();
        return result;
    }

    public unsafe bool TryGetValue(string property, out HANDLE value)
    {
        HANDLE result;
        fixed (WCHAR* propertyPtr = property)
        { result = User32.GetPropW(Handle, propertyPtr); }
        value = result;
        return value != HANDLE.Zero;
    }

    public unsafe bool Contains(string property)
    {
        HANDLE result;
        fixed (WCHAR* propertyPtr = property)
        { result = User32.GetPropW(Handle, propertyPtr); }
        return result != HANDLE.Zero;
    }

    static unsafe BOOL DelPropProc(
        HWND handle,
        WCHAR* property,
        HANDLE value)
    {
        _ = User32.RemovePropW(handle, property);
        return TRUE;
    }
    public unsafe void Clear() => _ = User32.EnumPropsW(Handle, &DelPropProc);

    public override bool Equals(object? obj) => Equals(obj as WindowPropertiesContainer);

    public bool Equals(WindowPropertiesContainer? other) => other is not null && Handle == other.Handle;

    public override int GetHashCode() => Handle.GetHashCode();
}
