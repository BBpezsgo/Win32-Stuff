namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public readonly struct SystemTime
{
    public readonly WORD Year;
    public readonly WORD Month;
    public readonly WORD DayOfWeek;
    public readonly WORD Day;
    public readonly WORD Hour;
    public readonly WORD Minute;
    public readonly WORD Second;
    public readonly WORD Milliseconds;

    public static explicit operator DateTime(SystemTime v) => new(v.Year, v.Month, v.Day, v.Hour, v.Minute, v.Second, v.Milliseconds);
}
