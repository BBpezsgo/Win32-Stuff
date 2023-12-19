using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    [DebuggerDisplay($"{{{nameof(DebuggerDisplay)}(),nq}}")]
    public readonly struct DeviceDriver : IEquatable<DeviceDriver>
    {
        readonly unsafe void* ImageBase;

        unsafe DeviceDriver(void* imageBase) => ImageBase = imageBase;

        public static unsafe implicit operator void*(DeviceDriver module) => module.ImageBase;
        public static unsafe implicit operator nint(DeviceDriver module) => (nint)module.ImageBase;

        public static bool operator ==(DeviceDriver left, DeviceDriver right) => left.Equals(right);
        public static bool operator !=(DeviceDriver left, DeviceDriver right) => !left.Equals(right);

        /// <exception cref="WindowsException"/>
        public static unsafe DeviceDriver[] GetDeviceDrivers()
        {
            void** deviceDrivers = stackalloc void*[128];
            DWORD got = default;
            if (Kernel32.EnumDeviceDrivers(deviceDrivers, (uint)(128 * sizeof(void*)), &got) == 0)
            { throw WindowsException.Get(); }
            got /= (uint)sizeof(void*);
            DeviceDriver[] result = new DeviceDriver[got];
            for (int i = 0; i < got; i++)
            { result[i] = new DeviceDriver(deviceDrivers[i]); }
            return result;
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe string FileName
        {
            get
            {
                const int MaxLength = 128;
                fixed (WCHAR* fileNamePtr = new string('\0', MaxLength))
                {
                    DWORD length = Kernel32.GetDeviceDriverFileNameW(ImageBase, fileNamePtr, MaxLength);
                    if (length == 0) throw WindowsException.Get();
                    return new string(fileNamePtr, 0, (int)length);
                }
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public unsafe string BaseName
        {
            get
            {
                const int MaxLength = 128;
                fixed (WCHAR* fileNamePtr = new string('\0', MaxLength))
                {
                    DWORD length = Kernel32.GetDeviceDriverBaseNameW(ImageBase, fileNamePtr, MaxLength);
                    if (length == 0) throw WindowsException.Get();
                    return new string(fileNamePtr, 0, (int)length);
                }
            }
        }

        public override bool Equals(object? obj) => obj is DeviceDriver module && Equals(module);
        public unsafe bool Equals(DeviceDriver other) => ImageBase == other.ImageBase;
        public override unsafe int GetHashCode() => ((nint)ImageBase).GetHashCode();
        public override unsafe string ToString() => "0x" + ((nint)ImageBase).ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');
        /// <exception cref="WindowsException"/>
        string DebuggerDisplay() => $"{BaseName} ({ToString()})";
    }
}
