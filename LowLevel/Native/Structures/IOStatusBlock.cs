using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    /// <summary>
    /// A driver sets an IRP's I/O status block
    /// to indicate the final status of an I/O request,
    /// before calling <c>IoCompleteRequest</c> for the IRP.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct IOStatusBlock
    {
        /// <summary>
        /// This is the completion status, either <see cref="NtStatuses.Success"/>
        /// if the requested operation was completed
        /// successfully or an informational, warning,
        /// or error <c>STATUS_XXX</c> value.
        /// </summary>
        [FieldOffset(0)] public NTSTATUS Status;
        /// <summary>
        /// Reserved. For internal use only.
        /// </summary>
        [FieldOffset(0)] readonly unsafe void* Pointer;
        /// <summary>
        /// This is set to a request-dependent value.
        /// For example, on successful completion of a transfer request,
        /// this is set to the number of bytes transferred.
        /// If a transfer request is completed with another
        /// <c>STATUS_XXX</c>, this member is set to zero.
        /// </summary>
        [FieldOffset(4)] public ULONG_PTR Information;
    }
}
