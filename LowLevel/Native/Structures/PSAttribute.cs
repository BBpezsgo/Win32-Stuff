﻿using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PSAttribute
    {
        public ULONG_PTR Attribute;
        public SIZE_T Size;
        public unsafe void* ValuePtr;
        public unsafe SIZE_T* ReturnLength;
    }
}
