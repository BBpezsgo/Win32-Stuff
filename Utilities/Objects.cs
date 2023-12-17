using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    using LowLevel;

    [SupportedOSPlatform("windows")]
    public static class Objects
    {
        /// <exception cref="GdiException"/>
        public static HGDIOBJ GetCurrentObject(HDC dc, ObjectType type)
        {
            HGDIOBJ obj = Gdi32.GetCurrentObject(dc, (uint)type);
            if (obj == HGDIOBJ.Zero)
            { throw new GdiException($"{nameof(Gdi32.GetCurrentObject)} has failed"); }
            return obj;
        }

        /// <exception cref="GdiException"/>
        unsafe public static T GetObject<T>(HANDLE handle) where T : unmanaged
        {
            T obj = default;
            if (Gdi32.GetObject(handle, sizeof(T), &obj) == 0)
            { throw new GdiException($"{nameof(Gdi32.GetObject)} has failed"); }
            return obj;
        }

        unsafe public static bool GetObject<T>(HANDLE handle, ref T obj) where T : unmanaged
            => Gdi32.GetObject(handle, sizeof(T), Unsafe.AsPointer(ref obj)) != 0;

        unsafe static int EnumObjectsProc([In] void* lpLogObject, [In] LPARAM lpData)
        {
            GCHandle handle = GCHandle.FromIntPtr(lpData);

            if (!handle.IsAllocated)
            { return FALSE; }

            object? obj = handle.Target;
            if (obj == null)
            { return FALSE; }

            List<IntPtr> list = (List<IntPtr>)obj;
            list.Add((IntPtr)lpLogObject);

            return TRUE;
        }

        /// <summary>
        /// Gets the pens or brushes available for the specified device context (DC).
        /// </summary>
        /// <param name="dc">
        /// A handle to the DC.
        /// </param>
        /// <param name="type">
        /// The object type.
        /// This parameter can be <see cref="ObjectType.BRUSH"/> or <see cref="ObjectType.PEN"/>.
        /// </param>
        unsafe public static void*[] GetObjects(HDC dc, ObjectType type)
        {
            List<IntPtr> list = new();
            GCHandle handle = GCHandle.Alloc(list, GCHandleType.Weak);
            
            _ = Gdi32.EnumObjects(dc, (int)type, &EnumObjectsProc, GCHandle.ToIntPtr(handle));

            void*[] result = new void*[list.Count];

            for (int i = 0; i < list.Count; i++)
            { result[i] = (void*)list[i]; }

            handle.Free();

            return result;
        }
    }
}
