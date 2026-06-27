using System.Numerics;
using SDL3;
using Smash;

public class Font : IDisposable, IAsset
{
    public nint Handle { get; }

    internal Dictionary<int, Dictionary<string, nint>> _alreadyCreatedTexts = new();

    public Font(nint fontHandle)
    {
        Handle = fontHandle;
    }

    public Vector2 MeasureString(string text, int pointSize)
    {
        TTF.GetTextSize(GetOrCreateText(text, pointSize), out int widht, out int height);
        return new Vector2(widht, height);
    }

    internal nint GetOrCreateText(string text, int pointSize)
    {
        if (_alreadyCreatedTexts.TryGetValue(pointSize, out var font))

        if (font.TryGetValue(text, out nint textObject))
        {
            return textObject;
        }

        nint textObjectHandle = TTF.CreateText(SmashEngine._fontEngine, Handle, text, 0);
        _alreadyCreatedTexts[pointSize][text] = textObjectHandle;

        return textObjectHandle;
    }

    public void Dispose()
    {
        foreach (Dictionary<string, nint> kvp in _alreadyCreatedTexts.Values)
        {
            foreach (nint alreadyCreatedText in kvp.Values)
            {
                TTF.DestroyText(alreadyCreatedText);
            }
        }

        TTF.CloseFont(Handle);
        _alreadyCreatedTexts.Clear();
    }
}