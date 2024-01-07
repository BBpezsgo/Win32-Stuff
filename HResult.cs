using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    /// <summary>
    /// See also <see href="https://learn.microsoft.com/en-us/windows/win32/com/structure-of-com-error-codes">the Microsoft documentation</see>
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public readonly struct HResult : IEquatable<HResult>
    {
        readonly HRESULT code;

        HResult(int code) => this.code = code;

        public readonly bool IsSucceeded => WinErrorMacros.SUCCEEDED(code);
        public readonly bool IsFailed => WinErrorMacros.FAILED(code);
        public readonly bool IsError => WinErrorMacros.IS_ERROR(code);
        /// <summary>
        /// A single letter, 'S' or 'E', that indicates whether the function
        /// call succeeded ('S') or produced an error ('E')
        /// </summary>
        public readonly int Severity => WinErrorMacros.HRESULT_SEVERITY(code);
        /// <summary>
        /// Indicates the system service responsible for the error.
        /// </summary>
        public readonly int FacilityId => WinErrorMacros.HRESULT_FACILITY(code);
        public readonly string? Facility => LowLevel.Facility.ToString(FacilityId);
        /// <summary>
        /// A unique number that is assigned to represent the error or warning.
        /// </summary>
        public readonly int Code => WinErrorMacros.HRESULT_CODE(code);

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
                    uint messageLength = Kernel32.FormatMessage(
                        FormatMessageAttributes.FORMAT_MESSAGE_FROM_SYSTEM |
                        FormatMessageAttributes.FORMAT_MESSAGE_IGNORE_INSERTS,
                        IntPtr.Zero,
                        (uint)code,
                        LanguageMacros.LANG_SYSTEM_DEFAULT,
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
        public const HRESULT ABORT = unchecked((HRESULT)0x80004004);
        /// <summary>
        /// A general access-denied error.
        /// </summary>
        public const HRESULT ACCESSDENIED = unchecked((HRESULT)0x80070005);
        /// <summary>
        /// An unspecified failure has occurred.
        /// </summary>
        public const HRESULT FAIL = unchecked((HRESULT)0x80004005);
        /// <summary>
        /// An invalid handle was used.
        /// </summary>
        public const HRESULT HANDLE = unchecked((HRESULT)0x80070006);
        /// <summary>
        /// One or more arguments are invalid.
        /// </summary>
        public const HRESULT INVALIDARG = unchecked((HRESULT)0x80070057);
        /// <summary>
        /// The <c>QueryInterface</c> method did not recognize the requested interface.
        /// The interface is not supported.
        /// </summary>
        public const HRESULT NOINTERFACE = unchecked((HRESULT)0x80004002);
        /// <summary>
        /// The method is not implemented.
        /// </summary>
        public const HRESULT NOTIMPL = unchecked((HRESULT)0x80004001);
        /// <summary>
        /// The method failed to allocate necessary memory.
        /// </summary>
        public const HRESULT OUTOFMEMORY = unchecked((HRESULT)0x8007000E);
        /// <summary>
        /// The data necessary to complete the operation is not yet available.
        /// </summary>
        public const HRESULT PENDING = unchecked((HRESULT)0x8000000A);
        /// <summary>
        /// An invalid pointer was used.
        /// </summary>
        public const HRESULT INVALID_POINTER = unchecked((HRESULT)0x80004003);
        /// <summary>
        /// A catastrophic failure has occurred.
        /// </summary>
        public const HRESULT UNEXPECTED = unchecked((HRESULT)0x8000FFFF);
        /// <summary>
        /// The method succeeded and returned the boolean value <see cref="Constants.FALSE"/>.
        /// </summary>
        public const HRESULT FALSE = unchecked((HRESULT)0x00000001);
        /// <summary>
        /// The method succeeded.
        /// If a boolean return value is expected, the returned value is <see cref="TRUE"/>.
        /// </summary>
        public const HRESULT OK = unchecked((HRESULT)0x00000000);

        #endregion
    }

    public class HResultException : Exception
    {
        [SupportedOSPlatform("windows")]
        public HResultException(HResult hResult)
            : base($"HRESULT ({hResult.Code}) ({hResult.Facility}) ({hResult.Severity}) {hResult.Message}")
        { }
    }
}
