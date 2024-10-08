﻿namespace Win32.Forms;

public enum ProgressBarState : uint
{
    Normal = 0x0001,
    Error = 0x0002,
    Paused = 0x0003,
}

[SupportedOSPlatform("windows")]
public class ProgressBar : Control
{
    public ProgressBar(
        Form parent,
        string label,
        RECT rect
    ) : base(
        parent,
        label,
        Forms.ClassName.ProgressBar,
        WindowStyles.VISIBLE | WindowStyles.CHILD,
        rect,
        parent.GenerateControlId()
    )
    { }

    public ProgressBar(HWND handle) : base(handle) { }

    public void SetRange(ushort min, ushort max)
    {
        ULONG lParam = BitUtils.MakeLong(min, max);
        User32.SendMessage(Handle, ProgressBarControlMessage.SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public uint Value
    {
        get
        {
            LRESULT result = User32.SendMessage(Handle, ProgressBarControlMessage.GETPOS, WPARAM.Zero, LPARAM.Zero);
            return unchecked((uint)result.ToInt32());
        }
        set => User32.SendMessage(Handle, ProgressBarControlMessage.SETPOS, (WPARAM)value, LPARAM.Zero);
    }

    public int GetLowLimit()
        => User32.SendMessage(Handle, ProgressBarControlMessage.GETRANGE, (WPARAM)TRUE, LPARAM.Zero).ToInt32();

    public int GetHighLimit()
        => User32.SendMessage(Handle, ProgressBarControlMessage.GETRANGE, (WPARAM)FALSE, LPARAM.Zero).ToInt32();

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe PBRANGE Range
    {
        get
        {
            PBRANGE result = default;
            User32.SendMessage(Handle, ProgressBarControlMessage.GETRANGE, WPARAM.Zero, (LPARAM)(&result));
            return result;
        }
        set
        {
            ULONG lParam = BitUtils.MakeLong((ushort)value.Low, (ushort)value.High);
            User32.SendMessage(Handle, ProgressBarControlMessage.SETRANGE, WPARAM.Zero, unchecked((LPARAM)lParam));
        }
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public uint Step
    {
        get
        {
            LRESULT result = User32.SendMessage(Handle, ProgressBarControlMessage.GETSTEP, WPARAM.Zero, LPARAM.Zero);
            return unchecked((uint)result.ToInt32());
        }
        set => User32.SendMessage(Handle, ProgressBarControlMessage.SETSTEP, (WPARAM)value, LPARAM.Zero);
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public ProgressBarState State
    {
        get
        {
            LRESULT result = User32.SendMessage(Handle, ProgressBarControlMessage.GETSTATE, WPARAM.Zero, LPARAM.Zero);
            return (ProgressBarState)unchecked((uint)result.ToInt32());
        }
        set => User32.SendMessage(Handle, ProgressBarControlMessage.SETSTATE, (WPARAM)(uint)value, LPARAM.Zero).ToInt32();
    }

    public void Add(uint value)
        => User32.SendMessage(Handle, ProgressBarControlMessage.DELTAPOS, (WPARAM)value, LPARAM.Zero);

    public void StepIt()
        => User32.SendMessage(Handle, ProgressBarControlMessage.STEPIT, WPARAM.Zero, LPARAM.Zero);
}
