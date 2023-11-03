using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32
{
    public static partial class TPM
    {
        public const int LEFTBUTTON = 0x0000;
        public const int RIGHTBUTTON = 0x0002;

        // #if(WINVER >= 0x0400)

        /* Don't send any notification messages */
        public const int NONOTIFY = 0x0080;
        public const int RETURNCMD = 0x0100;
        // #endif /* WINVER >= 0x0400 */

        // #if(WINVER >= 0x0500)
        public const int RECURSE = 0x0001;
        public const int HORPOSANIMATION = 0x0400;
        public const int HORNEGANIMATION = 0x0800;
        public const int VERPOSANIMATION = 0x1000;
        public const int VERNEGANIMATION = 0x2000;

        // #if(_WIN32_WINNT >= 0x0500)
        public const int NOANIMATION = 0x4000;
        // #endif /* _WIN32_WINNT >= 0x0500 */

        // #if(_WIN32_WINNT >= 0x0501)
        public const int LAYOUTRTL = 0x8000;
        // #endif /* _WIN32_WINNT >= 0x0501 */

        // #endif /* WINVER >= 0x0500 */
    }
}
