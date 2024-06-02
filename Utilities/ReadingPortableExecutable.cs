using System.Buffers;
using System.Text;

namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct RelocationEntry
{
    /// <summary>
    /// Offset of the relocation within provided segment.
    /// </summary>
    public ushort Offset;
    /// <summary>
    /// Segment of the relocation, relative to the load segment address.
    /// </summary>
    public ushort Segment;
}

/// <summary>
/// DOS .EXE header
/// </summary>
/// <remarks>
/// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct ImageDOSHeader
{
    /// <summary> 0x5A4D (ASCII for 'M' and 'Z') </summary>
    public ushort Signature;
    /// <summary> Number of bytes in the last page </summary>
    public ushort ExtraBytes;
    /// <summary> Number of whole/partial pages </summary>
    public ushort Pages;
    /// <summary> Number of entries in the relocation table </summary>
    public ushort RelocationItems;
    /// <summary>
    /// The number of paragraphs taken up by the header.
    /// It can be any value, as the loader just uses it to find
    /// where the actual executable data starts. It may be larger
    /// than what the "standard" fields take up, and you may use
    /// it if you want to include your own header metadata, or put
    /// the relocation table there, or use it for any other purpose.
    /// </summary>
    public ushort HeaderSize;
    /// <summary>
    /// The number of paragraphs required by the program, excluding
    /// the PSP and program image.
    /// If no free block is big enough, the loading stops.
    /// </summary>
    public ushort MinimumAllocation;
    /// <summary>
    /// The number of paragraphs requested by the program.
    /// If no free block is big enough, the biggest one possible is allocated.
    /// </summary>
    public ushort MaximumAllocation;
    /// <summary> Relocatable segment address for SS </summary>
    public ushort InitialSS;
    /// <summary> Initial value for SP </summary>
    public ushort InitialSP;
    /// <summary> When added to the sum of all other words in the file, the result should be zero. </summary>
    public ushort Checksum;
    /// <summary> Initial value for IP </summary>
    public ushort InitialIP;
    /// <summary> Relocatable segment address for CS </summary>
    public ushort InitialCS;
    /// <summary> The (absolute) offset to the relocation table </summary>
    public ushort RelocationTable;
    /// <summary> Value used for overlay management. If zero, this is the main executable. </summary>
    public ushort Overlay;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res_0;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res_1;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res_2;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res_3;
    /// <summary> OEM identifier (for <see cref="OEMInfo"/>) </summary>
    public ushort OEMIdentifier;
    /// <summary> OEM information; <see cref="OEMIdentifier"/> specific </summary>
    public ushort OEMInfo;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_0;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_1;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_2;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_3;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_4;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_5;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_6;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_7;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_8;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)] readonly ushort e_res2_9;
    /// <summary> Starting address of the PE header </summary>
    public uint PEHeaderStart;
}

/// <remarks>
/// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public struct ImageDataDirectory
{
    public uint VirtualAddress;
    public uint Size;

    readonly string GetDebuggerDisplay()
    {
        if (VirtualAddress == 0 && Size == 0)
        { return "none"; }
        return $"( {Size} bytes at 0x{Convert.ToString(VirtualAddress, 16).PadLeft(8, '0')} )";
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ImageOptionalHeaderStandard
{
    /// <summary>
    /// The unsigned integer that identifies the state of the image file.
    /// The most common number is <c>0x10B</c>, which identifies it as a normal
    /// executable file. <c>0x107</c> identifies it as a ROM image, and <c>0x20B</c>
    /// identifies it as a PE32+ executable.
    /// </summary>
    public ushort Magic;
    /// <summary>
    /// The linker major version number.
    /// </summary>
    public byte MajorLinkerVersion;
    /// <summary>
    /// The linker minor version number.
    /// </summary>
    public byte MinorLinkerVersion;
    /// <summary>
    /// The size of the code (text) section, or the sum of all
    /// code sections if there are multiple sections.
    /// </summary>
    public uint SizeOfCode;
    /// <summary>
    /// The size of the initialized data section, or the sum of
    /// all such sections if there are multiple data sections.
    /// </summary>
    public uint SizeOfInitializedData;
    /// <summary>
    /// The size of the uninitialized data section (BSS), or
    /// the sum of all such sections if there are multiple BSS sections.
    /// </summary>
    public uint SizeOfUninitializedData;
    /// <summary>
    /// The address of the entry point relative to the image base when
    /// the executable file is loaded into memory. For program images,
    /// this is the starting address. For device drivers, this is the
    /// address of the initialization function. An entry point is optional
    /// for DLLs. When no entry point is present, this field must be zero.
    /// </summary>
    public uint AddressOfEntryPoint;
    /// <summary>
    /// The address that is relative to the image base of the
    /// beginning-of-code section when it is loaded into memory.
    /// </summary>
    public uint BaseOfCode;
}

/// <remarks>
/// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
/// </remarks>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ImageOptionalHeader32
{
    public ImageOptionalHeaderStandard Standard;

    /// <summary>
    /// The address that is relative to the image base of the beginning-of-data section when it is loaded into memory.
    /// </summary>
    public uint BaseOfData;
    /// <summary>
    /// The preferred address of the first byte of image when loaded into memory;
    /// must be a multiple of 64 K. The default for DLLs is 0x10000000.
    /// The default for Windows CE EXEs is 0x00010000. The default for Windows NT,
    /// Windows 2000, Windows XP, Windows 95, Windows 98, and
    /// Windows Me is 0x00400000.
    /// </summary>
    public uint ImageBase;
    /// <summary>
    /// The alignment (in bytes) of sections when they are loaded into memory.
    /// It must be greater than or equal to FileAlignment.
    /// The default is the page size for the architecture.
    /// </summary>
    public uint SectionAlignment;
    /// <summary>
    /// The alignment factor (in bytes) that is used to align the raw data
    /// of sections in the image file. The value should be a power of 2
    /// between 512 and 64 K, inclusive. The default is 512.
    /// If the <see cref="SectionAlignment"/> is less than the architecture's page size,
    /// then FileAlignment must match <see cref="SectionAlignment"/>.
    /// </summary>
    public uint FileAlignment;
    /// <summary>
    /// The major version number of the required operating system.
    /// </summary>
    public ushort MajorOperatingSystemVersion;
    /// <summary>
    /// The minor version number of the required operating system.
    /// </summary>
    public ushort MinorOperatingSystemVersion;
    /// <summary>
    /// The major version number of the image.
    /// </summary>
    public ushort MajorImageVersion;
    /// <summary>
    /// The minor version number of the image.
    /// </summary>
    public ushort MinorImageVersion;
    /// <summary>
    /// The major version number of the subsystem.
    /// </summary>
    public ushort MajorSubsystemVersion;
    /// <summary>
    /// The minor version number of the subsystem.
    /// </summary>
    public ushort MinorSubsystemVersion;
    /// <summary>
    /// Reserved, must be zero.
    /// </summary>
    readonly uint Win32VersionValue;
    /// <summary>
    /// The size (in bytes) of the image, including all headers,
    /// as the image is loaded in memory.
    /// It must be a multiple of <see cref="SectionAlignment"/>.
    /// </summary>
    public uint SizeOfImage;
    /// <summary>
    /// The combined size of an MS-DOS stub, PE header,
    /// and section headers rounded up to a
    /// multiple of <see cref="FileAlignment"/>.
    /// </summary>
    public uint SizeOfHeaders;
    /// <summary>
    /// The image file checksum. The algorithm for computing the checksum
    /// is incorporated into IMAGHELP.DLL.
    /// The following are checked for validation at load time:
    /// all drivers, any DLL loaded at boot time, and any DLL
    /// that is loaded into a critical Windows process.
    /// </summary>
    public uint CheckSum;
    /// <summary>
    /// The subsystem that is required to run this image.
    /// </summary>
    public ImageSubsystem Subsystem;
    public DLLCharacteristics DllCharacteristics;
    /// <summary>
    /// The size of the stack to reserve.
    /// Only <see cref="SizeOfStackCommit"/> is committed;
    /// the rest is made available one page at a
    /// time until the reserve size is reached.
    /// </summary>
    public uint SizeOfStackReserve;
    /// <summary>
    /// The size of the stack to commit.
    /// </summary>
    public uint SizeOfStackCommit;
    /// <summary>
    /// The size of the local heap space to reserve.
    /// Only <see cref="SizeOfHeapCommit"/> is committed;
    /// the rest is made available one page at a
    /// time until the reserve size is reached.
    /// </summary>
    public uint SizeOfHeapReserve;
    /// <summary>
    /// The size of the local heap space to commit.
    /// </summary>
    public uint SizeOfHeapCommit;
    /// <summary>
    /// Reserved, must be zero.
    /// </summary>
    readonly uint LoaderFlags;
    /// <summary>
    /// The number of data-directory entries in the remainder
    /// of the optional header.
    /// Each describes a location and size.
    /// </summary>
    public uint NumberOfRvaAndSizes;

    public ImageDataDirectory ExportTable;
    public ImageDataDirectory ImportTable;
    public ImageDataDirectory ResourceTable;
    public ImageDataDirectory ExceptionTable;
    public ImageDataDirectory CertificateTable;
    public ImageDataDirectory BaseRelocationTable;
    public ImageDataDirectory Debug;
    public ImageDataDirectory Architecture;
    public ImageDataDirectory GlobalPtr;
    public ImageDataDirectory TLSTable;
    public ImageDataDirectory LoadConfigTable;
    public ImageDataDirectory BoundImport;
    public ImageDataDirectory IAT;
    public ImageDataDirectory DelayImportDescriptor;
    public ImageDataDirectory CLRRuntimeHeader;
    readonly ImageDataDirectory Reserved;
}

/// <remarks>
/// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
/// </remarks>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ImageOptionalHeader64
{
    public ImageOptionalHeaderStandard Standard;

    /// <inheritdoc cref="ImageOptionalHeader32.ImageBase"/>
    public ulong ImageBase;
    /// <inheritdoc cref="ImageOptionalHeader32.SectionAlignment"/>
    public uint SectionAlignment;
    /// <inheritdoc cref="ImageOptionalHeader32.FileAlignment"/>
    public uint FileAlignment;
    /// <inheritdoc cref="ImageOptionalHeader32.MajorOperatingSystemVersion"/>
    public ushort MajorOperatingSystemVersion;
    /// <inheritdoc cref="ImageOptionalHeader32.MinorOperatingSystemVersion"/>
    public ushort MinorOperatingSystemVersion;
    /// <inheritdoc cref="ImageOptionalHeader32.MajorImageVersion"/>
    public ushort MajorImageVersion;
    /// <inheritdoc cref="ImageOptionalHeader32.MinorImageVersion"/>
    public ushort MinorImageVersion;
    /// <inheritdoc cref="ImageOptionalHeader32.MajorSubsystemVersion"/>
    public ushort MajorSubsystemVersion;
    /// <inheritdoc cref="ImageOptionalHeader32.MinorSubsystemVersion"/>
    public ushort MinorSubsystemVersion;
    /// <inheritdoc cref="ImageOptionalHeader32.Win32VersionValue"/>
    public uint Win32VersionValue;
    /// <inheritdoc cref="ImageOptionalHeader32.SizeOfImage"/>
    public uint SizeOfImage;
    /// <inheritdoc cref="ImageOptionalHeader32.SizeOfHeaders"/>
    public uint SizeOfHeaders;
    /// <inheritdoc cref="ImageOptionalHeader32.CheckSum"/>
    public uint CheckSum;
    /// <inheritdoc cref="ImageOptionalHeader32.Subsystem"/>
    public ImageSubsystem Subsystem;
    /// <inheritdoc cref="ImageOptionalHeader32.DllCharacteristics"/>
    public DLLCharacteristics DllCharacteristics;
    /// <inheritdoc cref="ImageOptionalHeader32.SizeOfStackReserve"/>
    public ulong SizeOfStackReserve;
    /// <inheritdoc cref="ImageOptionalHeader32.SizeOfStackCommit"/>
    public ulong SizeOfStackCommit;
    /// <inheritdoc cref="ImageOptionalHeader32.SizeOfHeapReserve"/>
    public ulong SizeOfHeapReserve;
    /// <inheritdoc cref="ImageOptionalHeader32.SizeOfHeapCommit"/>
    public ulong SizeOfHeapCommit;
    /// <inheritdoc cref="ImageOptionalHeader32.LoaderFlags"/>
    public uint LoaderFlags;
    /// <inheritdoc cref="ImageOptionalHeader32.NumberOfRvaAndSizes"/>
    public uint NumberOfRvaAndSizes;

    public ImageDataDirectory ExportTable;
    public ImageDataDirectory ImportTable;
    public ImageDataDirectory ResourceTable;
    public ImageDataDirectory ExceptionTable;
    public ImageDataDirectory CertificateTable;
    public ImageDataDirectory BaseRelocationTable;
    public ImageDataDirectory Debug;
    public ImageDataDirectory Architecture;
    public ImageDataDirectory GlobalPtr;
    public ImageDataDirectory TLSTable;
    public ImageDataDirectory LoadConfigTable;
    public ImageDataDirectory BoundImport;
    public ImageDataDirectory IAT;
    public ImageDataDirectory DelayImportDescriptor;
    public ImageDataDirectory CLRRuntimeHeader;
    readonly ImageDataDirectory Reserved;
}

/// <remarks>
/// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
/// </remarks>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ImageFileHeader
{
    public ushort Machine;
    public ushort NumberOfSections;
    public uint TimeDateStamp;
    public uint PointerToSymbolTable;
    public uint NumberOfSymbols;
    public ushort SizeOfOptionalHeader;
    public ushort Characteristics;
}

/// <remarks>
/// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
/// </remarks>
[StructLayout(LayoutKind.Explicit)]
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public struct ImageSectionHeader
{
    [FieldOffset(0)] public unsafe fixed byte Name[8];
    [FieldOffset(8)] public uint VirtualSize;
    [FieldOffset(12)] public uint VirtualAddress;
    [FieldOffset(16)] public uint SizeOfRawData;
    [FieldOffset(20)] public uint PointerToRawData;
    [FieldOffset(24)] public uint PointerToRelocations;
    [Obsolete("This value should be zero for an image because COFF debugging information is deprecated.")]
    [FieldOffset(28)] public uint PointerToLineNumbers;
    [FieldOffset(32)] public ushort NumberOfRelocations;
    [Obsolete("This value should be zero for an image because COFF debugging information is deprecated.")]
    [FieldOffset(34)] public ushort NumberOfLineNumbers;
    [FieldOffset(36)] public DataSectionFlags Characteristics;

    [FieldOffset(40)] Memory<byte> _data;

    public readonly unsafe string Section
    {
        get
        {
            fixed (byte* namePtr = Name)
            { return Encoding.UTF8.GetString(namePtr, 8).TrimEnd('\0'); }
        }
    }

    public readonly Memory<byte> RawData => _data;

    readonly string GetDebuggerDisplay() => $"( \"{Section}\" {SizeOfRawData} bytes )";

    public void ReadData(Memory<byte> total)
    {
        if (PointerToRawData == 0 || SizeOfRawData == 0)
        { _data = Memory<byte>.Empty; }
        _data = total.Slice((int)PointerToRawData, (int)SizeOfRawData);
    }

    public unsafe readonly Span<T> DataAs<T>()
        where T : unmanaged
    {
        using MemoryHandle pin = _data.Pin();
        return new Span<T>((T*)pin.Pointer, _data.Length);
    }

    public readonly string DataAsStringA() => Encoding.ASCII.GetString(_data.Span).TrimEnd('\0');
    public readonly string DataAsStringW() => Encoding.Unicode.GetString(_data.Span).TrimEnd('\0');
}

/// <summary>
/// The following values defined for the Subsystem field of
/// the optional header determine which
/// Windows subsystem (if any) is required to run the image.
/// </summary>
public enum ImageSubsystem : ushort
{
    /// <summary>An unknown subsystem</summary>
    Unknown = 0,
    /// <summary>Device drivers and native Windows processes</summary>
    Native = 1,
    /// <summary>The Windows graphical user interface (GUI) subsystem</summary>
    WindowsGUI = 2,
    /// <summary>The Windows character subsystem</summary>
    WindowsCUI = 3,
    /// <summary>The OS/2 character subsystem</summary>
    OS2CUI = 5,
    /// <summary>The Posix character subsystem</summary>
    PosixCUI = 7,
    /// <summary>Native Win9x driver</summary>
    NativeWindows = 8,
    /// <summary>Windows CE</summary>
    WindowsCEGUI = 9,
    /// <summary>An Extensible Firmware Interface (EFI) application</summary>
    EFIApplication = 10,
    /// <summary>An EFI driver with boot services</summary>
    EFIBootServiceDriver = 11,
    /// <summary>An EFI driver with run-time services</summary>
    EFIRuntimeDriver = 12,
    /// <summary>An EFI ROM image</summary>
    EFIROM = 13,
    /// <summary>XBOX</summary>
    XBOX = 14,
    /// <summary>Windows boot application</summary>
    WindowsBootApplication = 16,
}

/// <summary>
/// The following values are defined for the <c>DllCharacteristics</c> field of the optional header.
/// </summary>
[Flags]
public enum DLLCharacteristics : ushort
{
    /// <summary>Image can handle a high entropy 64-bit virtual address space</summary>
    HighEntropyVA = 0x0020,
    /// <summary>DLL can be relocated at load time</summary>
    DynamicBase = 0x0040,
    /// <summary>Code Integrity checks are enforced</summary>
    ForceIntegrity = 0x0080,
    /// <summary>Image is NX compatible</summary>
    NXCompatible = 0x0100,
    /// <summary>Isolation aware, but do not isolate the image</summary>
    NoIsolation = 0x0200,
    /// <summary>Does not use structured exception (SE) handling. No SE handler may be called in this image.</summary>
    NoSEH = 0x0400,
    /// <summary>Do not bind the image</summary>
    NoBind = 0x0800,
    /// <summary>Image must execute in an AppContainer</summary>
    AppContainer = 0x1000,
    /// <summary>A WDM driver</summary>
    WDMDriver = 0x2000,
    /// <summary>Image supports Control Flow Guard</summary>
    GuardCF = 0x4000,
    /// <summary>Terminal Server</summary>
    TerminalServerAware = 0x8000,
}

/// <remarks>
/// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
/// </remarks>
[Flags]
[SuppressMessage("Roslynator", "RCS1191")]
[SuppressMessage("Roslynator", "RCS1234")]
public enum DataSectionFlags : uint
{
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    TypeReg = 0x00000000,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    TypeDSect = 0x00000001,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    TypeNoLoad = 0x00000002,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    TypeGroup = 0x00000004,
    /// <summary> The section should not be padded to the next boundary. </summary>
    [Obsolete($"This flag is obsolete and is replaced by {nameof(Align1Bytes)}. This is valid only for object files.")]
    TypeNoPadded = 0x00000008,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    TypeCopy = 0x00000010,
    /// <summary>
    /// The section contains executable code.
    /// </summary>
    ContentCode = 0x00000020,
    /// <summary>
    /// The section contains initialized data.
    /// </summary>
    ContentInitializedData = 0x00000040,
    /// <summary>
    /// The section contains uninitialized data.
    /// </summary>
    ContentUninitializedData = 0x00000080,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    LinkOther = 0x00000100,
    /// <summary>
    /// The section contains comments or other information. The .drectve section has this type. This is valid for object files only.
    /// </summary>
    LinkInfo = 0x00000200,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    TypeOver = 0x00000400,
    /// <summary>
    /// The section will not become part of the image. This is valid only for object files.
    /// </summary>
    LinkRemove = 0x00000800,
    /// <summary>
    /// The section contains COMDAT data. For more information, see section 5.5.6, COMDAT Sections (Object Only). This is valid only for object files.
    /// </summary>
    LinkComDat = 0x00001000,
    /// <summary>
    /// Reset speculative exceptions handling bits in the TLB entries for this section.
    /// </summary>
    NoDeferSpecExceptions = 0x00004000,
    /// <summary>
    /// The section contains data referenced through the global pointer (GP).
    /// </summary>
    RelativeGP = 0x00008000,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    MemPurgeable = 0x00020000,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    Memory16Bit = 0x00020000,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    MemoryLocked = 0x00040000,
    /// <summary>
    /// Reserved for future use.
    /// </summary>
    MemoryPreload = 0x00080000,
    /// <summary>
    /// Align data on a 1-byte boundary. Valid only for object files.
    /// </summary>
    Align1Bytes = 0x00100000,
    /// <summary>
    /// Align data on a 2-byte boundary. Valid only for object files.
    /// </summary>
    Align2Bytes = 0x00200000,
    /// <summary>
    /// Align data on a 4-byte boundary. Valid only for object files.
    /// </summary>
    Align4Bytes = 0x00300000,
    /// <summary>
    /// Align data on an 8-byte boundary. Valid only for object files.
    /// </summary>
    Align8Bytes = 0x00400000,
    /// <summary>
    /// Align data on a 16-byte boundary. Valid only for object files.
    /// </summary>
    Align16Bytes = 0x00500000,
    /// <summary>
    /// Align data on a 32-byte boundary. Valid only for object files.
    /// </summary>
    Align32Bytes = 0x00600000,
    /// <summary>
    /// Align data on a 64-byte boundary. Valid only for object files.
    /// </summary>
    Align64Bytes = 0x00700000,
    /// <summary>
    /// Align data on a 128-byte boundary. Valid only for object files.
    /// </summary>
    Align128Bytes = 0x00800000,
    /// <summary>
    /// Align data on a 256-byte boundary. Valid only for object files.
    /// </summary>
    Align256Bytes = 0x00900000,
    /// <summary>
    /// Align data on a 512-byte boundary. Valid only for object files.
    /// </summary>
    Align512Bytes = 0x00A00000,
    /// <summary>
    /// Align data on a 1024-byte boundary. Valid only for object files.
    /// </summary>
    Align1024Bytes = 0x00B00000,
    /// <summary>
    /// Align data on a 2048-byte boundary. Valid only for object files.
    /// </summary>
    Align2048Bytes = 0x00C00000,
    /// <summary>
    /// Align data on a 4096-byte boundary. Valid only for object files.
    /// </summary>
    Align4096Bytes = 0x00D00000,
    /// <summary>
    /// Align data on an 8192-byte boundary. Valid only for object files.
    /// </summary>
    Align8192Bytes = 0x00E00000,
    /// <summary>
    /// The section contains extended relocations.
    /// </summary>
    LinkExtendedRelocationOverflow = 0x01000000,
    /// <summary>
    /// The section can be discarded as needed.
    /// </summary>
    MemoryDiscardable = 0x02000000,
    /// <summary>
    /// The section cannot be cached.
    /// </summary>
    MemoryNotCached = 0x04000000,
    /// <summary>
    /// The section is not pageable.
    /// </summary>
    MemoryNotPaged = 0x08000000,
    /// <summary>
    /// The section can be shared in memory.
    /// </summary>
    MemoryShared = 0x10000000,
    /// <summary>
    /// The section can be executed as code.
    /// </summary>
    MemoryExecute = 0x20000000,
    /// <summary>
    /// The section can be read.
    /// </summary>
    MemoryRead = 0x40000000,
    /// <summary>
    /// The section can be written to.
    /// </summary>
    MemoryWrite = 0x80000000
}

/// <summary>
/// Reads in the header information of the Portable Executable format.
/// Provides information such as the date the assembly was compiled.
/// </summary>
public class PEHeader
{
    /// <summary>
    /// The DOS header
    /// </summary>
    public readonly ImageDOSHeader DosHeader;

    /// <summary>
    /// The file header
    /// </summary>
    public readonly ImageFileHeader FileHeader;

    /// <summary>
    /// Optional 32 bit file header
    /// </summary>
    public readonly ImageOptionalHeader32? OptionalHeader32;

    /// <summary>
    /// Optional 64 bit file header
    /// </summary>
    public readonly ImageOptionalHeader64? OptionalHeader64;

    /// <summary>
    /// Image Section headers. Number of sections is in the file header.
    /// </summary>
    public readonly ImmutableArray<ImageSectionHeader> ImageSectionHeaders;

    readonly Memory<byte> TotalBytes;

    /// <summary>
    /// Gets if the file header is 32 bit or not
    /// </summary>
    /// <remarks>
    /// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
    /// </remarks>
    [MemberNotNullWhen(true, nameof(OptionalHeader32))]
    [MemberNotNullWhen(false, nameof(OptionalHeader64))]
    public bool Is32BitHeader
    {
        get
        {
            const ushort IMAGE_FILE_32BIT_MACHINE = 0x0100;
            return (IMAGE_FILE_32BIT_MACHINE & FileHeader.Characteristics) == IMAGE_FILE_32BIT_MACHINE;
        }
    }

    /// <summary>
    /// Gets the timestamp from the file header
    /// </summary>
    /// <remarks>
    /// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
    /// </remarks>
    public DateTime TimeStamp
    {
        get
        {
            // Timestamp is a date offset from 1970
            DateTime returnValue = new(1970, 1, 1, 0, 0, 0);

            // Add in the number of seconds since 1970/1/1
            returnValue = returnValue.AddSeconds(FileHeader.TimeDateStamp);

            // Adjust to local time zone
            returnValue += TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);

            return returnValue;
        }
    }

    /// <remarks>
    /// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
    /// </remarks>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="MissingMethodException"/>
    /// <exception cref="InvalidOperationException"/>
    /// <exception cref="EndOfStreamException"/>
    /// <exception cref="PathTooLongException"/>
    /// <exception cref="DirectoryNotFoundException"/>
    /// <exception cref="UnauthorizedAccessException"/>
    /// <exception cref="FileNotFoundException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="System.Security.SecurityException"/>
    public unsafe PEHeader(string filePath)
    {
        byte[] buffer = File.ReadAllBytes(filePath);
        TotalBytes = new Memory<byte>(buffer);

        // Read in the DLL or EXE and get the timestamp
        using MemoryStream stream = new(buffer);
        using BinaryReader reader = new(stream);
        DosHeader = Memory.FromBinaryReader<ImageDOSHeader>(reader);

        // Add 4 bytes to the offset
        stream.Seek(DosHeader.PEHeaderStart, SeekOrigin.Begin);

        uint ntHeadersSignature = reader.ReadUInt32();
        FileHeader = Memory.FromBinaryReader<ImageFileHeader>(reader);

        if (this.Is32BitHeader)
        {
            OptionalHeader32 = Memory.FromBinaryReader<ImageOptionalHeader32>(reader);
            OptionalHeader64 = null;
        }
        else
        {
            OptionalHeader32 = null;
            OptionalHeader64 = Memory.FromBinaryReader<ImageOptionalHeader64>(reader);
        }

        ImageSectionHeader[] imageSectionHeaders = new ImageSectionHeader[FileHeader.NumberOfSections];
        for (int i = 0; i < FileHeader.NumberOfSections; i++)
        {
            imageSectionHeaders[i] = Memory.FromBinaryReader<ImageSectionHeader>(reader, 40);
            imageSectionHeaders[i].ReadData(TotalBytes);
        }
        ImageSectionHeaders = ImmutableCollectionsMarshal.AsImmutableArray(imageSectionHeaders);
    }

    /// <exception cref="KeyNotFoundException"/>
    unsafe public ref readonly ImageSectionHeader GetSection(string sectionName)
    {
        for (int i = 0; i < ImageSectionHeaders.Length; i++)
        {
            if (string.Equals(ImageSectionHeaders[i].Section, sectionName, StringComparison.OrdinalIgnoreCase))
            { return ref ImageSectionHeaders.AsSpan()[i]; }
        }
        throw new KeyNotFoundException($"Section \"{sectionName}\" not found");
    }

    unsafe public bool TryGetSection(string sectionName, ref ImageSectionHeader imageSectionHeader)
    {
        for (int i = 0; i < ImageSectionHeaders.Length; i++)
        {
            if (string.Equals(ImageSectionHeaders[i].Section, sectionName, StringComparison.OrdinalIgnoreCase))
            {
                imageSectionHeader = ImageSectionHeaders[i];
                return true;
            }
        }
        return false;
    }

    unsafe public bool HasSection(string sectionName)
    {
        for (int i = 0; i < ImageSectionHeaders.Length; i++)
        {
            if (string.Equals(ImageSectionHeaders[i].Section, sectionName, StringComparison.OrdinalIgnoreCase))
            { return true; }
        }
        return false;
    }

    /// <exception cref="KeyNotFoundException"/>
    unsafe public void ValidateShellcode()
    {
        if (!HasSection(".text"))
        { throw new KeyNotFoundException("Section \".text\" not found"); }

        if (HasSection(".eh_fram"))
        {
#if DEBUG
            string bruh = GetSection(".eh_fram").DataAsStringA();
#endif

            Memory<byte> data = GetSection(".eh_fram").RawData;
            if (Memory.HasData<byte>(data.Span))
            { throw new KeyNotFoundException("Assembly has \".eh_fram\" section"); }
        }

        if (HasSection(".data"))
        {
#if DEBUG
            string bruh = GetSection(".data").DataAsStringA();
#endif

            Memory<byte> data = GetSection(".data").RawData;
            if (Memory.HasData<byte>(data.Span))
            { throw new KeyNotFoundException("Assembly has \".data\" section"); }
        }

        if (HasSection(".rdata"))
        {
#if DEBUG
            string bruh = GetSection(".rdata").DataAsStringA();
#endif

            Memory<byte> data = GetSection(".rdata").RawData;
            if (Memory.HasData<byte>(data.Span))
            { throw new KeyNotFoundException("Assembly has \".rdata\" section"); }
        }

        if (HasSection(".idata"))
        {
#if DEBUG
            string bruh = GetSection(".idata").DataAsStringA();
#endif

            Memory<byte> data = GetSection(".idata").RawData;
            if (Memory.HasData<byte>(data.Span))
            { throw new KeyNotFoundException("Assembly has \".idata\" section"); }
        }
    }

    /// <exception cref="KeyNotFoundException"/>
    /// <exception cref="WindowsException"/>
    /// <exception cref="OverflowException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    [SupportedOSPlatform("windows")]
    unsafe public Thread ExecuteShellcode(void* arg)
    {
        Memory<byte> shellcode = GetSection(".text").RawData;

        void* executable = VirtualMemory.Alloc(shellcode.Length, MemoryProtectionFlags.ExecuteReadWrite);

        using MemoryHandle pin = shellcode.Pin();

        Buffer.MemoryCopy(pin.Pointer, executable, shellcode.Length, shellcode.Length);

        return Process.CurrentProcess.CreateThread((delegate*<void*, uint>)executable, arg);
    }
}
