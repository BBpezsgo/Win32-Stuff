namespace Win32
{
    /// <summary>Information Device Context</summary>
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
        /// A <see cref="DEVMODE"/> structure containing device-specific initialization
        /// data for the device driver. The <see href="https://learn.microsoft.com/en-us/windows/desktop/printdocs/documentproperties">DocumentProperties</see> function retrieves
        /// this structure filled in for a specified device. The <c>lpdvmInit</c> parameter
        /// must be <c>NULL</c> if the device driver is to use the default initialization
        /// (if any) specified by the user.
        /// </param>
        /// <returns></returns>
        /// <exception cref="GdiException"/>
        unsafe public static InformationDC Create(string driver, string device, DEVMODE deviceMode)
        {
            fixed (WCHAR* driverPtr = driver)
            fixed (WCHAR* devicePtr = device)
            {
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
                HDC handle = Gdi32.CreateICW(driverPtr, devicePtr, null, &deviceMode);
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
                if (handle == HDC.Zero)
                { throw new GdiException($"Failed to create DC ({nameof(Gdi32.CreateICW)})"); }
                return new InformationDC(handle);
            }
        }
    }
}
