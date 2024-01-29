using System.Diagnostics.CodeAnalysis;

namespace Win32
{
    public static class VirtualKeyboard
    {
        static string? _prompt;
        static string? _defaultValue;
        static Action<string>? _response;

        [MemberNotNullWhen(true, nameof(PromptMessage))]
        public static bool ShouldShowUp => _prompt is not null;
        public static string? PromptMessage => _prompt;
        public static string? DefaultValue => _defaultValue;

        public static void Show(string prompt, Action<string> response, string? defaultValue = null)
        {
            _prompt = prompt;
            _defaultValue = defaultValue;
            _response = response;
        }

        public static void Submit(string value)
        {
            _response?.Invoke(value);
        }

        public static void Hide()
        {
            _prompt = null;
            _defaultValue = null;
            _response = null;
        }
    }
}
