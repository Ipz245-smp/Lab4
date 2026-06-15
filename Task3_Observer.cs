using System;
using System.Collections.Generic;
using System.Text;


abstract class LightNode
{
    public abstract string OuterHTML { get; }
    public abstract string InnerHTML { get; }
}

class LightTextNode : LightNode
{
    private string _text;
    public LightTextNode(string text) { _text = text; }
    public override string OuterHTML => _text;
    public override string InnerHTML => _text;
}

enum DisplayType { Block, Inline }
enum ClosingType  { Single, WithClosing }

class LightElementNode : LightNode
{
    public string TagName   { get; }
    public DisplayType Display { get; }
    public ClosingType Closing { get; }
    public List<string> CssClasses { get; } = new List<string>();

    private List<LightNode> _children = new List<LightNode>();
    private Dictionary<string, List<Action<string>>> _eventListeners
        = new Dictionary<string, List<Action<string>>>();

    public int ChildCount => _children.Count;

    public LightElementNode(string tagName, DisplayType display, ClosingType closing, params string[] cssClasses)
    {
        TagName = tagName;
        Display = display;
        Closing = closing;
        CssClasses.AddRange(cssClasses);
    }

    public void AddChild(LightNode node) => _children.Add(node);

    public void AddEventListener(string eventType, Action<string> handler)
    {
        if (!_eventListeners.ContainsKey(eventType))
            _eventListeners[eventType] = new List<Action<string>>();
        _eventListeners[eventType].Add(handler);
    }

    public void DispatchEvent(string eventType)
    {
        if (_eventListeners.ContainsKey(eventType))
        {
            Console.WriteLine($"[Event '{eventType}' on <{TagName}>]");
            foreach (var handler in _eventListeners[eventType])
                handler(eventType);
        }
        else
        {
            Console.WriteLine($"[Event '{eventType}' on <{TagName}>]: немає підписників");
        }
    }

    private string ClassAttr => CssClasses.Count > 0
        ? $" class=\"{string.Join(" ", CssClasses)}\""
        : "";

    public override string InnerHTML
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var child in _children)
                sb.Append(child.OuterHTML);
            return sb.ToString();
        }
    }

    public override string OuterHTML
    {
        get
        {
            if (Closing == ClosingType.Single)
                return $"<{TagName}{ClassAttr}/>";
            return $"<{TagName}{ClassAttr}>{InnerHTML}</{TagName}>";
        }
    }
}
