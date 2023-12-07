namespace Win32.D2D1
{
    /// <summary>
    /// Describes how present should behave.
    /// </summary>
    public enum D2D1_PRESENT_OPTIONS : DWORD
    {
        NONE = 0x00000000,

        /// <summary>
        /// Keep the target contents intact through present.
        /// </summary>
        RETAIN_CONTENTS = 0x00000001,

        /// <summary>
        /// Do not wait for display refresh to commit changes to display.
        /// </summary>
        IMMEDIATELY = 0x00000002,
    }
}
