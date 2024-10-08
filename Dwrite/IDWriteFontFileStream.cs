﻿namespace Win32.DWrite;

/// <summary>
/// The interface for loading font file data.
/// </summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("6d4865fe-0ab8-4d91-8f62-5dd6be34a3e0")]
[SupportedOSPlatform("windows")]
public interface IDWriteFontFileStream
{
    /// <summary>
    /// Reads a fragment from a file.
    /// </summary>
    /// <param name="fragmentStart">Receives the pointer to the start of the font file fragment.</param>
    /// <param name="fileOffset">Offset of the fragment from the beginning of the font file.</param>
    /// <param name="fragmentSize">Size of the fragment in bytes.</param>
    /// <param name="fragmentContext">The client defined context to be passed to the ReleaseFileFragment.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    /// <remarks>
    /// IMPORTANT: ReadFileFragment() implementations must check whether the requested file fragment
    /// is within the file bounds. Otherwise, an error should be returned from ReadFileFragment.
    /// </remarks>
    abstract unsafe void ReadFileFragment(
        void** fragmentStart,
        UINT64 fileOffset,
        UINT64 fragmentSize,
        [Out] void** fragmentContext
    );

    /// <summary>
    /// Releases a fragment from a file.
    /// </summary>
    /// <param name="fragmentContext">The client defined context of a font fragment returned from ReadFileFragment.</param>
    abstract unsafe void ReleaseFileFragment(
        void* fragmentContext
    );

    /// <summary>
    /// Obtains the total size of a file.
    /// </summary>
    /// <param name="fileSize">Receives the total size of the file.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    /// <remarks>
    /// Implementing GetFileSize() for asynchronously loaded font files may require
    /// downloading the complete file contents, therefore this method should only be used for operations that
    /// either require complete font file to be loaded (e.g., copying a font file) or need to make
    /// decisions based on the value of the file size (e.g., validation against a persisted file size).
    /// </remarks>
    abstract unsafe void GetFileSize(
        [Out] UINT64* fileSize
    );

    /// <summary>
    /// Obtains the last modified time of the file. The last modified time is used by DirectWrite font selection algorithms
    /// to determine whether one font resource is more up to date than another one.
    /// </summary>
    /// <param name="lastWriteTime">Receives the last modified time of the file in the format that represents
    /// the number of 100-nanosecond intervals since January 1, 1601 (UTC).</param>
    /// <returns>
    /// Standard HRESULT error code. For resources that don't have a concept of the last modified time, the implementation of
    /// GetLastWriteTime should return E_NOTIMPL.
    /// </returns>
    abstract unsafe void GetLastWriteTime(
        [Out] UINT64* lastWriteTime
    );
}
