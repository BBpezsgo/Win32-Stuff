﻿using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public readonly struct HResult : IEquatable<HResult>
    {
        readonly HRESULT code;

        public HResult(int code) => this.code = code;

        public readonly bool IsSucceeded => WinErrorMacros.SUCCEEDED(code);
        public readonly bool IsFailed => WinErrorMacros.FAILED(code);
        public readonly bool IsError => WinErrorMacros.IS_ERROR(code);
        public readonly int Severity => WinErrorMacros.HRESULT_SEVERITY(code);
        public readonly int FacilityId => WinErrorMacros.HRESULT_FACILITY(code);
        public readonly string? Facility => FACILITY.ToString(FacilityId);
        public readonly int Code => WinErrorMacros.HRESULT_CODE(code);

        public override bool Equals(object? obj) => obj is HResult result && Equals(result);
        public bool Equals(HResult other) => code == other.code;

        public override int GetHashCode() => HashCode.Combine(code);

        public override string ToString() => code.ToString(CultureInfo.InvariantCulture);
        readonly string GetDebuggerDisplay() => ToString();

        public static bool operator ==(HResult left, HResult right) => left.Equals(right);
        public static bool operator !=(HResult left, HResult right) => !left.Equals(right);

        public static implicit operator HRESULT(HResult hr) => hr.code;
        public static implicit operator HResult(HRESULT hr) => new(hr);

        const int MESSAGE_BUFFER_SIZE = 255;

        unsafe public string? Message
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
    }
}