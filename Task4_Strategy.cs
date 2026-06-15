using System;
using System.Collections.Generic;
using System.Text;

interface IImageLoadStrategy
{
    string Load(string href);
}

class FileSystemImageStrategy : IImageLoadStrategy
{
    public string Load(string href)
    {
        Console.WriteLine($"[Strategy] Завантаження з файлової системи: {href}");
        return $"[local image: {href}]";
    }
}

class NetworkImageStrategy : IImageLoadStrategy
{
    public string Load(string href)
    {
        Console.WriteLine($"[Strategy] Завантаження з мережі: {href}");
        return $"[network image: {href}]";
    }
}


abstract class LightNodeS
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
}

class LightTextNodeS : LightNodeS
{
    private string _text;
    public LightTextNodeS(string text) { _text = text; }
    public override string OuterHTML => _text;
    public override string InnerHTML => _text;
}

enum DisplayTypeS { Block, Inline }
enum ClosingTypeS  { Single, WithClosing }

class LightElementNodeS : LightNodeS
{
    public string TagName { get; }
    private List<LightNodeS> _children = new List<LightNodeS>();
    private string _classAttr;

    public LightElementNodeS(string tagName, string cssClass = "")
    {
        TagName = tagName;
        _classAttr = cssClass.Length > 0 ? $" class=\"{cssClass}\"" : "";
    }

    public void AddChild(LightNodeS node) => _children.Add(node);

    public override string InnerHTML
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var c in _children) sb.Append(c.OuterHTML);
            return sb.ToString();
        }
    }
    public override string OuterHTML => $"<{TagName}{_classAttr}>{InnerHTML}</{TagName}>";
}

class LightImageNode : LightNodeS
{
    private string _href;
    private IImageLoadStrategy _strategy;
    private string _alt;

    public LightImageNode(string href, string alt = "")
    {
        _href = href;
        _alt  = alt;
        _strategy = SelectStrategy(href);
    }

    private IImageLoadStrategy SelectStrategy(string href)
    {
        if (href.StartsWith("http://") || href.StartsWith("https://"))
            return new NetworkImageStrategy();
        return new FileSystemImageStrategy();
    }

    public override string InnerHTML => "";
    public override string OuterHTML
    {
        get
        {
            string loaded = _strategy.Load(_href);
            return $"<img src=\"{_href}\" alt=\"{_alt}\" data-loaded=\"{loaded}\"/>";
        }
    }
}
