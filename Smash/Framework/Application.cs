using System.Numerics;
using SDL3;

namespace SmashFramework;

public abstract class Application
{
    internal static bool _applicationShouldClose = false;

    public virtual void Start() { }
    public virtual void Update(double deltaTime) { }
    public virtual void Render() { }
    public virtual void End() { }

    protected void CreateWindowAndRenderer(string title, int width, int height, out Window window, out Renderer renderer)
    {
        if (!SDL.CreateWindowAndRenderer(title, width, height, 0, out nint windowHandle, out nint rendererHandle))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            throw new Exception($"SDL ERROR: Error creating window and rendering: {SDL.GetError}");
        }

        window = new Window(windowHandle);
        renderer = new Renderer(rendererHandle);

        window.Size = new Vector2(width, height);
        SmashEngine._windows.Add(window);
        SmashEngine._fontEngine = TTF.CreateRendererTextEngine(rendererHandle);
    }

    public bool ApplicationShouldClose()
    {
        return _applicationShouldClose;
    }
}
