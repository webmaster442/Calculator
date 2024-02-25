﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

namespace Calculator.RequestHandlers;

internal sealed class Template
{
    private readonly StringBuilder _stringBuilder;

    public Template(string content)
    {
        _stringBuilder = new StringBuilder(content);
    }

    public Template ApplyTag(string tag, string value)
    {
        _stringBuilder.Replace($"<!--{{{tag}}}-->", value);
        return this;
    }

    public string Render()
    {
        return _stringBuilder.ToString();
    }

    public const string Content = "content";
}