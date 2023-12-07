namespace Win32
{
    public static class MEM
    {
        /// <summary>
        /// Allocates memory charges (from the overall size of memory and the paging files on disk) for the specified reserved memory pages. The function also guarantees that when the caller later initially accesses the memory, the contents will be zero. Actual physical pages are not allocated unless/until the virtual addresses are actually accessed.
        /// To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT | MEM_RESERVE.
        /// Attempting to commit a specific address range by specifying MEM_COMMIT without MEM_RESERVE and a non-NULL lpAddress fails unless the entire range has already been reserved.The resulting error code is ERROR_INVALID_ADDRESS.
        /// An attempt to commit a page that is already committed does not cause the function to fail.This means that you can commit pages without first determining the current commitment state of each page.
        /// If lpAddress specifies an address within an enclave, flAllocationType must be MEM_COMMIT.
        /// </summary>
        public const DWORD MEM_COMMIT = 0x00001000;

        /// <summary>
        /// Reserves a range of the process's virtual address space without allocating any actual physical storage in memory or in the paging file on disk.
        /// You commit reserved pages by calling VirtualAllocEx again with MEM_COMMIT. To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT | MEM_RESERVE.
        /// Other memory allocation functions, such as malloc and LocalAlloc, cannot use reserved memory until it has been released.
        /// </summary>
        public const DWORD MEM_RESERVE = 0x00002000;

        /// <summary>
        /// Indicates that data in the memory range specified by lpAddress and dwSize is no longer of interest.The pages should not be read from or written to the paging file. However, the memory block will be used again later, so it should not be decommitted. This value cannot be used with any other value.
        /// Using this value does not guarantee that the range operated on with MEM_RESET will contain zeros. If you want the range to contain zeros, decommit the memory and then recommit it.
        /// When you use MEM_RESET, the VirtualAllocEx function ignores the value of fProtect. However, you must still set fProtect to a valid protection value, such as PAGE_NOACCESS.
        /// VirtualAllocEx returns an error if you use MEM_RESET and the range of memory is mapped to a file. A shared view is only acceptable if it is mapped to a paging file.
        /// </summary>
        public const DWORD MEM_RESET = 0x00080000;

        /// <summary>
        /// MEM_RESET_UNDO should only be called on an address range to which MEM_RESET was successfully applied earlier. It indicates that the data in the specified memory range specified by lpAddress and dwSize is of interest to the caller and attempts to reverse the effects of MEM_RESET. If the function succeeds, that means all data in the specified address range is intact. If the function fails, at least some of the data in the address range has been replaced with zeroes.
        /// This value cannot be used with any other value. If MEM_RESET_UNDO is called on an address range which was not MEM_RESET earlier, the behavior is undefined. When you specify MEM_RESET, the VirtualAllocEx function ignores the value of flProtect. However, you must still set flProtect to a valid protection value, such as PAGE_NOACCESS.
        /// Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  The MEM_RESET_UNDO flag is not supported until Windows 8 and Windows Server 2012.
        /// </summary>
        public const DWORD MEM_RESET_UNDO = 0x1000000;

        /// <summary>
        /// Decommits the specified region of committed pages.After the operation, the pages are in the reserved state.
        /// The function does not fail if you attempt to decommit an uncommitted page.This means that you can decommit a range of pages without first determining the current commitment state.
        /// The MEM_DECOMMIT value is not supported when the lpAddress parameter provides the base address for an enclave.This is true for enclaves that do not support dynamic memory management (i.e.SGX1). SGX2 enclaves permit MEM_DECOMMIT anywhere in the enclave.
        /// </summary>
        public const DWORD MEM_DECOMMIT = 0x00004000;

        /// <summary>
        /// Releases the specified region of pages, or placeholder(for a placeholder, the address space is released and available for other allocations). After this operation, the pages are in the free state.
        /// If you specify this value, dwSize must be 0 (zero), and lpAddress must point to the base address returned by the VirtualAlloc function when the region is reserved.The function fails if either of these conditions is not met.
        /// If any pages in the region are committed currently, the function first decommits, and then releases them.
        /// The function does not fail if you attempt to release pages that are in different states, some reserved and some committed.This means that you can release a range of pages without first determining the current commitment state.
        /// </summary>
        public const DWORD MEM_RELEASE = 0x00008000;

        /// <summary>
        /// To coalesce two adjacent placeholders, specify MEM_RELEASE | MEM_COALESCE_PLACEHOLDERS.When you coalesce placeholders, lpAddress and dwSize must exactly match the overall range of the placeholders to be merged.
        /// </summary>
        public const DWORD MEM_COALESCE_PLACEHOLDERS = 0x00000001;

        /// <summary>
        /// Frees an allocation back to a placeholder (after you've replaced a placeholder with a private allocation using VirtualAlloc2 or Virtual2AllocFromApp).
        /// To split a placeholder into two placeholders, specify MEM_RELEASE | MEM_PRESERVE_PLACEHOLDER.
        /// </summary>
        public const DWORD MEM_PRESERVE_PLACEHOLDER = 0x00000002;
    }
}
