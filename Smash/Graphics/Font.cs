using System.Numerics;
using SDL3;
using Smash;

public class Font : IDisposable, IAsset
{
    internal string _fontPath;
    internal Dictionary<float, (nint handle, Dictionary<string, nint> texts)> _alreadyCreatedTexts = new();

    public Font(string fontPath)
    {
        _fontPath = fontPath;
    }

    public Vector2 MeasureString(string text, float pointSize)
    {
        TTF.GetTextSize(GetOrCreateText(text, pointSize), out int widht, out int height);
        return new Vector2(widht, height);
    }

    internal nint GetOrCreateText(string text, float pointSize)
    {
        if (_alreadyCreatedTexts.TryGetValue(pointSize, out var font))
        {
            if (font.texts.TryGetValue(text, out nint textObject))
            {
                return textObject;
            }
        }
        else
        {
            _alreadyCreatedTexts.Add(pointSize, (TTF.OpenFont(_fontPath, pointSize), new()));
        }

        nint textObjectHandle = TTF.CreateText(SmashEngine._fontEngine, _alreadyCreatedTexts[pointSize].handle, text, 0);
        _alreadyCreatedTexts[pointSize].texts[text] = textObjectHandle;

        return textObjectHandle;
    }

    public void Dispose()
    {
        foreach (var idklol in _alreadyCreatedTexts.Values)
        {
            foreach (nint alreadyCreatedText in idklol.texts.Values)
            {
                TTF.DestroyText(alreadyCreatedText);
            }

            TTF.CloseFont(idklol.handle);
        }

        _alreadyCreatedTexts.Clear();
    }
}