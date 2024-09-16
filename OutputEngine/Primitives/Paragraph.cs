﻿using System.Collections;
using System.Text;

namespace OutputEngine.Primitives;

public class Paragraph : Element, IEnumerable<TextPart>
{
    //public static implicit operator Paragraph(TextPart textPart) => [textPart];
    //public static implicit operator string(Paragraph textGroup) => textGroup.PlainOutput().JoinLines();
    //public static explicit operator Paragraph(string s) => [new TextPart(s)];

    private List<TextPart> parts = new();

    public Paragraph(params TextPart[] textParts)
    => parts.AddRange(textParts);

    public int PlainWidth(int trialWidth)
    {
        IEnumerable<string> strings = PlainOutput(trialWidth);
        return strings.Max(s => s.Length);
    }

    public IEnumerable<string> PlainOutput(int outputWidth = int.MaxValue)
    {
        var sb = new StringBuilder();
        var first = this.First();
        var last = this.Last();
        foreach (var part in this)
        {
            if (part != first && part.Whitespace.HasFlag(Whitespace.Before))
            {
                sb.Append(' ');
            }
            sb.Append(part.Text);
            if (part != last && part.Whitespace.HasFlag(Whitespace.After))
            {
                sb.Append(' ');
            }
        }

        return sb.ToString().Wrap(outputWidth);
    }

    public void Add(TextPart part) 
        => this.parts.Add(part);

    public void AddRange(IEnumerable<TextPart> parts) 
        => this.parts.AddRange(parts);

    public TextPart this[int index]
        => parts[index];

    public IEnumerable<TextPart> Slice(int start, int length) 
        => parts[new Range(start, length)];

    public int Count() 
        => parts.Count();

    public int Length
    => Count();

    public IEnumerator<TextPart> GetEnumerator() 
        => ((IEnumerable<TextPart>)this.parts).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() 
        => ((IEnumerable)this.parts).GetEnumerator();

    public override string ToString()
    { return PlainOutput().JoinLines(); }
}
