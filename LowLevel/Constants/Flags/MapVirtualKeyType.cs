﻿namespace Win32;

public enum MapVirtualKeyType : UINT
{
    /// <summary>
    /// The uCode parameter is a virtual-key code and is translated
    /// into a scan code. If it is a virtual-key code that does not
    /// distinguish between left- and right-hand keys, the left-hand
    /// scan code is returned. If there is no translation, the function
    /// returns 0.
    /// </summary>
    VirtualKey2VirtualScanCode = 0,
    /// <summary>
    /// The uCode parameter is a scan code and is translated into a
    /// virtual-key code that does not distinguish between left- and
    /// right-hand keys. If there is no translation, the function returns 0.
    /// </summary>
    VirtualScanCode2VirtualKey = 1,
    /// <summary>
    /// The uCode parameter is a virtual-key code and is translated
    /// into an unshifted character value in the low order word of
    /// the return value. Dead keys (diacritics) are indicated by
    /// setting the top bit of the return value. If there is no translation,
    /// the function returns 0. See Remarks.
    /// </summary>
    VirtualKey2Char = 2,
    /// <summary>
    /// The uCode parameter is a scan code and is translated into a
    /// virtual-key code that distinguishes between left- and right-hand keys.
    /// If there is no translation, the function returns 0.
    /// </summary>
    VirtualScanCode2VirtualKeyEx = 3,
    /// <summary>
    /// Windows Vista and later: The uCode parameter is a virtual-key
    /// code and is translated into a scan code. If it is a virtual-key code
    /// that does not distinguish between left- and right-hand keys, the
    /// left-hand scan code is returned. If the scan code is an extended
    /// scan code, the high byte of the uCode value can contain either
    /// 0xe0 or 0xe1 to specify the extended scan code. If there is no
    /// translation, the function returns 0.
    /// </summary>
    VirtualKey2VirtualScanCodeEx = 4,
}
