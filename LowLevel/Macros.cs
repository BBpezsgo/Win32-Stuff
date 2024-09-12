namespace Win32;

public static class IntResource
{
    public static bool Is(ULONG_PTR r) => (((ULONG)r) >> 16) == 0;
    public static bool Is(ULONG r) => (r >> 16) == 0;

    public static unsafe CHAR* MakeA(WORD i) => (CHAR*)(ULONG_PTR)i;
    public static unsafe WCHAR* MakeW(WORD i) => (WCHAR*)(ULONG_PTR)i;
}

public static class Locale
{
    public const int LocaleNameMaxLength = 85;
    public static readonly ULONG SystemDefault = Make(PrimaryLanguage.SystemDefault, LanguageSort.Default);
    public static readonly ULONG UserDefault = Make(PrimaryLanguage.UserDefault, LanguageSort.Default);
    public static readonly ULONG Neutral = Make(PrimaryLanguage.Make(PrimaryLanguage.Neutral, SubLanguage.Neutral), LanguageSort.Default);
    public static readonly ULONG Invariant = Make(PrimaryLanguage.Make(PrimaryLanguage.Invariant, SubLanguage.Neutral), LanguageSort.Default);
    public static readonly ULONG CustomDefault = Make(PrimaryLanguage.Make(PrimaryLanguage.Neutral, SubLanguage.CustomDefault), LanguageSort.Default);
    public static readonly ULONG CustomUnspecified = Make(PrimaryLanguage.Make(PrimaryLanguage.Neutral, SubLanguage.CustomUnspecified), LanguageSort.Default);
    public static readonly ULONG CustomUIDefault = Make(PrimaryLanguage.Make(PrimaryLanguage.Neutral, SubLanguage.CustomUIDefault), LanguageSort.Default);

    public static ULONG Make(WORD language, WORD sort) => BitUtils.MakeLong(language, sort);
}

public static class SubLanguage
{
    /// <summary>
    /// Neutral sublanguage
    /// </summary>
    public const WORD Neutral = 0x00;
    /// <summary>
    /// Invariant sublanguage
    /// </summary>
    public const WORD Invariant = 0x00;
    /// <summary>
    /// User default sublanguage
    /// </summary>
    public const WORD Default = 0x01;
    /// <summary>
    /// System default sublanguage
    /// </summary>
    public const WORD SystemDefault = 0x02;
    /// <summary>
    /// Default custom sublanguage
    /// </summary>
    public const WORD CustomDefault = 0x03;
    /// <summary>
    /// Unspecified custom sublanguage
    /// </summary>
    public const WORD CustomUnspecified = 0x04;
    /// <summary>
    /// Default custom MUI sublanguage
    /// </summary>
    public const WORD CustomUIDefault = 0x05;

    public static WORD GetId(WORD language) => (WORD)(language >> 10);
}

public static class PrimaryLanguage
{
    /// <summary>
    /// Default custom (MUI) locale language
    /// </summary>
    public const WORD Neutral = 0x00;

    /// <summary>
    /// User default locale language
    /// </summary>
    // public const WORD UserDefault = 0x01;
    public static readonly WORD UserDefault = Make(PrimaryLanguage.Neutral, SubLanguage.Default);

    /// <summary>
    /// System default locale language
    /// </summary>
    // public const WORD SystemDefault = 0x02;
    public static readonly WORD SystemDefault = Make(PrimaryLanguage.Neutral, SubLanguage.SystemDefault);

    /// <summary>
    /// Invariant locale language
    /// </summary>
    public const WORD Invariant = 0x7F;

    public static WORD FromLocaleId(WORD localeId) => localeId;
    public static WORD FromLocaleId(DWORD localeId) => (WORD)((localeId >> 16) & 0b_1111);
    public static WORD GetPrimaryLangId(WORD language) => (WORD)(language & 0b_0000_0011_1111_1111); // 0x3ff

    public static WORD Make(WORD primary, WORD secondary) => (WORD)((secondary << 10) | primary);
}

public static class LanguageSort
{
    /// <summary>
    /// Sorting default
    /// </summary>
    public const WORD Default = 0x0;
    /// <summary>
    /// Invariant (Mathematical Symbols)
    /// </summary>
    public const WORD InvariantMath = 0x1;
}
