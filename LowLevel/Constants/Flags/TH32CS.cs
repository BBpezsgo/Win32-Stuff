namespace Win32.LowLevel
{
    public enum TH32CS : DWORD
    {
        /// <summary>
        /// Indicates that the snapshot handle is to be inheritable.
        /// </summary>
        INHERIT = 0x80000000,
        /// <summary>
        /// Includes all processes and threads in the system,
        /// plus the heaps and modules of the process specified
        /// in <c>th32ProcessID</c>. Equivalent to specifying the
        /// <see cref="SNAPHEAPLIST"/>, <see cref="SNAPMODULE"/>,
        /// <see cref="SNAPPROCESS"/>, and <see cref="SNAPTHREAD"/>
        /// values combined using an OR operation ('|').
        /// </summary>
        SNAPALL = SNAPHEAPLIST | SNAPMODULE | SNAPPROCESS | SNAPTHREAD,
        /// <summary>
        /// Includes all heaps of the process specified in
        /// <c>th32ProcessID</c> in the snapshot. To enumerate the heaps,
        /// see <see cref="Kernel32.Heap32ListFirst"/>.
        /// </summary>
        SNAPHEAPLIST = 0x00000001,
        /// <summary>
        /// <para>
        /// Includes all modules of the process specified in
        /// <c>th32ProcessID</c> in the snapshot. To enumerate the modules,
        /// see <see cref="Kernel32.Module32FirstW"/>. If
        /// the function fails with <c>ERROR_BAD_LENGTH</c>,
        /// retry the function until it succeeds.
        /// </para>
        /// <para>
        /// 64-bit Windows:  Using this flag in a 32-bit process includes
        /// the 32-bit modules of the process specified in <c>th32ProcessID</c>,
        /// while using it in a 64-bit process includes the 64-bit modules.
        /// To include the 32-bit modules of the process specified in
        /// <c>th32ProcessID</c> from a 64-bit process, use the <see cref="SNAPMODULE32"/> flag.
        /// </para>
        /// </summary>
        SNAPMODULE = 0x00000008,

        /// <summary>
        /// Includes all 32-bit modules of the process specified
        /// in <c>th32ProcessID</c> in the snapshot when called from a 64-bit
        /// process. This flag can be combined with <see cref="SNAPMODULE"/> or <see cref="SNAPALL"/>.
        /// If the function fails with <c>ERROR_BAD_LENGTH</c>, retry the function
        /// until it succeeds.
        /// </summary>
        SNAPMODULE32 = 0x00000010,
        /// <summary>
        /// Includes all processes in the system in the snapshot.
        /// To enumerate the processes, see <see cref="Kernel32.Process32FirstW"/>.
        /// </summary>
        SNAPPROCESS = 0x00000002,
        /// <summary>
        /// Includes all threads in the system in the snapshot.
        /// To enumerate the threads, see <see cref="Kernel32.Thread32First"/>.
        /// To identify the threads that belong to a specific
        /// process, compare its process identifier to the
        /// <c>th32OwnerProcessID</c> member of the <see cref="ThreadEntry"/> structure
        /// when enumerating the threads.
        /// </summary>
        SNAPTHREAD = 0x00000004,
    }
}
