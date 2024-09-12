using System.Globalization;

namespace Win32;

/// <summary>
/// See also <see href="https://learn.microsoft.com/en-us/windows/win32/com/structure-of-com-error-codes">the Microsoft documentation</see>
/// </summary>
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public readonly struct HResult : IEquatable<HResult>
{
    readonly HRESULT code;

    HResult(int code) => this.code = code;

    public readonly bool IsSucceeded => HResult.SUCCEEDED(code);
    public readonly bool IsFailed => HResult.FAILED(code);
    public readonly bool IsError => HResult.IS_ERROR(code);
    /// <summary>
    /// A single letter, 'S' or 'E', that indicates whether the function
    /// call succeeded ('S') or produced an error ('E')
    /// </summary>
    public readonly int Severity => HResult.HRESULT_SEVERITY(code);
    /// <summary>
    /// Indicates the system service responsible for the error.
    /// </summary>
    public readonly int FacilityId => HResult.HRESULT_FACILITY(code);
    public readonly string? Facility => Win32.Facility.ToString(FacilityId);
    /// <summary>
    /// A unique number that is assigned to represent the error or warning.
    /// </summary>
    public readonly int Code => HResult.HRESULT_CODE(code);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is HResult result && Equals(result);
    /// <inheritdoc/>
    public bool Equals(HResult other) => code == other.code;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(code);

    /// <inheritdoc/>
    public override string ToString() => code.ToString(CultureInfo.InvariantCulture);
    readonly string GetDebuggerDisplay() => ToString();

    /// <exception cref="HResultException"/>
    [SupportedOSPlatform("windows")]
    public HResult Throw()
    {
        if (IsSucceeded) return this;
        throw new HResultException(this);
    }

    /// <inheritdoc cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Equality"/>
    public static bool operator ==(HResult left, HResult right) => left.code == right.code;
    /// <inheritdoc cref="System.Numerics.IEqualityOperators{TSelf, TOther, TResult}.op_Inequality"/>
    public static bool operator !=(HResult left, HResult right) => left.code != right.code;

    public static implicit operator HRESULT(HResult hr) => hr.code;
    public static implicit operator HResult(HRESULT hr) => new(hr);

    const int MESSAGE_BUFFER_SIZE = 255;

    [SupportedOSPlatform("windows")]
    public unsafe string? Message
    {
        get
        {
            fixed (char* buffer = new string('\0', MESSAGE_BUFFER_SIZE))
            {
                uint messageLength = Kernel32.FormatMessageW(
                    FormatMessageFlags.FromSystem |
                    FormatMessageFlags.IgnoreInserts,
                    IntPtr.Zero,
                    (uint)code,
                    PrimaryLanguage.SystemDefault,
                    buffer,
                    MESSAGE_BUFFER_SIZE,
                    IntPtr.Zero);

                if (messageLength == 0)
                { return null; }

                string result = new(buffer, 0, (int)messageLength);
                return result.Trim();
            }
        }
    }

    #region Common HRESULT Values

    /// <summary>
    /// The operation was aborted because of an unspecified error.
    /// </summary>
    public const HRESULT Abort = unchecked((HRESULT)0x80004004);
    /// <summary>
    /// A general access-denied error.
    /// </summary>
    public const HRESULT AccessDenied = unchecked((HRESULT)0x80070005);
    /// <summary>
    /// An unspecified failure has occurred.
    /// </summary>
    public const HRESULT Fail = unchecked((HRESULT)0x80004005);
    /// <summary>
    /// An invalid handle was used.
    /// </summary>
    public const HRESULT Handle = unchecked((HRESULT)0x80070006);
    /// <summary>
    /// One or more arguments are invalid.
    /// </summary>
    public const HRESULT InvalidArg = unchecked((HRESULT)0x80070057);
    /// <summary>
    /// The <c>QueryInterface</c> method did not recognize the requested interface.
    /// The interface is not supported.
    /// </summary>
    public const HRESULT NoInterface = unchecked((HRESULT)0x80004002);
    /// <summary>
    /// The method is not implemented.
    /// </summary>
    public const HRESULT NotImplemented = unchecked((HRESULT)0x80004001);
    /// <summary>
    /// The method failed to allocate necessary memory.
    /// </summary>
    public const HRESULT OutOfMemory = unchecked((HRESULT)0x8007000E);
    /// <summary>
    /// The data necessary to complete the operation is not yet available.
    /// </summary>
    public const HRESULT Pending = unchecked((HRESULT)0x8000000A);
    /// <summary>
    /// An invalid pointer was used.
    /// </summary>
    public const HRESULT InvalidPointer = unchecked((HRESULT)0x80004003);
    /// <summary>
    /// A catastrophic failure has occurred.
    /// </summary>
    public const HRESULT Unexpected = unchecked((HRESULT)0x8000FFFF);
    /// <summary>
    /// The method succeeded and returned the boolean value <see cref="Constants.FALSE"/>.
    /// </summary>
    public const HRESULT False = 0x00000001;
    /// <summary>
    /// The method succeeded.
    /// If a boolean return value is expected, the returned value is <see cref="TRUE"/>.
    /// </summary>
    public const HRESULT Ok = 0x00000000;

    #endregion

    #region Utils

    /// <summary>
    /// Extracts the error code portion of the <c>HRESULT</c>.
    /// </summary>
    /// <param name="hr">The <c>HRESULT</c> value.</param>
    static int HRESULT_CODE(HRESULT hr) => hr & 0xFFFF;

    /// <summary>
    /// Creates an <c>HRESULT</c> value from its component pieces.
    /// </summary>
    /// <param name="sev">The severity.</param>
    /// <param name="fac">The facility.</param>
    /// <param name="code">The code.</param>
    /// <returns>
    /// An <c>HRESULT</c> given the severity bit,
    /// facility code, and error code that comprise the <c>HRESULT</c>.
    /// </returns>
    /// <remarks>
    /// <b>Note:</b> Calling <see cref="MAKE_HRESULT"/> for <see cref="HResult.Ok"/>
    /// verification carries a performance penalty.
    /// You should not routinely use <see cref="MAKE_HRESULT"/> for successful results.
    /// </remarks>
    [SuppressMessage("Roslynator", "RCS1213:Remove unused member declaration")]
    static HRESULT MAKE_HRESULT(ULONG sev, ULONG fac, ULONG code) => (HRESULT)((sev << 31) | (fac << 16) | code);

    /// <summary>
    /// Extracts the facility of the specified <c>HRESULT</c>,
    /// which indicates what API or framework originated this error.
    /// </summary>
    /// <param name="hr">The <c>HRESULT</c> value.</param>
    static int HRESULT_FACILITY(HRESULT hr) => (hr >> 16) & 0x1fff;

    /// <summary>
    /// Extracts the severity bit of the <c>HRESULT</c>.
    /// </summary>
    /// <param name="hr">The <c>HRESULT</c>.</param>
    static int HRESULT_SEVERITY(HRESULT hr) => (hr >> 31) & 0x1;

    /// <summary>
    /// Tests the severity bit of the <c>SCODE</c> or <c>HRESULT</c>;
    /// returns <see langword="true"/> if the severity is zero and <see langword="false"/> if it is one.
    /// </summary>
    /// <param name="hr">
    /// The status code. This value can be an <c>HRESULT</c> or an <c>SCODE</c>.
    /// A non-negative number indicates success.
    /// </param>
    static bool SUCCEEDED(HRESULT hr) => hr >= 0;

    /// <summary>
    /// Tests the severity bit of the <c>SCODE</c> or <c>HRESULT</c>;
    /// returns <see langword="true"/> if the severity is one and <see langword="false"/> if it is zero.
    /// </summary>
    /// <param name="hr">
    /// The status code. This value can be an <c>HRESULT</c> or an <c>SCODE</c>.
    /// A negative number indicates failure.
    /// </param>
    static bool FAILED(HRESULT hr) => hr < 0;

    /// <summary>
    /// Provides a generic test for errors on any status value.
    /// </summary>
    /// <param name="status">
    /// The status code.
    /// This value can be an <c>HRESULT</c> or an <c>SCODE</c>.
    /// </param>
    static bool IS_ERROR(HRESULT status) => unchecked((ULONG)status) >> 31 == 1;

    /// <summary>
    /// Maps a <see href="https://learn.microsoft.com/en-us/windows/desktop/Debug/system-error-codes">system error code</see> to an <c>HRESULT</c> value.
    /// </summary>
    /// <param name="sysError">The system error code.</param>
    public static HResult FromWin32(LONG sysError) => (sysError <= 0) ? (HResult)sysError : (HResult)unchecked((HRESULT)((sysError & 0x0000FFFF) | (Win32.Facility.WIN32 << 16) | 0x80000000));

    /// <summary>
    /// Maps an NT status value to an <c>HRESULT</c> value.
    /// </summary>
    /// <param name="ntStatus">The NT status value.</param>
    public static HResult FromNt(LONG ntStatus) => (HResult)unchecked(ntStatus | 0x10000000);  // 0x10000000 = FACILITY_NT_BIT

    #endregion
}

public class HResultException : Exception
{
    [SupportedOSPlatform("windows")]
    public HResultException(HResult hResult)
        : base($"HRESULT ({hResult.Code}) ({hResult.Facility}) ({hResult.Severity}) {hResult.Message}")
    { }
}
