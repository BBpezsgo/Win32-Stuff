namespace Win32;

public static class VirtualKeyboard
{
    [MemberNotNullWhen(true, nameof(PromptMessage))]
    public static bool ShouldShowUp => PromptMessage is not null;
    public static string? PromptMessage { get; private set; }
    public static string? DefaultValue { get; private set; }

    static Action<string>? Response;

    public static void Show(string prompt, Action<string> response, string? defaultValue = null)
    {
        PromptMessage = prompt;
        DefaultValue = defaultValue;
        Response = response;
    }

    public static void Submit(string value)
    {
        Response?.Invoke(value);
    }

    public static void Hide()
    {
        PromptMessage = null;
        DefaultValue = null;
        Response = null;
    }
}
