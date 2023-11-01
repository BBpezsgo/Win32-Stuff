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
                        WS.VISIBLE | WS.CHILD,
                        x,
                        y,
                        width,
                        height,
                        id);
        }

        public void SetRange(ushort min, ushort max)
        {
            ULONG lParam = Macros.MAKELONG(min, max);
            User32.SendMessage(Handle, PBM.SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public uint Value
        {
            get
            {
                LRESULT result = User32.SendMessage(Handle, PBM.GETPOS, WPARAM.Zero, LPARAM.Zero);
                return unchecked((uint)result.ToInt32());
            }
            set => User32.SendMessage(Handle, PBM.SETPOS, (WPARAM)value, LPARAM.Zero);
        }

        public int GetLowLimit()
            => User32.SendMessage(Handle, PBM.GETRANGE, (WPARAM)TRUE, LPARAM.Zero).ToInt32();

        public int GetHighLimit()
            => User32.SendMessage(Handle, PBM.GETRANGE, (WPARAM)FALSE, LPARAM.Zero).ToInt32();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public PBRANGE Range
        {
            get
            {
                PBRANGE result = new();
                User32.SendMessage(Handle, PBM.GETRANGE, WPARAM.Zero, (LPARAM)(&result));
                return result;
            }
            set
            {
                ULONG lParam = Macros.MAKELONG((ushort)value.Low, (ushort)value.High);
                User32.SendMessage(Handle, PBM.SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public uint Step
        {
            get
            {
                LRESULT result = User32.SendMessage(Handle, PBM.GETSTEP, WPARAM.Zero, LPARAM.Zero);
                return unchecked((uint)result.ToInt32());
            }
            set => User32.SendMessage(Handle, PBM.SETSTEP, (WPARAM)value, LPARAM.Zero);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        unsafe public ProgressBarState State
        {
            get
            {
                LRESULT result = User32.SendMessage(Handle, PBM.GETSTATE, WPARAM.Zero, LPARAM.Zero);
                return (ProgressBarState)unchecked((uint)result.ToInt32());
            }
            set => User32.SendMessage(Handle, PBM.SETSTATE, (WPARAM)(uint)value, LPARAM.Zero).ToInt32();
        }

        public void Add(uint value)
            => User32.SendMessage(Handle, PBM.DELTAPOS, (WPARAM)value, LPARAM.Zero);

        public void StepIt()
            => User32.SendMessage(Handle, PBM.STEPIT, WPARAM.Zero, LPARAM.Zero);
    }
}
