using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32.COM
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("cafcb56c-6ac3-4889-bf47-9e23bbd260ec")]
    public interface IDXGISurface : IDXGIDeviceSubObject
    {
    }
}
