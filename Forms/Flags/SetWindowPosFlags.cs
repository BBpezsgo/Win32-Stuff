﻿namespace Win32.Forms;

[Flags]
[SuppressMessage("Roslynator", "RCS1234")]
public enum SetWindowPosFlags : UINT
{
    /// <summary>
    /// Retains the current size (ignores the cx and cy parameters).
    /// </summary>
    NoSize = 0x0001,
    /// <summary>
    /// Retains the current position (ignores X and Y parameters).
    /// </summary>
    NoMove = 0x0002,
    /// <summary>
    /// Retains the current Z order (ignores the <c>hWndInsertAfter</c> parameter).
    /// </summary>
    NoZOrder = 0x0004,
    /// <summary>
    /// Does not redraw changes.If this flag is set, no repainting of any kind occurs.
    /// This applies to the client area, the nonclient area (including the title bar and
    /// scroll bars), and any part of the parent window uncovered as a result of the window
    /// being moved.When this flag is set, the application must explicitly invalidate or
    /// redraw any parts of the window and parent window that need redrawing.
    /// </summary>
    NoRedraw = 0x0008,
    /// <summary>
    /// Does not activate the window.If this flag is not set, the window is activated
    /// and moved to the top of either the topmost or non-topmost group
    /// (depending on the setting of the <c>hWndInsertAfter</c> parameter).
    /// </summary>
    NoActivate = 0x0010,
    /// <summary>
    /// Draws a frame (defined in the window's class description) around the window.
    /// </summary>
    DrawFrame = 0x0020,
    /// <summary>
    /// Applies new frame styles set using the <see cref="User32.SetWindowLong"/> function.
    /// Sends a <see cref="WindowMessage.WM_NCCALCSIZE"/> message to the window, even if the window's
    /// size is not being changed. If this flag is not specified, <see cref="WindowMessage.WM_NCCALCSIZE"/>
    /// is sent only when the window's size is being changed.
    /// </summary>
    FrameChanged = 0x0020,
    /// <summary>
    /// Displays the window.
    /// </summary>
    ShowWindow = 0x0040,
    /// <summary>
    /// Hides the window.
    /// </summary>
    HideWindow = 0x0080,
    /// <summary>
    /// Discards the entire contents of the client area.If this flag is not specified,
    /// the valid contents of the client area are saved and copied back into the client
    /// area after the window is sized or repositioned.
    /// </summary>
    CoCopyBits = 0x0100,
    /// <summary>
    /// Does not change the owner window's position in the Z order.
    /// </summary>
    NoOwnerZOrder = 0x0200,
    /// <summary>
    /// Prevents the window from receiving the <see cref="WindowMessage.WM_WINDOWPOSCHANGING"/> message.
    /// </summary>
    NoSendChanging = 0x0400,
    /// <summary>
    /// Prevents generation of the <see cref="WindowMessage.WM_SYNCPAINT"/> message.
    /// </summary>
    DeferErase = 0x2000,
    /// <summary>
    /// If the calling thread and the thread that owns the window are attached
    /// to different input queues, the system posts the request to the thread
    /// that owns the window. This prevents the calling thread from blocking its
    /// execution while other threads process the request.
    /// </summary>
    AsyncWindowPos = 0x4000,
}
