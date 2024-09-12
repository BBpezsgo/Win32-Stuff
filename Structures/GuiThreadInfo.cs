namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public readonly struct GuiThreadInfo
{
    readonly DWORD StructSize;

    public readonly DWORD Flags;
    public readonly HWND ActiveHandle;
    public readonly HWND FocusHandle;
    public readonly HWND CaptureHandle;
    public readonly HWND MenuOwnerHandle;
    public readonly HWND MoveSizeHandle;
    public readonly HWND CaretHandle;
    public readonly RECT Caret;

    GuiThreadInfo(DWORD structSize) : this() => this.StructSize = structSize;

    public static unsafe GuiThreadInfo Create() => new((DWORD)sizeof(GuiThreadInfo));
}
