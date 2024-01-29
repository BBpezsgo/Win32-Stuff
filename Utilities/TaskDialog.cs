namespace Win32
{
    public enum TaskDialogIcon : WORD
    {
        Warning = unchecked((WORD)(-1)),
        Error = unchecked((WORD)(-2)),
        Information = unchecked((WORD)(-3)),
        Shield = unchecked((WORD)(-4)),
    }

    [SupportedOSPlatform("windows")]
    public static class TaskDialog
    {
        public static unsafe int Show(string? windowTitle, string? mainInstruction, string? content, int buttons, TaskDialogIcon icon)
            => Show(windowTitle, mainInstruction, content, buttons, IntResource.MakeW((WORD)icon));
        public static unsafe int Show(string? windowTitle, string? mainInstruction, string? content, int buttons, char* icon)
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
