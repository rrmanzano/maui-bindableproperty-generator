#pragma warning disable SA1300 // Element must begin with upper-case letter

// Based on https://github.com/SaladLab/CodeWriter
namespace Maui.BindableProperty.Generator;

public static class CodeWriterExtensions
{
    public static void _(this CodeWriter w, string str = null)
    {
        w.Write(str);
    }

    public static void _(this CodeWriter w, string str, params string[] strs)
    {
        w.Write(str, strs);
    }

    public static UsingHandle b(this CodeWriter w, params string[] strs)
    {
        return w.OpenBlock(strs, newLineAfterBlockEnd: false);
    }

    public static UsingHandle B(this CodeWriter w, params string[] strs)
    {
        return w.OpenBlock(strs, newLineAfterBlockEnd: true);
    }

    public static UsingHandle i(this CodeWriter w, string begin = null, string end = null)
    {
        return w.OpenIndent(begin, end, newLineAfterBlockEnd: false);
    }

    public static UsingHandle I(this CodeWriter w, string begin = null, string end = null)
    {
        return w.OpenIndent(begin, end, newLineAfterBlockEnd: true);
    }
}
