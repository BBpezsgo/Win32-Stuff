﻿namespace Win32.DWrite;

/// <summary>
/// The root factory interface for all DWrite objects.
/// </summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("b859ee5a-d838-4b5b-a2e8-1adc7d93db48")]
[SupportedOSPlatform("windows")]
public unsafe interface IDWriteFactory
{
    /// <summary>
    /// Gets a font collection representing the set of installed fonts.
    /// </summary>
    /// <param name="fontCollection">Receives a pointer to the system font collection object, or NULL in case of failure.</param>
    /// <param name="checkForUpdates">If this parameter is nonzero, the function performs an immediate check for changes to the set of
    /// installed fonts. If this parameter is FALSE, the function will still detect changes if the font cache service is running, but
    /// there may be some latency. For example, an application might specify TRUE if it has itself just installed a font and wants to
    /// be sure the font collection contains that font.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT GetSystemFontCollection(
        [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontCollection? fontCollection,
        BOOL checkForUpdates = FALSE
    );

    /// <summary>
    /// Creates a font collection using a custom font collection loader.
    /// </summary>
    /// <param name="collectionLoader">Application-defined font collection loader, which must have been previously
    /// registered using RegisterFontCollectionLoader.</param>
    /// <param name="collectionKey">Key used by the loader to identify a collection of font files.</param>
    /// <param name="collectionKeySize">Size in bytes of the collection key.</param>
    /// <param name="fontCollection">Receives a pointer to the system font collection object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateCustomFontCollection(
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteFontCollectionLoader collectionLoader,
        void* collectionKey,
        UINT32 collectionKeySize,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontCollection fontCollection
    );

    /// <summary>
    /// Registers a custom font collection loader with the factory object.
    /// </summary>
    /// <param name="fontCollectionLoader">Application-defined font collection loader.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT RegisterFontCollectionLoader(
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteFontCollectionLoader fontCollectionLoader
    );

    /// <summary>
    /// Unregisters a custom font collection loader that was previously registered using RegisterFontCollectionLoader.
    /// </summary>
    /// <param name="fontCollectionLoader">Application-defined font collection loader.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT UnregisterFontCollectionLoader(
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteFontCollectionLoader fontCollectionLoader
    );

    /// <summary>
    /// CreateFontFileReference creates a font file reference object from a local font file.
    /// </summary>
    /// <param name="filePath">Absolute file path. Subsequent operations on the constructed object may fail
    /// if the user provided filePath doesn't correspond to a valid file on the disk.</param>
    /// <param name="lastWriteTime">Last modified time of the input file path. If the parameter is omitted,
    /// the function will access the font file to obtain its last write time, so the clients are encouraged to specify this value
    /// to avoid extra disk access. Subsequent operations on the constructed object may fail
    /// if the user provided lastWriteTime doesn't match the file on the disk.</param>
    /// <param name="fontFile">Contains newly created font file reference object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateFontFileReference(
        [In] WCHAR* filePath,
        [In] FileTime* lastWriteTime,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontFile fontFile
    );

    /// <summary>
    /// CreateCustomFontFileReference creates a reference to an application specific font file resource.
    /// This function enables an application or a document to use a font without having to install it on the system.
    /// The fontFileReferenceKey has to be unique only in the scope of the fontFileLoader used in this call.
    /// </summary>
    /// <param name="fontFileReferenceKey">Font file reference key that uniquely identifies the font file resource
    /// during the lifetime of fontFileLoader.</param>
    /// <param name="fontFileReferenceKeySize">Size of font file reference key in bytes.</param>
    /// <param name="fontFileLoader">Font file loader that will be used by the font system to load data from the file identified by
    /// fontFileReferenceKey.</param>
    /// <param name="fontFile">Contains the newly created font file object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    /// <remarks>
    /// This function is provided for cases when an application or a document needs to use a font
    /// without having to install it on the system. fontFileReferenceKey has to be unique only in the scope
    /// of the fontFileLoader used in this call.
    /// </remarks>
    abstract HRESULT CreateCustomFontFileReference(
        void* fontFileReferenceKey,
        UINT32 fontFileReferenceKeySize,
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteFontFileLoader fontFileLoader,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontFile fontFile
    );

    /// <summary>
    /// Creates a font face object.
    /// </summary>
    /// <param name="fontFaceType">The file format of the font face.</param>
    /// <param name="numberOfFiles">The number of font files required to represent the font face.</param>
    /// <param name="fontFiles">Font files representing the font face. Since IDWriteFontFace maintains its own references
    /// to the input font file objects, it's OK to release them after this call.</param>
    /// <param name="faceIndex">The zero based index of a font face in cases when the font files contain a collection of font faces.
    /// If the font files contain a single face, this value should be zero.</param>
    /// <param name="fontFaceSimulationFlags">Font face simulation flags for algorithmic emboldening and italicization.</param>
    /// <param name="fontFace">Contains the newly created font face object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateFontFace(
        FontFaceType fontFaceType,
        UINT32 numberOfFiles,
        [MarshalAs(UnmanagedType.IUnknown)] ref IDWriteFontFile fontFiles,
        UINT32 faceIndex,
        FontSimulations fontFaceSimulationFlags,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object fontFace
    );

    /// <summary>
    /// Creates a rendering parameters object with default settings for the primary monitor.
    /// </summary>
    /// <param name="renderingParams">Holds the newly created rendering parameters object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateRenderingParams(
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object renderingParams
    );

    /// <summary>
    /// Creates a rendering parameters object with default settings for the specified monitor.
    /// </summary>
    /// <param name="monitor">The monitor to read the default values from.</param>
    /// <param name="renderingParams">Holds the newly created rendering parameters object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateMonitorRenderingParams(
        HMONITOR monitor,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object renderingParams
    );

    /// <summary>
    /// Creates a rendering parameters object with the specified properties.
    /// </summary>
    /// <param name="gamma">The gamma value used for gamma correction, which must be greater than zero and cannot exceed 256.</param>
    /// <param name="enhancedContrast">The amount of contrast enhancement, zero or greater.</param>
    /// <param name="clearTypeLevel">The degree of ClearType level, from 0.0f (no ClearType) to 1.0f (full ClearType).</param>
    /// <param name="pixelGeometry">The geometry of a device pixel.</param>
    /// <param name="renderingMode">Method of rendering glyphs. In most cases, this should be DWRITE_RENDERING_MODE_DEFAULT to automatically use an appropriate mode.</param>
    /// <param name="renderingParams">Holds the newly created rendering parameters object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateCustomRenderingParams(
        FLOAT gamma,
        FLOAT enhancedContrast,
        FLOAT clearTypeLevel,
        PixelGeometry pixelGeometry,
        RenderingMode renderingMode,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object renderingParams
    );

    /// <summary>
    /// Registers a font file loader with DirectWrite.
    /// </summary>
    /// <param name="fontFileLoader">Pointer to the implementation of the IDWriteFontFileLoader for a particular file resource type.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    /// <remarks>
    /// This function registers a font file loader with DirectWrite.
    /// Font file loader interface handles loading font file resources of a particular type from a key.
    /// The font file loader interface is recommended to be implemented by a singleton object.
    /// A given instance can only be registered once.
    /// Succeeding attempts will return an error that it has already been registered.
    /// IMPORTANT: font file loader implementations must not register themselves with DirectWrite
    /// inside their constructors and must not unregister themselves in their destructors, because
    /// registration and unregistration operations increment and decrement the object reference count respectively.
    /// Instead, registration and unregistration of font file loaders with DirectWrite should be performed
    /// outside of the font file loader implementation as a separate step.
    /// </remarks>
    abstract HRESULT RegisterFontFileLoader(
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteFontFileLoader fontFileLoader
    );

    /// <summary>
    /// Unregisters a font file loader that was previously registered with the DirectWrite font system using RegisterFontFileLoader.
    /// </summary>
    /// <param name="fontFileLoader">Pointer to the file loader that was previously registered with the DirectWrite font system using RegisterFontFileLoader.</param>
    /// <returns>
    /// This function will succeed if the user loader is requested to be removed.
    /// It will fail if the pointer to the file loader identifies a standard DirectWrite loader,
    /// or a loader that is never registered or has already been unregistered.
    /// </returns>
    /// <remarks>
    /// This function unregisters font file loader callbacks with the DirectWrite font system.
    /// The font file loader interface is recommended to be implemented by a singleton object.
    /// IMPORTANT: font file loader implementations must not register themselves with DirectWrite
    /// inside their constructors and must not unregister themselves in their destructors, because
    /// registration and unregistration operations increment and decrement the object reference count respectively.
    /// Instead, registration and unregistration of font file loaders with DirectWrite should be performed
    /// outside of the font file loader implementation as a separate step.
    /// </remarks>
    abstract HRESULT UnregisterFontFileLoader(
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteFontFileLoader fontFileLoader
    );

    /// <summary>
    /// Create a text format object used for text layout.
    /// </summary>
    /// <param name="fontFamilyName">Name of the font family</param>
    /// <param name="fontCollection">Font collection. NULL indicates the system font collection.</param>
    /// <param name="fontWeight">Font weight</param>
    /// <param name="fontStyle">Font style</param>
    /// <param name="fontStretch">Font stretch</param>
    /// <param name="fontSize">Logical size of the font in DIP units. A DIP ("device-independent pixel") equals 1/96 inch.</param>
    /// <param name="localeName">Locale name</param>
    /// <param name="textFormat">Contains newly created text format object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    /// <remarks>
    /// If fontCollection is nullptr, the system font collection is used, grouped by typographic family name
    /// (DWRITE_FONT_FAMILY_MODEL_WEIGHT_STRETCH_STYLE) without downloadable fonts.
    /// </remarks>
    abstract HRESULT CreateTextFormat(
        [In] WCHAR* fontFamilyName,
        [In, Optional, MarshalAs(UnmanagedType.IUnknown)] IDWriteFontCollection? fontCollection,
        FontWeight fontWeight,
        FontStyle fontStyle,
        FontStretch fontStretch,
        FLOAT fontSize,
        [In] WCHAR* localeName,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteTextFormat? textFormat
    );

    /// <summary>
    /// Create a typography object used in conjunction with text format for text layout.
    /// </summary>
    /// <param name="typography">Contains newly created typography object, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateTypography(
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object typography
    );

    /// <summary>
    /// Create an object used for interoperability with GDI.
    /// </summary>
    /// <param name="gdiInterop">Receives the GDI interop object if successful, or NULL in case of failure.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT GetGdiInterop(
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object gdiInterop
    );

    /// <summary>
    /// CreateTextLayout takes a string, format, and associated constraints
    /// and produces an object representing the fully analyzed
    /// and formatted result.
    /// </summary>
    /// <param name="text">The string to layout.</param>
    /// <param name="textLength">The length of the string.</param>
    /// <param name="textFormat">The format to apply to the string.</param>
    /// <param name="maxWidth">Width of the layout box.</param>
    /// <param name="maxHeight">Height of the layout box.</param>
    /// <param name="textLayout">The resultant object.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateTextLayout(
        WCHAR* text,
        UINT32 textLength,
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteTextFormat textFormat,
        FLOAT maxWidth,
        FLOAT maxHeight,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object textLayout
    );

    /// <summary>
    /// CreateGdiCompatibleTextLayout takes a string, format, and associated constraints
    /// and produces and object representing the result formatted for a particular display resolution
    /// and measuring mode. The resulting text layout should only be used for the intended resolution,
    /// and for cases where text scalability is desired, CreateTextLayout should be used instead.
    /// </summary>
    /// <param name="text">The string to layout.</param>
    /// <param name="textLength">The length of the string.</param>
    /// <param name="textFormat">The format to apply to the string.</param>
    /// <param name="layoutWidth">Width of the layout box.</param>
    /// <param name="layoutHeight">Height of the layout box.</param>
    /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if rendering onto a 96 DPI device then pixelsPerDip
    /// is 1. If rendering onto a 120 DPI device then pixelsPerDip is 120/96.</param>
    /// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the
    /// scaling specified the font size and pixelsPerDip.</param>
    /// <param name="useGdiNatural">
    /// When set to FALSE, instructs the text layout to use the same metrics as GDI aliased text.
    /// When set to TRUE, instructs the text layout to use the same metrics as text measured by GDI using a font
    /// created with CLEARTYPE_NATURAL_QUALITY.
    /// </param>
    /// <param name="textLayout">The resultant object.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateGdiCompatibleTextLayout(
        WCHAR* text,
        UINT32 textLength,
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteTextFormat textFormat,
        FLOAT layoutWidth,
        FLOAT layoutHeight,
        FLOAT pixelsPerDip,
        [In] Matrix* transform,
        BOOL useGdiNatural,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object textLayout
    );

    /// <summary>
    /// The application may call this function to create an inline object for trimming, using an ellipsis as the omission sign.
    /// The ellipsis will be created using the current settings of the format, including base font, style, and any effects.
    /// Alternate omission signs can be created by the application by implementing IDWriteInlineObject.
    /// </summary>
    /// <param name="textFormat">Text format used as a template for the omission sign.</param>
    /// <param name="trimmingSign">Created omission sign.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateEllipsisTrimmingSign(
        [In, MarshalAs(UnmanagedType.IUnknown)] IDWriteTextFormat textFormat,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object trimmingSign
    );

    /// <summary>
    /// Return an interface to perform text analysis with.
    /// </summary>
    /// <param name="textAnalyzer">The resultant object.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateTextAnalyzer(
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object textAnalyzer
    );

    /// <summary>
    /// Creates a number substitution object using a locale name,
    /// substitution method, and whether to ignore user overrides (uses NLS
    /// defaults for the given culture instead).
    /// </summary>
    /// <param name="substitutionMethod">Method of number substitution to use.</param>
    /// <param name="localeName">Which locale to obtain the digits from.</param>
    /// <param name="ignoreUserOverride">Ignore the user's settings and use the locale defaults</param>
    /// <param name="numberSubstitution">Receives a pointer to the newly created object.</param>
    abstract HRESULT CreateNumberSubstitution(
        [In] NumberSubstitutionMethod substitutionMethod,
        [In] WCHAR* localeName,
        [In] BOOL ignoreUserOverride,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object numberSubstitution
    );

    /// <summary>
    /// Creates a glyph run analysis object, which encapsulates information
    /// used to render a glyph run.
    /// </summary>
    /// <param name="glyphRun">Structure specifying the properties of the glyph run.</param>
    /// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if rendering onto a 96 DPI bitmap then pixelsPerDip
    /// is 1. If rendering onto a 120 DPI bitmap then pixelsPerDip is 120/96.</param>
    /// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the
    /// scaling specified by the emSize and pixelsPerDip.</param>
    /// <param name="renderingMode">Specifies the rendering mode, which must be one of the raster rendering modes (i.e., not default
    /// and not outline).</param>
    /// <param name="measuringMode">Specifies the method to measure glyphs.</param>
    /// <param name="baselineOriginX">Horizontal position of the baseline origin, in DIPs.</param>
    /// <param name="baselineOriginY">Vertical position of the baseline origin, in DIPs.</param>
    /// <param name="glyphRunAnalysis">Receives a pointer to the newly created object.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract HRESULT CreateGlyphRunAnalysis(
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        [In] GlyphRun* glyphRun,
#pragma warning restore CS8500 
        FLOAT pixelsPerDip,
        [In] Matrix* transform,
        RenderingMode renderingMode,
        MeasuringMode measuringMode,
        FLOAT baselineOriginX,
        FLOAT baselineOriginY,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object glyphRunAnalysis
    );
}
