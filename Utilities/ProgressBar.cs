using System.Diagnostics;

namespace Win32.Utilities
{
    public enum ProgressBarState : uint
    {
        Normal = 0x0001,
        Error = 0x0002,
        Paused = 0x0003,
    }

    public class ProgressBar : Control
    {
        public delegate void SimpleEventHandler(ProgressBar sender);

        unsafe public ProgressBar(
            HWND parent,
            string label,
            int x,
            int y,
            int width,
            int height,
            ushort id)
        {
            Handle = Control.AnyHandle(
                        parent,
                        label,
                        ClassName.PROGRESS_BAR,
                        WS.WS_VISIBLE | WS.WS_CHILD,
                        x,
                        y,
                        width,
                        height,
                        id);
        }

        public void SetRange(ushort min, ushort max)
        {
            ULONG lParam = Macros.MAKELONG(min, max);
            User32.SendMessage(Handle, PBM.PBM_SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public uint Position
        {
            get
            {
                LRESULT result = User32.SendMessage(Handle, PBM.PBM_GETPOS, WPARAM.Zero, LPARAM.Zero);
                return unchecked((uint)result.ToInt32());
            }
            set
            {
                User32.SendMessage(Handle, PBM.PBM_SETPOS, (WPARAM)value, LPARAM.Zero);
            }
        }

        public int GetLowLimit()
            => User32.SendMessage(Handle, PBM.PBM_GETRANGE, (WPARAM)TRUE, LPARAM.Zero).ToInt32();

        public int GetHighLimit()
            => User32.SendMessage(Handle, PBM.PBM_GETRANGE, (WPARAM)FALSE, LPARAM.Zero).ToInt32();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public PBRANGE Range
        {
            get
            {
                PBRANGE result = new();
                User32.SendMessage(Handle, PBM.PBM_GETRANGE, WPARAM.Zero, (LPARAM)(&result));
                return result;
            }
            set
            {
                ULONG lParam = Macros.MAKELONG((ushort)value.Low, (ushort)value.High);
                User32.SendMessage(Handle, PBM.PBM_SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public uint Step
        {
            get
            {
                LRESULT result = User32.SendMessage(Handle, PBM.PBM_GETSTEP, WPARAM.Zero, LPARAM.Zero);
                return unchecked((uint)result.ToInt32());
            }
            set
            {
                User32.SendMessage(Handle, PBM.PBM_SETSTEP, (WPARAM)value, LPARAM.Zero);
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public ProgressBarState State
        {
            get
            {
                LRESULT result = User32.SendMessage(Handle, PBM.PBM_GETSTATE, WPARAM.Zero, LPARAM.Zero);
                return (ProgressBarState)unchecked((uint)result.ToInt32());
            }
            set
            {
                User32.SendMessage(Handle, PBM.PBM_SETSTATE, (WPARAM)(uint)value, LPARAM.Zero).ToInt32();
            }
        }

        public void Add(uint value)
        {
            User32.SendMessage(Handle, PBM.PBM_DELTAPOS, (WPARAM)value, LPARAM.Zero);
        }

        public void StepIt()
        {
            User32.SendMessage(Handle, PBM.PBM_STEPIT, WPARAM.Zero, LPARAM.Zero);
        }
    }
}
