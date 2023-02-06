using System;
using System.Collections.Generic;

// Based on https://github.com/SaladLab/CodeWriter
namespace Maui.BindableProperty.Generator;

public class CodeWriterSettings
{
    /// <summary>
    /// Indentation string. For Tab, "\t". For 4 spaces, "    "
    /// </summary>
    public string Indent;

    /// <summary>
    /// String for begin of block. For C#, "{".
    /// </summary>
    public string BlockBegin;

    /// <summary>
    /// String for end of block. For C#, "}".
    /// </summary>
    public string BlockEnd;

    /// <summary>
    /// String for newline. For Windows, "\r\n".
    /// </summary>
    public string NewLine;

    /// <summary>
    /// NewLine is inserted with new block.
    ///
    /// BlockNewLine: true
    /// <code>
    /// Block
    /// {
    /// }
    /// </code>
    ///
    /// BlockNewLine: false
    /// <code>
    /// Block {
    /// }
    /// </code>
    /// </summary>
    public bool NewLineBeforeBlockBegin;

    /// <summary>
    /// After ToString() called, TranslationMapping replace string.
    /// For example, when TranslationMapping = {"A": "a"} will translate text "ABC" to "aBC".
    /// </summary>
    public Dictionary<string, string> TranslationMapping = new Dictionary<string, string>();

    public CodeWriterSettings()
    {
    }

    public CodeWriterSettings(CodeWriterSettings o)
    {
        Indent = o.Indent;
        BlockBegin = o.BlockBegin;
        BlockEnd = o.BlockEnd;
        NewLine = o.NewLine;
        NewLineBeforeBlockBegin = o.NewLineBeforeBlockBegin;
        TranslationMapping = new Dictionary<string, string>(o.TranslationMapping);
    }

    public static CodeWriterSettings CSharpDefault = new CodeWriterSettings
    {
        Indent = "    ",
        BlockBegin = "{",
        BlockEnd = "}",
        NewLine = Environment.NewLine,
        NewLineBeforeBlockBegin = true,
    };
}
