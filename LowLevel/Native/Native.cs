using System.Runtime.InteropServices;

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

namespace Win32.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class Native
    {
        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        unsafe extern public static NTSTATUS NtOpenFile(
          [Out] HANDLE* FileHandle,
          [In] ACCESS_MASK DesiredAccess,
          [In] ObjectAttributes* ObjectAttributes,
          [Out] IOStatusBlock* IoStatusBlock,
          [In] ULONG ShareAccess,
          [In] ULONG OpenOptions
        );

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        unsafe extern public static NTSTATUS NtOpenProcess(
            [Out] HANDLE* ProcessHandle,
            [In] ACCESS_MASK DesiredAccess,
            [In] ObjectAttributes* ObjectAttributes,
            [In, Optional] ClientId* ClientId);

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        unsafe extern public static NTSTATUS NtCreateFile(
          [Out] HANDLE* FileHandle,
          [In] ACCESS_MASK DesiredAccess,
          [In] ObjectAttributes* ObjectAttributes,
          [Out] IOStatusBlock* IoStatusBlock,
          [In, Optional] LARGE_INTEGER* AllocationSize,
          [In] ULONG FileAttributes,
          [In] ULONG ShareAccess,
          [In] ULONG CreateDisposition,
          [In] ULONG CreateOptions,
          [In] void* EaBuffer,
          [In] ULONG EaLength
        );

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        unsafe extern public static NTSTATUS NtCreateThreadEx(
             [Out] HANDLE* ThreadHandle,
             [In] ACCESS_MASK DesiredAccess,
             [In, Optional] ObjectAttributes* ObjectAttributes,
             [In] HANDLE ProcessHandle,
             [In] delegate*<void*, uint> StartRoutine,
             [In, Optional] void* Argument,
             [In] ULONG CreateFlags, // THREAD_CREATE_FLAGS_*
             [In] SIZE_T ZeroBits,
             [In] SIZE_T StackSize,
             [In] SIZE_T MaximumStackSize,
             [In, Optional] PSAttributeList* AttributeList);

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        extern public static NTSTATUS NtClose([In] HANDLE Handle);
    }
}
