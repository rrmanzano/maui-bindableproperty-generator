using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Based on https://github.com/SaladLab/CodeWriter
namespace Maui.BindableProperty.Generator
{
    public class CodeWriter
    {
        private readonly CodeWriterSettings _settings;
        private readonly StringBuilder _sb;
        private int _indent;
        private bool _newLineOnBlockEnd;

        public CodeWriterSettings Settings => _settings;
        public int Indent => _indent;
        public List<string> HeadLines { get; set; }

        public CodeWriter(CodeWriterSettings settings)
        {
            _settings = settings;
            _sb = new StringBuilder();
            _indent = 0;
        }

        public void WriteRaw(string str = null)
        {
            if (str != null)
                _sb.Append(str);
        }

        public void Write(string str = null)
        {
            if (_newLineOnBlockEnd)
            {
                if (str != null)
                    _sb.Append(_settings.NewLine);

                _newLineOnBlockEnd = false;
            }

            WriteInternal(str);
        }

        public void Write(string str, params string[] strs)
        {
            Write(str);
            foreach (var s in strs)
                Write(s);
        }

        private void WriteInternal(string str = null)
        {
            if (str != null)
            {
                _sb.Append(GetIndentString());
                _sb.Append(str);
                _sb.Append(_settings.NewLine);
            }
            else
            {
                _sb.Append(_settings.NewLine);
            }
        }

        public UsingHandle OpenBlock(string[] strs, bool newLineAfterBlockEnd = false)
        {
            if (_newLineOnBlockEnd)
            {
                _sb.Append(_settings.NewLine);
                _newLineOnBlockEnd = false;
            }

            if (strs.Any())
            {
                for (var i = 0; i < strs.Length; i++)
                {
                    _sb.Append(GetIndentString(i == 0 ? 0 : 1));
                    _sb.Append(strs[i]);
                    if (i < strs.Length - 1)
                        _sb.Append(_settings.NewLine);
                }

                if (_settings.NewLineBeforeBlockBegin)
                {
                    _sb.Append(_settings.NewLineBeforeBlockBegin ? _settings.NewLine : " ");
                    Write(_settings.BlockBegin);
                }
                else
                {
                    _sb.Append(" ");
                    _sb.Append(_settings.BlockBegin);
                    _sb.Append(_settings.NewLine);
                }
            }
            else
            {
                Write(_settings.BlockBegin);
            }

            IncIndent();
            return new UsingHandle(() =>
            {
                DecIndent();
                WriteInternal(_settings.BlockEnd);

                _newLineOnBlockEnd = newLineAfterBlockEnd;
            });
        }

        public UsingHandle OpenIndent(string begin, string end, bool newLineAfterBlockEnd = false)
        {
            if (_newLineOnBlockEnd)
            {
                _sb.Append(_settings.NewLine);
                _newLineOnBlockEnd = false;
            }

            if (begin != null)
            {
                _sb.Append(GetIndentString());
                _sb.Append(begin);
                _sb.Append(_settings.NewLine);
            }

            IncIndent();
            return new UsingHandle(() =>
            {
                DecIndent();
                if (end != null)
                    WriteInternal(end);

                _newLineOnBlockEnd = newLineAfterBlockEnd;
            });
        }

        public void IncIndent()
        {
            _indent += 1;
        }

        public void DecIndent()
        {
            if (_indent == 0)
                throw new InvalidOperationException("Cannot decrease indent.");

            _indent -= 1;
        }

        public string GetIndentString(int additional = 0)
        {
            return string.Concat(Enumerable.Repeat(_settings.Indent, _indent + additional));
        }

        public override string ToString()
        {
            var headComment = HeadLines != null
                                  ? string.Join(_settings.NewLine, HeadLines) + _settings.NewLine
                                  : "";

            var text = headComment + _sb.ToString();
            if (_settings.TranslationMapping != null)
            {
                foreach (var i in _settings.TranslationMapping)
                {
                    text = text.Replace(i.Key, i.Value);
                }
            }
            return text;
        }

        public bool WriteAllText(string path, bool skipNotChanged = true)
        {
            return WriteAllText(path, skipNotChanged, Encoding.UTF8);
        }

        public bool WriteAllText(string path, bool skipNotChanged, Encoding encoding)
        {
            var text = ToString();

            if (skipNotChanged && File.Exists(path))
            {
                var existingText = File.ReadAllText(path);
                if (existingText == text)
                {
                    return false;
                }
            }
            File.WriteAllText(path, text, encoding);
            return true;
        }
    }
}
