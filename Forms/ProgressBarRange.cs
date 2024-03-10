global using PBRANGE = Win32.Forms.ProgressBarRange;

namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public readonly struct ProgressBarRange
{
    public readonly int Low;
    public readonly int High;

    public ProgressBarRange(int low, int high)
    {
        Low = low;
        High = high;
    }

    public static implicit operator ValueTuple<int, int>(PBRANGE v) => (v.Low, v.High);
    public static implicit operator PBRANGE(ValueTuple<int, int> v) => new(v.Item1, v.Item2);
}
