namespace Win32;

public enum ThreadInformationClass : int
{
    ThreadMemoryPriority = 0,
    ThreadAbsoluteCpuPriority = 1,
    ThreadDynamicCodePolicy = 2,
    ThreadPowerThrottling = 3,
    ThreadInformationClassMax = 4,
}
