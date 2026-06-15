using System;
using System.Collections.Generic;

class TextDocument
{
    public string Content { get; set; }

    public TextDocument(string content)
    {
        Content = content;
    }
    public class Snapshot
    {
        public string Content { get; }
        public DateTime SavedAt { get; }

        public Snapshot(string content)
        {
            Content = content;
            SavedAt = DateTime.Now;
        }
    }

    public Snapshot Save()     => new Snapshot(Content);
    public void Restore(Snapshot s) => Content = s.Content;
}

class TextEditor
{
    private TextDocument _document;
    private Stack<TextDocument.Snapshot> _history = new Stack<TextDocument.Snapshot>();

    public TextEditor(string initialContent)
    {
        _document = new TextDocument(initialContent);
    }

    public void Type(string text)
    {
        _history.Push(_document.Save());
        _document.Content += text;
        Console.WriteLine($"[Редактор] Поточний текст: \"{_document.Content}\"");
    }

    public void Undo()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("[Редактор] Немає збережених станів для скасування.");
            return;
        }
        _document.Restore(_history.Pop());
        Console.WriteLine($"[Редактор] Скасовано. Поточний текст: \"{_document.Content}\"");
    }

    public void ShowCurrent() => Console.WriteLine($"[Редактор] Поточний текст: \"{_document.Content}\"");
}
