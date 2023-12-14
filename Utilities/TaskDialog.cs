namespace Win32
{
    public enum TaskDialogIcon : int
    {
        WARNING_ICON = -1,
        ERROR_ICON = -2,
        INFORMATION_ICON = -3,
        SHIELD_ICON = -4,
    }

    public static class TaskDialog
    {
        unsafe public static int Show(string? windowTitle, string? mainInstruction, string? content, int buttons, TaskDialogIcon icon)
            => Show(windowTitle, mainInstruction, content, buttons, Macros.MAKEINTRESOURCEW(unchecked((WORD)icon)));
        unsafe public static int Show(string? windowTitle, string? mainInstruction, string? content, int buttons, char* icon)
        {
            int nButtonPressed = default;
            HResult result;
            fixed (WCHAR* windowTitlePtr = windowTitle)
            fixed (WCHAR* mainInstructionPtr = mainInstruction)
            fixed (WCHAR* contentPtr = content)
            {
                result = Comctl32.TaskDialog(
                    HWND.Zero,
                    HINSTANCE.Zero,
                    windowTitlePtr,
                    mainInstructionPtr,
                    contentPtr,
                    buttons,
                    icon,
                    &nButtonPressed);
            }
            return nButtonPressed;
        }
    }
}
