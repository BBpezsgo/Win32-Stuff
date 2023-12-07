using System.Runtime.InteropServices;

namespace Win32.D2D1
{
    /// <summary>
    /// Represents a 3-by-2 matrix.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_MATRIX_3X2_F
    {
        /// <summary>
        /// Horizontal scaling / cosine of rotation
        /// </summary>
        public FLOAT m11;

        /// <summary>
        /// Vertical shear / sine of rotation
        /// </summary>
        public FLOAT m12;

        /// <summary>
        /// Horizontal shear / negative sine of rotation
        /// </summary>
        public FLOAT m21;

        /// <summary>
        /// Vertical scaling / cosine of rotation
        /// </summary>
        public FLOAT m22;

        /// <summary>
        /// Horizontal shift (always orthogonal regardless of rotation)
        /// </summary>
        public FLOAT dx;

        /// <summary>
        /// Vertical shift (always orthogonal regardless of rotation)
        /// </summary>
        public FLOAT dy;
    }
}
