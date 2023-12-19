using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class Native
    {
        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe NTSTATUS NtOpenFile(
          [Out] HANDLE* FileHandle,
          [In] ACCESS_MASK DesiredAccess,
          [In] ObjectAttributes* ObjectAttributes,
          [Out] IOStatusBlock* IoStatusBlock,
          [In] ULONG ShareAccess,
          [In] ULONG OpenOptions
        );

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe NTSTATUS NtOpenProcess(
            [Out] HANDLE* ProcessHandle,
            [In] ACCESS_MASK DesiredAccess,
            [In] ObjectAttributes* ObjectAttributes,
            [In, Optional] ClientId* ClientId);

        [DllImport("ntdll.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe NTSTATUS NtCreateFile(
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
        public static extern unsafe NTSTATUS NtCreateThreadEx(
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
        public static extern NTSTATUS NtClose([In] HANDLE Handle);
    }
}
