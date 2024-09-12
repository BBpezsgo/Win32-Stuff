namespace Win32.Forms;

/// <summary>
/// Common Controls Library
/// </summary>
[SupportedOSPlatform("windows")]
public static class Comctl32
{
    /// <summary>
    /// The <c>TaskDialog</c> function creates, displays, and operates a task dialog.
    /// The task dialog contains application-defined message text and title,
    /// icons, and any combination of predefined push buttons.
    /// This function does not support the registration of a callback
    /// function to receive notifications.
    /// </summary>
    /// <param name="hwndOwner">
    /// Handle to the owner window of the task dialog to be created.
    /// If this parameter is <c>NULL</c>, the task dialog has no owner window.
    /// </param>
    /// <param name="hInstance">
    /// Handle to the module that contains the icon resource identified
    /// by the <paramref name="pszIcon"/> member, and the string resources identified
    /// by the <paramref name="pszWindowTitle"/> and <paramref name="pszMainInstruction"/> members.
    /// If this parameter is <c>NULL</c>, <paramref name="pszIcon"/> must be <c>NULL</c> or a
    /// pointer to a null-terminated, Unicode string that contains
    /// a system resource identifier.
    /// </param>
    /// <param name="pszWindowTitle">
    /// Pointer to the string to be used for the task dialog title.
    /// This parameter is a null-terminated, Unicode string that
    /// contains either text, or an integer resource identifier
    /// passed through the <see cref="IntResource.MakeW"/> macro. If this parameter
    /// is <c>NULL</c>, the filename of the executable program is used.
    /// </param>
    /// <param name="pszMainInstruction">
    /// Pointer to the string to be used for the main instruction.
    /// This parameter is a null-terminated, Unicode string that
    /// contains either text, or an integer resource identifier
    /// passed through the <see cref="IntResource.MakeW"/> macro. This parameter
    /// can be <c>NULL</c> if no main instruction is wanted.
    /// </param>
    /// <param name="pszContent">
    /// Pointer to a string used for additional text that appears
    /// below the main instruction, in a smaller font.
    /// This parameter is a null-terminated, Unicode string
    /// that contains either text, or an integer resource
    /// identifier passed through the <see cref="IntResource.MakeW"/> macro.
    /// Can be <c>NULL</c> if no additional text is wanted.
    /// </param>
    /// <param name="dwCommonButtons">
    /// <para>
    /// Specifies the push buttons displayed in the dialog box.
    /// </para>
    /// <para>
    /// If no buttons are specified, the dialog box will contain the OK button by default.
    /// </para>
    /// </param>
    /// <param name="pszIcon">
    /// Pointer to a string that identifies the icon to display in the task dialog.
    /// </param>
    /// <param name="pnButton">
    /// When this function returns, contains a pointer to an integer
    /// location that receives one of the following values:
    /// <list type="table">
    /// <item>
    /// <term>0</term>
    /// <description>
    /// Function call failed. Refer to return value for more information.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="DialogBoxCommand.IDCANCEL"/></term>
    /// <description>
    /// Cancel button was selected, Alt-F4 was pressed,
    /// Escape was pressed or the user clicked on the close window button.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="DialogBoxCommand.IDNO"/></term>
    /// <description>
    /// No button was selected.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="DialogBoxCommand.IDOK"/></term>
    /// <description>
    /// OK button was selected.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="DialogBoxCommand.IDRETRY"/></term>
    /// <description>
    /// Retry button was selected.
    /// </description>
    /// </item>
    /// <item>
    /// <term><see cref="DialogBoxCommand.IDYES"/></term>
    /// <description>
    /// Yes button was selected.
    /// </description>
    /// </item>
    /// </list>
    /// If this value is <c>NULL</c>, no value is returned.
    /// </param>
    /// <returns>
    /// <para>
    /// This function can return one of these values.
    /// <list type="table">
    /// <item><term>S_OK</term><description>The operation completed successfully.</description></item>
    /// <item><term>E_OUTOFMEMORY</term><description>There is insufficient memory to complete the operation.</description></item>
    /// <item><term>E_INVALIDARG</term><description>One or more arguments are not valid.</description></item>
    /// <item><term>E_FAIL</term><description>The operation failed.</description></item>
    /// </list>
    /// </para>
    /// </returns>
    [DllImport("Comctl32.dll", SetLastError = true)]
    public static extern unsafe HRESULT TaskDialog(
      HWND hwndOwner,
      HINSTANCE hInstance,
      WCHAR* pszWindowTitle,
      WCHAR* pszMainInstruction,
      WCHAR* pszContent,
      int dwCommonButtons,
      WCHAR* pszIcon,
      out int pnButton
    );

    [DllImport("Comctl32.dll", SetLastError = true)]
    public static extern void InitCommonControls();

    [DllImport("Comctl32.dll", SetLastError = true)]
    public static extern unsafe BOOL InitCommonControlsEx(
      InitCommonControlsEx* picce
    );
}
