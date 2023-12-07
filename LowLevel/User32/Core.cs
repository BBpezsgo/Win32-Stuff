using System.Runtime.InteropServices;

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

namespace Win32.LowLevel
{
    public static partial class User32
    {
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int GetMouseMovePointsEx(
          [In] UINT cbSize,
          [In] MouseMovePoint* lppt,
          [Out] MouseMovePoint* lpptBuf,
          [In] int nBufPoints,
          [In] DWORD resolution
        );

        /// <summary>
        /// Retrieves the current double-click time for the mouse.
        /// A double-click is a series of two clicks of the mouse button,
        /// the second occurring within a specified time after the first.
        /// The double-click time is the maximum number of milliseconds that may occur between
        /// the first and second click of a double-click.
        /// The maximum double-click time is 5000 milliseconds.
        /// </summary>
        /// <returns>
        /// The return value specifies the current double-click time, in milliseconds.
        /// The maximum return value is 5000 milliseconds.
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern UINT GetDoubleClickTime();

        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL SetDoubleClickTime(
          [In] UINT unnamedParam1
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern UINT SendInput(
          [In] UINT cInputs,
          [In] InputInfo* pInputs,
          [In] int cbSize
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int ToUnicode(
          [In] UINT wVirtKey,
          [In] UINT wScanCode,
          [In, Optional] BYTE* lpKeyState,
          [Out] WCHAR* pwszBuff,
          [In] int cchBuff,
          [In] UINT wFlags
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern UINT MapVirtualKeyW(
          [In] UINT uCode,
          [In] MapVirtualKeyType uMapType
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern BOOL GetKeyboardState(
          [Out] BYTE* lpKeyState
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern HWND WindowFromDC(
          [In] HDC hDC
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern HBRUSH GetSysColorBrush(
          [In] int nIndex
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern BOOL SetRect(
          [Out] RECT* lprc,
          [In] int xLeft,
          [In] int yTop,
          [In] int xRight,
          [In] int yBottom
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int DrawTextW(
          [In] HDC hdc,
          [In, Out] WCHAR* lpchText,
          [In] int cchText,
          [In, Out] RECT* lprc,
          [In] UINT format
        );

        unsafe public static int DrawTextW(
           HDC hdc,
           string lpchText,
           int cchText,
           RECT* lprc,
           UINT format
        )
        { fixed (WCHAR* lpchTextPtr = lpchText) return DrawTextW(hdc, lpchTextPtr, cchText, lprc, format); }

        [DllImport("User32.dll", SetLastError = true)]
        public static extern HDC GetWindowDC(
          [In] HWND hWnd
        );

        /// <summary>
        /// The <c>GetDC</c> function retrieves a handle to a device context (DC)
        /// for the client area of a specified window or for the entire
        /// screen. You can use the returned handle in subsequent GDI
        /// functions to draw in the DC. The device context is an opaque
        /// data structure, whose values are used internally by GDI.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern HDC GetDC(
          [In] HWND hWnd
        );

        /// <summary>
        /// The <c>ReleaseDC</c> function releases a device context (DC),
        /// freeing it for use by other applications. The effect of
        /// the <c>ReleaseDC</c> function depends on the type of DC.
        /// It frees only common and window DCs. It has no effect
        /// on class or private DCs.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose DC is to be released.
        /// </param>
        /// <param name="hDC">
        /// A handle to the DC to be released.
        /// </param>
        /// <returns>
        /// <para>
        /// The return value indicates whether the DC was released.
        /// If the DC was released, the return value is 1.
        /// </para>
        /// <para>
        /// If the DC was not released, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern int ReleaseDC(
          [In] HWND hWnd,
          [In] HDC hDC
        );

        /// <summary>
        /// Processes accelerator keys for menu commands.
        /// The function translates a WM_KEYDOWN or
        /// <see cref="WindowMessage.WM_SYSKEYDOWN"/> message to a <see cref="WindowMessage.WM_COMMAND"/> or
        /// <see cref="WindowMessage.WM_SYSCOMMAND"/> message (if there is an entry
        /// for the key in the specified accelerator table)
        /// and then sends the <see cref="WindowMessage.WM_COMMAND"/> or <see cref="WindowMessage.WM_SYSCOMMAND"/>
        /// message directly to the specified window procedure.
        /// <c>TranslateAccelerator</c> does not return until the
        /// window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose messages are to be translated.
        /// </param>
        /// <param name="hAccTable">
        /// A handle to the accelerator table. The accelerator table must have been
        /// loaded by a call to the <see cref="LoadAcceleratorsW"/> function or created by a call
        /// to the <see cref="CreateAcceleratorTable"/> function.
        /// </param>
        /// <param name="lpMsg">
        /// A pointer to an <see cref="MSG"/> structure that contains message information retrieved
        /// from the calling thread's message queue using the <see cref="GetMessageW"/>
        /// or <see cref="PeekMessageW"/> function.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int TranslateAcceleratorW(
          [In] HWND hWnd,
          [In] HACCEL hAccTable,
          [In] MSG* lpMsg
        );

        /// <summary>
        /// Loads the specified accelerator table.
        /// </summary>
        /// <param name="hInstance">
        /// A handle to the module whose executable file contains the accelerator table to be loaded.
        /// </param>
        /// <param name="lpTableName">
        /// The name of the accelerator table to be loaded. Alternatively,
        /// this parameter can specify the resource identifier of an accelerator-table
        /// resource in the low-order word and zero in the high-order word.
        /// To create this value, use the <see cref="Macros.MAKEINTRESOURCE"/> macro.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a handle to the loaded accelerator table.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <c>NULL</c>.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern HACCEL LoadAcceleratorsW(
          [In, Optional] HINSTANCE hInstance,
          [In] WCHAR* lpTableName
        );

        /// <summary>
        /// Loads an icon, cursor, animated cursor, or bitmap.
        /// </summary>
        /// <param name="hInst"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="fuLoad"></param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is the handle of the newly loaded image.
        /// </para>
        /// <para>
        /// If the function fails, the return value is <c>NULL</c>.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern HANDLE LoadImageW(
          [In, Optional] HINSTANCE hInst,
          [In] WCHAR* name,
          [In] UINT type,
          [In] int cx,
          [In] int cy,
          [In] UINT fuLoad
        );

        /// <summary>
        /// Unregisters a window class, freeing the memory required for the class.
        /// </summary>
        /// <param name="lpClassName">
        /// A null-terminated string or a class atom.
        /// If <paramref name="lpClassName"/> is a string, it specifies the window class name.
        /// This class name must have been registered by a previous call
        /// to the <see cref="RegisterClassW"/> or <see cref="RegisterClassExW"/> function.
        /// System classes, such as dialog box controls, cannot be unregistered.
        /// If this parameter is an atom, it must be a class atom created by
        /// a previous call to the <see cref="RegisterClassW"/> or <see cref="RegisterClassExW"/> function.
        /// The atom must be in the low-order word of <paramref name="lpClassName"/>;
        /// the high-order word must be zero.
        /// </param>
        /// <param name="hInstance">
        /// A handle to the instance of the module that created the class.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the class could not be found or if a window still
        /// exists that was created with the class, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// Before calling this function, an application must destroy all
        /// windows created with the specified class.
        /// </para>
        /// <para>
        /// All window classes that an application registers are unregistered when it terminates.
        /// </para>
        /// <para>
        /// Class atoms are special atoms returned only by
        /// <see cref="RegisterClassW"/> and <see cref="RegisterClassExW"/>.
        /// </para>
        /// <para>
        /// No window classes registered by a DLL are unregistered when the .dll is unloaded.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern BOOL UnregisterClassW(
          [In] WCHAR* lpClassName,
          [In, Optional] HINSTANCE hInstance
        );

        /// <summary>
        /// Retrieves information about a window class, including a handle to
        /// the small icon associated with the window class.
        /// The <see cref="GetClassInfoW"/> function does not retrieve a handle to the small icon.
        /// </summary>
        /// <param name="hInstance">
        /// A handle to the instance of the application that created the class.
        /// To retrieve information about classes defined by the system
        /// (such as buttons or list boxes), set this parameter to <c>NULL</c>.
        /// </param>
        /// <param name="lpszClass">
        /// The class name. The name must be that of a preregistered class or
        /// a class registered by a previous call to the <see cref="RegisterClassW"/> or
        /// <see cref="RegisterClassExW"/> function. Alternatively, this parameter can be
        /// a class atom created by a previous call to <see cref="RegisterClassW"/> or
        /// <see cref="RegisterClassExW"/>. The atom must be in the low-order word of
        /// <paramref name="lpszClass"/>; the high-order word must be zero.
        /// </param>
        /// <param name="lpwcx">
        /// A pointer to a <see cref="WNDCLASSEXW"/> structure that
        /// receives the information about the class.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function finds a matching class and successfully
        /// copies the data, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function does not find a matching class and successfully copy the data,
        /// the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// Class atoms are created using the <see cref="RegisterClassW"/> or
        /// <see cref="RegisterClassExW"/> function, not the <see cref="GlobalAddAtom"/> function.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern BOOL GetClassInfoExW(
          [In, Optional] HINSTANCE hInstance,
          [In] WCHAR* lpszClass,
          [Out] WNDCLASSEXW* lpwcx
        );

        /// <summary>
        /// Retrieves the name of the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window and, indirectly, the class to which the window belongs.
        /// </param>
        /// <param name="lpClassName">
        /// The class name string.
        /// </param>
        /// <param name="nMaxCount">
        /// The length of the <paramref name="lpClassName"/> buffer, in characters.
        /// The buffer must be large enough to include the terminating null
        /// character; otherwise, the class name string is truncated
        /// to <paramref name="nMaxCount"/>-1 characters.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is the number
        /// of characters copied to the buffer, not including
        /// the terminating null character.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/> function.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int GetClassNameW(
          [In] HWND hWnd,
          [Out] WCHAR* lpClassName,
          [In] int nMaxCount
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern BOOL SetPropW(
          [In] HWND hWnd,
          [In] WCHAR* lpString,
          [In, Optional] HANDLE hData
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern HANDLE RemovePropW(
          [In] HWND hWnd,
          [In] WCHAR* lpString
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern HANDLE GetPropW(
          [In] HWND hWnd,
          [In] WCHAR* lpString
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int EnumPropsExW(
          [In] HWND hWnd,
          [In] delegate*<HWND, WCHAR*, HANDLE, ULONG_PTR, BOOL> lpEnumFunc,
          [In] LPARAM lParam
        );

        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int EnumPropsW(
          [In] HWND hWnd,
          [In] delegate*<HWND, WCHAR*, HANDLE, BOOL> lpEnumFunc
        );

        /// <summary>
        /// Destroys the specified timer.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window associated with the specified timer.
        /// This value must be the same as the <c>hWnd</c> value passed to
        /// the <see cref="SetTimer"/> function that created the timer.
        /// </param>
        /// <param name="uIDEvent">
        /// The timer to be destroyed. If the window handle
        /// passed to <see cref="SetTimer"/> is valid, this parameter
        /// must be the same as the <c>nIDEvent</c> value passed to <see cref="SetTimer"/>.
        /// If the application calls <see cref="SetTimer"/> with hWnd set to <c>NULL</c>, this
        /// parameter must be the timer identifier returned by <see cref="SetTimer"/>.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// The <c>KillTimer</c> function does not remove
        /// <see cref="WindowMessage.WM_TIMER"/> messages already posted to the message queue.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL KillTimer(
          [In, Optional] HWND hWnd,
          [In] UINT_PTR uIDEvent
        );

        /// <summary>
        /// Creates a timer with the specified time-out value.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window to be associated with the timer.
        /// This window must be owned by the calling thread.
        /// If a <c>NULL</c> value for hWnd is passed in along with an
        /// <paramref name="nIDEvent"/> of an existing timer, that timer will be
        /// replaced in the same way that an existing non-<c>NULL</c>
        /// <paramref name="hWnd"/> timer will be.
        /// </param>
        /// <param name="nIDEvent">
        /// A nonzero timer identifier. If the <paramref name="hWnd"/> parameter is <c>NULL</c>,
        /// and the <paramref name="nIDEvent"/> does not match an existing timer then it is
        /// ignored and a new timer ID is generated. If the <paramref name="hWnd"/> parameter
        /// is not <c>NULL</c> and the window specified by <paramref name="hWnd"/> already has a
        /// timer with the value <paramref name="nIDEvent"/>, then the existing timer is
        /// replaced by the new timer. When <c>SetTimer</c> replaces a timer,
        /// the timer is reset. Therefore, a message will be sent after
        /// the current time-out value elapses, but the previously set
        /// time-out value is ignored. If the call is not intended to
        /// replace an existing timer, <paramref name="nIDEvent"/> should be 0 if the
        /// <paramref name="hWnd"/> is <c>NULL</c>.
        /// </param>
        /// <param name="uElapse">
        /// <para>
        /// The time-out value, in milliseconds.
        /// </para>
        /// <para>
        /// If <paramref name="uElapse"/> is less than <c>USER_TIMER_MINIMUM</c> (0x0000000A),
        /// the timeout is set to <c>USER_TIMER_MINIMUM</c>. If <paramref name="uElapse"/> is
        /// greater than <c>USER_TIMER_MAXIMUM</c> (0x7FFFFFFF), the timeout
        /// is set to <c>USER_TIMER_MAXIMUM</c>.
        /// </para>
        /// </param>
        /// <param name="lpTimerFunc">
        /// A pointer to the function to be notified when the
        /// time-out value elapses. For more information about
        /// the function, see <c>TimerProc</c>. If <paramref name="lpTimerFunc"/> is <c>NULL</c>,
        /// the system posts a <see cref="WindowMessage.WM_TIMER"/> message to the application queue.
        /// The <c>hwnd</c> member of the message's <see cref="MSG"/> structure contains the
        /// value of the <paramref name="hWnd"/> parameter.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds and the <paramref name="hWnd"/> parameter is <c>NULL</c>,
        /// the return value is an integer identifying the new timer.
        /// An application can pass this value to the <see cref="KillTimer"/> function
        /// to destroy the timer.
        /// </para>
        /// <para>
        /// If the function succeeds and the <paramref name="hWnd"/> parameter is not <c>NULL</c>,
        /// then the return value is a nonzero integer. An application can
        /// pass the value of the <paramref name="nIDEvent"/> parameter to the <see cref="KillTimer"/>
        /// function to destroy the timer.
        /// </para>
        /// <para>
        /// If the function fails to create a timer, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern UINT_PTR SetTimer(
          [In, Optional] HWND hWnd,
          [In] UINT_PTR nIDEvent,
          [In] UINT uElapse,
          [In, Optional] delegate*<HWND, UINT, UINT_PTR, DWORD, void> lpTimerFunc
        );

        /// <summary>
        /// Triggers a visual signal to indicate that a sound is playing.
        /// </summary>
        /// <returns>
        /// This function returns one of the following values.
        /// <list type="table">
        /// <item>
        /// <term>
        /// TRUE
        /// </term>
        /// <description>
        /// The visual signal was or will be displayed correctly.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// FALSE
        /// </term>
        /// <description>
        /// An error prevented the signal from being displayed.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Set the notification behavior by calling
        /// <see cref="SystemParametersInfo"/> with the <see cref="SPI.SETSOUNDSENTRY"/> value.
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL SoundSentry();

        /// <summary>
        /// The <c>FillRect</c> function fills a rectangle by using the specified brush.
        /// This function includes the left and top borders, but excludes the
        /// right and bottom borders of the rectangle.
        /// </summary>
        /// <param name="hDC">
        /// A handle to the device context.
        /// </param>
        /// <param name="lprc">
        /// A pointer to a <see cref="RECT"/> structure that contains
        /// the logical coordinates of the rectangle to be filled.
        /// </param>
        /// <param name="hbr">
        /// A handle to the brush used to fill the rectangle.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern int FillRect(
          [In] HDC hDC,
          [In] RECT* lprc,
          [In] HBRUSH hbr
        );

        /// <summary>
        /// Retrieves a handle that can be used by the <see cref="UpdateResourceW"/> function
        /// to add, delete, or replace resources in a binary module.
        /// </summary>
        /// <param name="pFileName">
        /// The binary file in which to update resources.
        /// An application must be able to obtain write-access to this file;
        /// the file referenced by <paramref name="pFileName"/> cannot be currently executing.
        /// If <paramref name="pFileName"/> does not specify a full path, the system searches
        /// for the file in the current directory.
        /// </param>
        /// <param name="bDeleteExistingResources">
        /// Indicates whether to delete the <paramref name="pFileName"/> parameter's existing resources.
        /// If this parameter is <c>TRUE</c>, existing resources are deleted and the updated
        /// file includes only resources added with the <see cref="UpdateResourceW"/> function.
        /// If this parameter is <c>FALSE</c>, the updated file includes existing resources
        /// unless they are explicitly deleted or replaced by using <see cref="UpdateResourceW"/>.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle that can be used by
        /// the <see cref="UpdateResourceW"/> and <see cref="EndUpdateResourceW"/> functions. The return value is
        /// <c>NULL</c> if the specified file is not a PE, the file does not exist, or the
        /// file cannot be opened for writing. To get extended error information,
        /// call <see cref="Kernel32.GetLastError"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// It is recommended that the resource file is not loaded before this function is called.
        /// However, if that file is already loaded, it will not cause an error to be returned.
        /// </para>
        /// <para>
        /// There are some restrictions on resource updates in files that contain
        /// Resource Configuration(RC Config) data: LN files and the associated .mui files.
        /// Details on which types of resources are allowed to be updated in these files
        /// are in the Remarks section for the <see cref="UpdateResourceW"/> function.
        /// </para>
        /// <para>
        /// This function can update resources within modules that contain both code and resources.
        /// As noted above, there are restrictions on resource updates in LN files and .mui files,
        /// both of which contain RC Config data;
        /// details of the restrictions are in the reference for the <see cref="UpdateResourceW"/> function.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern HANDLE BeginUpdateResourceW(
          [In] WCHAR* pFileName,
          [In] BOOL bDeleteExistingResources
        );

        /// <summary>
        /// Blocks keyboard and mouse input events from reaching applications.
        /// </summary>
        /// <param name="fBlockIt">
        /// The function's purpose. If this parameter is <c>TRUE</c>, keyboard and mouse input events are blocked.
        /// If this parameter is <c>FALSE</c>, keyboard and mouse events are unblocked.
        /// Note that only the thread that blocked input can successfully unblock input.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If input is already blocked, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// When input is blocked, real physical input from the mouse or keyboard will not
        /// affect the input queue's synchronous key state
        /// (reported by <see cref="GetKeyState"/> and <see cref="GetKeyboardState"/>),
        /// nor will it affect the asynchronous key state (reported by <see cref="GetAsyncKeyState"/>).
        /// However, the thread that is blocking input can affect both of these key states by calling <see cref="SendInput"/>.
        /// No other thread can do this.
        /// </para>
        /// <para>
        /// The system will unblock input in the following cases:
        /// <list type="bullet">
        /// <item>
        /// The thread that blocked input unexpectedly exits without
        /// calling <c>BlockInput</c> with <paramref name="fBlockIt"/> set to <c>FALSE</c>. In this case,
        /// the system cleans up properly and re-enables input.
        /// </item>
        /// <item>
        /// The user presses CTRL+ALT+DEL or the system invokes
        /// the <i>Hard System Error</i> modal message box
        /// (for example, when a program faults or a device fails).
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL BlockInput(
          [In] BOOL fBlockIt
        );

        /// <summary>
        /// Retrieves or sets the value of one of the system-wide parameters.
        /// This function can also update the user profile while setting a parameter.
        /// </summary>
        /// <param name="uiAction">
        /// The system-wide parameter to be retrieved or set.
        /// </param>
        /// <param name="uiParam">
        /// A parameter whose usage and format depends on the system parameter
        /// being queried or set. For more information about system-wide parameters,
        /// see the <c>uiAction</c> parameter. If not otherwise indicated, you
        /// must specify zero for this parameter.
        /// </param>
        /// <param name="pvParam">
        /// A parameter whose usage and format depends on the system parameter being queried or set.
        /// For more information about system-wide parameters, see the <c>uiAction</c> parameter.
        /// If not otherwise indicated, you must specify <c>NULL</c> for this parameter.
        /// For information on the <c>PVOID</c> datatype, see <see href="https://learn.microsoft.com/en-us/windows/desktop/WinProg/windows-data-types">Windows Data Types</see>.
        /// </param>
        /// <param name="fWinIni">
        /// <para>
        /// If a system parameter is being set, specifies whether the user profile
        /// is to be updated, and if so, whether the <see cref="WindowMessage.WM_SETTINGCHANGE"/> message is
        /// to be broadcast to all top-level windows to notify them of the change.
        /// </para>
        /// <para>
        /// This parameter can be zero if you do not want to update the user
        /// profile or broadcast the <see cref="WindowMessage.WM_SETTINGCHANGE"/> message.
        /// </para>
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a nonzero value.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        unsafe public static extern BOOL SystemParametersInfoW(
          [In] UINT uiAction,
          [In] UINT uiParam,
          [In, Out] void* pvParam,
          [In] UINT fWinIni
        );

        /// <summary>
        /// Determines whether the calling thread is already a GUI thread.
        /// It can also optionally convert the thread to a GUI thread.
        /// </summary>
        /// <param name="bConvert">
        /// If <c>TRUE</c> and the thread is not a GUI thread, convert the thread to a GUI thread.
        /// </param>
        /// <returns>
        /// <para>
        /// The function returns a nonzero value in the following situations:
        /// <list type="bullet">
        /// <item>
        /// If the calling thread is already a GUI thread.
        /// </item>
        /// <item>
        /// If <c>bConvert</c> is <c>TRUE</c> and the function successfully converts the thread to a GUI thread.
        /// </item>
        /// </list>
        /// Otherwise, the function returns zero.
        /// </para>
        /// <para>
        /// If <c>bConvert</c> is <c>TRUE</c> and the function cannot successfully convert
        /// the thread to a GUI thread, <c>IsGUIThread</c> returns <c>ERROR_NOT_ENOUGH_MEMORY</c>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL IsGUIThread(
          [In] BOOL bConvert
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int GetSystemMetrics(
          [In] SystemMetricsFlags nIndex
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL GetCursorPos(
          [Out] out POINT lpPoint
        );

        [DllImport("User32.dll", SetLastError = true)]
        public static extern BOOL SetCursorPos(
          [In] int X,
          [In] int Y
        );

        /// <summary>
        /// Retrieves information about the global cursor.
        /// </summary>
        /// <param name="pci">
        /// A pointer to a <see cref="CURSORINFO"/> structure that receives the information.
        /// Note that you must set the <c>cbSize</c> member to <see langword="sizeof"/>(<see cref="CURSORINFO"/>) before calling this function.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL GetCursorInfo(
          [In, Out] CURSORINFO* pci
        );

        /// <summary>
        /// Retrieves information about the active window or a specified GUI thread.
        /// </summary>
        /// <param name="idThread">
        /// The identifier for the thread for which information is to be retrieved.
        /// To retrieve this value, use the <see cref="GetWindowThreadProcessId"/> function.
        /// If this parameter is <c>NULL</c>, the function returns information for the foreground thread.
        /// </param>
        /// <param name="pgui">
        /// A pointer to a <see cref="GUITHREADINFO"/> structure that receives information describing the thread.
        /// Note that you must set the cbSize member to <see langword="sizeof"/>(<see cref="GUITHREADINFO"/>) before calling
        /// this function.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is nonzero.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// This function succeeds even if the active window is not owned by the calling process.
        /// If the specified thread does not exist or have an input queue, the function will fail.
        /// </para>
        /// <para>
        /// This function is useful for retrieving out-of-context information about a thread.
        /// The information retrieved is the same as if an application retrieved the information about itself.
        /// </para>
        /// <para>
        /// For an edit control, the returned rcCaret rectangle contains the caret plus information on
        /// text direction and padding. Thus, it may not give the correct position of the cursor.
        /// The Sans Serif font uses four characters for the cursor:
        /// <list type="table">
        /// <item>
        /// <term>
        /// CURSOR_LTR
        /// </term>
        /// <description>
        /// 0xf00c
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// CURSOR_RTL
        /// </term>
        /// <description>
        /// 0xf00d
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// CURSOR_THAI
        /// </term>
        /// <description>
        /// 0xf00e
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// CURSOR_USA
        /// </term>
        /// <description>
        /// 0xfff (this is a marker value with no associated glyph)
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// To get the actual insertion point in the <c>rcCaret</c> rectangle, perform the following steps.
        /// <list type="number">
        /// <item>
        /// Call <see cref="GetKeyboardLayout"/> to retrieve the current input language.
        /// </item>
        /// <item>
        /// Determine the character used for the cursor, based on the current input language.
        /// </item>
        /// <item>
        /// Call <see cref="CreateFont"/> using Sans Serif for the font,
        /// the height given by <c>rcCaret</c>, and a width of <c>zero</c>.
        /// For <c>fnWeight</c>, call <c>SystemParametersInfo(SPI_GETCARETWIDTH, 0, pvParam, 0)</c>.
        /// If <c>pvParam</c> is greater than 1, set <c>fnWeight</c> to 700, otherwise set <c>fnWeight</c> to 400.
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// The function may not return valid window handles in the
        /// <see cref="GUITHREADINFO"/> structure when called to retrieve information
        /// for the foreground thread, such as when a window is losing activation.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL GetGUIThreadInfo(
          [In] DWORD idThread,
          [In, Out] GUITHREADINFO* pgui
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern LRESULT SendMessage(
              [In] HWND hWnd,
              [In] UINT Msg,
              [In] WPARAM wParam,
              [In] LPARAM lParam
            );

        /// <summary>
        /// Displays a modal dialog box that contains a system icon,
        /// a set of buttons, and a brief application-specific message,
        /// such as status or error information. The message box returns
        /// an integer value that indicates which button the user clicked.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the owner window of the message box to be created.
        /// If this parameter is <c>NULL</c>, the message box has no owner window.
        /// </param>
        /// <param name="text">
        /// The message to be displayed. If the string consists of more
        /// than one line, you can separate the lines using a carriage return
        /// and/or linefeed character between each line.
        /// </param>
        /// <param name="caption">
        /// The dialog box title. If this parameter is <c>NULL</c>, the default title is Error.
        /// </param>
        /// <param name="type">
        /// The contents and behavior of the dialog box. This parameter can be
        /// a combination of flags from the following groups of flags.
        /// </param>
        /// <returns></returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern MessageBoxResult MessageBox(
          [In, Optional] HWND hWnd,
          [In, Optional] char* lpText,
          [In, Optional] char* lpCaption,
          [In] UINT uType
        );

        unsafe public static MessageBoxResult MessageBox(
           HWND hWnd,
           string lbText,
           string lpCaption,
           UINT uType)
        {
            fixed (char* _lbText = lbText)
            {
                fixed (char* _lpCaption = lpCaption)
                {
                    return User32.MessageBox(hWnd, _lbText, _lpCaption, uType);
                }
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern BOOL PostMessageW(
          [In, Optional] HWND hWnd,
          [In] UINT Msg,
          [In] WPARAM wParam,
          [In] LPARAM lParam
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void PostQuitMessage(
          [In] int nExitCode
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL InvalidateRect(
          [In] HWND hWnd,
          [In] RECT* lpRect,
          [In] BOOL bErase
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern LRESULT DispatchMessageW(
            [In] MSG* lpMsg
        );


        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL GetMessageW(
              [Out] MSG* lpMsg,
              [In, Optional] HWND hWnd,
              [In] UINT wMsgFilterMin,
              [In] UINT wMsgFilterMax
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL PeekMessageW(
              [Out] MSG* lpMsg,
              [In, Optional] HWND hWnd,
              [In] uint wMsgFilterMin,
              [In] uint wMsgFilterMax,
              [In] PeekMessageFlags wRemoveMsg
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL TranslateMessage(
          [In] MSG* lpMsg
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern BOOL EndPaint(
              [In] HWND hWnd,
              [In] Win32.Gdi32.PaintStruct* lpPaint
        );

        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern HDC BeginPaint(
              [In] HWND hWnd,
              [Out] Win32.Gdi32.PaintStruct* lpPaint
        );

        /// <summary>
        /// Registers a window class for subsequent use in
        /// calls to the <see cref="CreateWindowW"/> or <see cref="CreateWindowExW"/> function.
        /// </summary>
        /// <param name="unnamedParam1">
        /// A pointer to a <see cref="WNDCLASSEXW"/> structure. You must fill the structure
        /// with the appropriate class attributes before passing it to the function.
        /// </param>
        /// <returns>
        /// <para>
        /// If the function succeeds, the return value is a class atom that uniquely
        /// identifies the class being registered. This atom can only
        /// be used by the <see cref="CreateWindowW"/>, <see cref="CreateWindowExW"/>,
        /// <see cref="GetClassInfo"/>,
        /// <see cref="GetClassInfoEx"/>, <see cref="FindWindow"/>,
        /// <see cref="FindWindowEx"/>, and <see cref=" UnregisterClass"/>
        /// functions and the <c>IActiveIMMap::FilterClientWindows</c> method.
        /// </para>
        /// <para>
        /// If the function fails, the return value is zero.
        /// To get extended error information, call <see cref="Kernel32.GetLastError"/>.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// If you register the window class by using <c>RegisterClassExA</c>,
        /// the application tells the system that the windows of the
        /// created class expect messages with text or character parameters
        /// to use the ANSI character set; if you register it by using
        /// <c>RegisterClassExW</c>, the application requests that the system pass
        /// text parameters of messages as Unicode. The <see cref="IsWindowUnicode"/>
        /// function enables applications to query the nature of each window.
        /// For more information on ANSI and Unicode functions,
        /// see <see href="https://learn.microsoft.com/en-us/windows/desktop/Intl/conventions-for-function-prototypes">Conventions for Function Prototypes</see>.
        /// </para>
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        unsafe public static extern ATOM RegisterClassExW(
            [In] WNDCLASSEXW* unnamedParam1);
    }
}
