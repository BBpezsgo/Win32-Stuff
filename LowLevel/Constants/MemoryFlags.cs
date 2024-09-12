namespace Win32;

[Flags]
public enum MemoryFlags : DWORD
{
    /// <summary>
    /// To coalesce two adjacent placeholders, specify <see cref="Release"/> | <see cref="CoalescePlaceholders"/>.When you coalesce placeholders, <c>lpAddress</c> and <c>dwSize</c> must exactly match the overall range of the placeholders to be merged.
    /// </summary>
    CoalescePlaceholders = 0x00000001,

    /// <summary>
    /// Frees an allocation back to a placeholder (after you've replaced a placeholder with a private allocation using <see cref="Kernel32.VirtualAlloc2"/> or <see cref="Kernel32.Virtual2AllocFromApp"/>).
    /// To split a placeholder into two placeholders, specify <see cref="Release"/> | <see cref="PreservePlaceholder"/>.
    /// </summary>
    PreservePlaceholder = 0x00000002,

    /// <summary>
    /// Allocates memory charges (from the overall size of memory and the paging files on disk) for the specified reserved memory pages. The function also guarantees that when the caller later initially accesses the memory, the contents will be zero. Actual physical pages are not allocated unless/until the virtual addresses are actually accessed.
    /// To reserve and commit pages in one step, call <see cref="Kernel32.VirtualAllocEx"/> with <see cref="Commit"/> | <see cref="Reserve"/>.
    /// Attempting to commit a specific address range by specifying <see cref="Commit"/> without <see cref="Reserve"/> and a non-<see langword="NULL"/> <see cref="lpAddress"/> fails unless the entire range has already been reserved.The resulting error code is <c>ERROR_INVALID_ADDRESS</c>.
    /// An attempt to commit a page that is already committed does not cause the function to fail.This means that you can commit pages without first determining the current commitment state of each page.
    /// If <c>lpAddress</c> specifies an address within an enclave, <c>flAllocationType</c> must be <see cref="Commit"/>.
    /// </summary>
    Commit = 0x00001000,

    /// <summary>
    /// Reserves a range of the process's virtual address space without allocating any actual physical storage in memory or in the paging file on disk.
    /// You commit reserved pages by calling <see cref="Kernel32.VirtualAllocEx"/> again with <see cref="Commit"/>. To reserve and commit pages in one step, call <see cref="Kernel32.VirtualAllocEx"/> with <see cref="Commit"/> | <see cref="Reserve"/>.
    /// Other memory allocation functions, such as malloc and <see cref="Kernel32.LocalAlloc"/>, cannot use reserved memory until it has been released.
    /// </summary>
    Reserve = 0x00002000,

    /// <summary>
    /// Decommits the specified region of committed pages.After the operation, the pages are in the reserved state.
    /// The function does not fail if you attempt to decommit an uncommitted page. This means that you can decommit a range of pages without first determining the current commitment state.
    /// The <see cref="Decommit"/> value is not supported when the <c>lpAddress</c> parameter provides the base address for an enclave.This is true for enclaves that do not support dynamic memory management (i.e.SGX1). SGX2 enclaves permit <see cref="Decommit"/> anywhere in the enclave.
    /// </summary>
    Decommit = 0x00004000,

    /// <summary>
    /// Releases the specified region of pages, or placeholder (for a placeholder, the address space is released and available for other allocations). After this operation, the pages are in the free state.
    /// If you specify this value, <c>dwSize</c> must be 0 (zero), and <c>lpAddress</c> must point to the base address returned by the <see cref="Kernel32.VirtualAlloc"/> function when the region is reserved.The function fails if either of these conditions is not met.
    /// If any pages in the region are committed currently, the function first decommits, and then releases them.
    /// The function does not fail if you attempt to release pages that are in different states, some reserved and some committed.This means that you can release a range of pages without first determining the current commitment state.
    /// </summary>
    Release = 0x00008000,

    /// <summary>
    /// Indicates that data in the memory range specified by <c>lpAddress</c> and <c>dwSize</c> is no longer of interest.The pages should not be read from or written to the paging file. However, the memory block will be used again later, so it should not be decommitted. This value cannot be used with any other value.
    /// Using this value does not guarantee that the range operated on with <see cref="Reset"/> will contain zeros. If you want the range to contain zeros, decommit the memory and then recommit it.
    /// When you use <see cref="Reset"/>, the <see cref="Kernel32.VirtualAllocEx"/> function ignores the value of <c>fProtect</c>. However, you must still set <c>fProtect</c> to a valid protection value, such as <c>PAGE_NOACCESS</c>.
    /// <see cref="Kernel32.VirtualAllocEx"/> returns an error if you use <see cref="Reset"/> and the range of memory is mapped to a file. A shared view is only acceptable if it is mapped to a paging file.
    /// </summary>
    Reset = 0x00080000,

    /// <summary>
    /// <see cref="ResetUndo"/> should only be called on an address range to which <see cref="Reset"/> was successfully applied earlier. It indicates that the data in the specified memory range specified by <c>lpAddress</c> and <c>dwSize</c> is of interest to the caller and attempts to reverse the effects of <see cref="Reset"/>. If the function succeeds, that means all data in the specified address range is intact. If the function fails, at least some of the data in the address range has been replaced with zeroes.
    /// This value cannot be used with any other value. If <see cref="ResetUndo"/> is called on an address range which was not <see cref="Reset"/> earlier, the behavior is undefined. When you specify <see cref="Reset"/>, the <see cref="Kernel32.VirtualAllocEx"/> function ignores the value of <c>flProtect</c>. However, you must still set <c>flProtect</c> to a valid protection value, such as <c>PAGE_NOACCESS</c>.
    /// Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  The <see cref="ResetUndo"/> flag is not supported until Windows 8 and Windows Server 2012.
    /// </summary>
    ResetUndo = 0x1000000,
}
