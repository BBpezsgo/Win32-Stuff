namespace Win32.Forms;

public enum TaskDialogIcon : WORD
{
    Shield = unchecked((WORD)(-4)),
    Information = unchecked((WORD)(-3)),
    Error = unchecked((WORD)(-2)),
    Warning = unchecked((WORD)(-1)),
}

[SupportedOSPlatform("windows")]
public static class TaskDialog
{
    /// <exception cref="HResultException"/>
    public static unsafe int Show(string? windowTitle, string? mainInstruction, string? content, TaskDialogButtons buttons, TaskDialogIcon icon)
        => Show(0, windowTitle, mainInstruction, content, buttons, IntResource.MakeW((WORD)icon));

    /// <exception cref="HResultException"/>
    public static unsafe int Show(string? windowTitle, string? mainInstruction, string? content, TaskDialogButtons buttons = TaskDialogButtons.Ok, char* icon = null)
        => Show(0, windowTitle, mainInstruction, content, buttons, icon);

    /// <exception cref="HResultException"/>
    public static unsafe int Show(HWND owner, string? windowTitle, string? mainInstruction, string? content, TaskDialogButtons buttons, TaskDialogIcon icon)
        => Show(owner, windowTitle, mainInstruction, content, buttons, IntResource.MakeW((WORD)icon));

    /// <exception cref="HResultException"/>
    public static unsafe int Show(HWND owner, string? windowTitle, string? mainInstruction, string? content, TaskDialogButtons buttons = TaskDialogButtons.Ok, char* icon = null)
    {
        int nButtonPressed = default;
        HResult result;
        HINSTANCE hInstance = System.Diagnostics.Process.GetCurrentProcess().Handle;

        fixed (WCHAR* windowTitlePtr = windowTitle)
        fixed (WCHAR* mainInstructionPtr = mainInstruction)
        fixed (WCHAR* contentPtr = content)
        {
            result = Comctl32.TaskDialog(
                owner,
                hInstance,
                windowTitlePtr,
                mainInstructionPtr,
                contentPtr,
                (int)buttons,
                icon,
                out nButtonPressed);
        }
        result.Throw();
        return nButtonPressed;
    }
}
