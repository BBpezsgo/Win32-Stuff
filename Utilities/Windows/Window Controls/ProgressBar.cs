﻿using System.Diagnostics;

namespace Win32
{
    public enum ProgressBarState : uint
    {
        Normal = 0x0001,
        Error = 0x0002,
        Paused = 0x0003,
    }

    public class ProgressBar : Control
    {
        public ProgressBar(
            HWND parent,
            string label,
            RECT rect,
            ushort id
        ) : base(
            parent,
            label,
            ClassName.PROGRESS_BAR,
            WS.VISIBLE | WS.CHILD,
            rect,
            id
        )
        { }

        public ProgressBar(
            Form parent,
            string label,
            RECT rect,
            out ushort id
        ) : this(
            parent.Handle,
            label,
            rect,
            parent.GenerateControlId(out id)
        )
        { }

        public ProgressBar(HWND handle) : base(handle) { }

        public void SetRange(ushort min, ushort max)
        {
            ULONG lParam = Macros.MAKELONG(min, max);
            User32.SendMessage(Handle, PBM.SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
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

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public PBRANGE Range
        {
            get
            {
                PBRANGE result = default;
                User32.SendMessage(Handle, PBM.GETRANGE, WPARAM.Zero, (LPARAM)(&result));
                return result;
            }
            set
            {
                ULONG lParam = Macros.MAKELONG((ushort)value.Low, (ushort)value.High);
                User32.SendMessage(Handle, PBM.SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public uint Step
        {
            get
            {
                LRESULT result = User32.SendMessage(Handle, PBM.GETSTEP, WPARAM.Zero, LPARAM.Zero);
                return unchecked((uint)result.ToInt32());
            }
            set => User32.SendMessage(Handle, PBM.SETSTEP, (WPARAM)value, LPARAM.Zero);
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public ProgressBarState State
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

        public override void HandleNotification(Window parent, ushort code)
        {
            
        }
    }
}