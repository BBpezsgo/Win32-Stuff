namespace Win32.Gdi32
{
    using LowLevel;

    /// <summary>Information Device Context</summary>
    [SupportedOSPlatform("windows")]
    public class InformationDC : DC
    {
        InformationDC(HDC handle) : base(handle)
        { }

        /// <exception cref="GdiException"/>
        protected override void DisposeDC()
        {
            if (Gdi32.DeleteDC(Handle) == FALSE)
            { throw new GdiException($"Failed to delete DC ({nameof(Gdi32.DeleteDC)}) {this}"); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver">
        /// A string that specifies the name of the device driver (for example, Epson).
        /// </param>
        /// <param name="device">
        /// A string that specifies the name of the specific output device being used,
        /// as shown by the Print Manager (for example, Epson FX-80).
        /// It is not the printer model name. The <paramref name="device"/> parameter must be used.
        /// </param>
        /// <param name="deviceMode">
        /// A <see cref="DevMode"/> structure containing device-specific initialization
        /// data for the device driver. The <see href="https://learn.microsoft.com/en-us/windows/desktop/printdocs/documentproperties">DocumentProperties</see> function retrieves
        /// this structure filled in for a specified device. The <c>lpdvmInit</c> parameter
        /// must be <c>NULL</c> if the device driver is to use the default initialization
        /// (if any) specified by the user.
        /// </param>
        /// <returns></returns>
        /// <exception cref="GdiException"/>
        public static unsafe InformationDC Create(string driver, string device, DevMode deviceMode)
        {
            fixed (WCHAR* driverPtr = driver)
            fixed (WCHAR* devicePtr = device)
            {
                HDC handle = Gdi32.CreateICW(driverPtr, devicePtr, null, &deviceMode);
                if (handle == HDC.Zero)
                { throw new GdiException($"Failed to create DC ({nameof(Gdi32.CreateICW)})"); }
                return new InformationDC(handle);
            }
        }
    }
}
