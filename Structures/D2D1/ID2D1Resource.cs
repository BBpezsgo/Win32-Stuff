using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// The root interface for all resources in D2D.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2cd90691-12e2-11dc-9fed-001143a055f9")]
    public interface ID2D1Resource
    {
        /// <summary>
        /// Retrieve the factory associated with this resource.
        /// </summary>
        abstract void GetFactory(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out ID2D1Factory factory
        );
    }
}
