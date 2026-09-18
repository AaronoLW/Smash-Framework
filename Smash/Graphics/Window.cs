using System.Numerics;
using SDL3;

namespace SmashFramework;

public class Window(nint windowHandle) : IDisposable
{
    /// <summary>
    /// The handle of this instance of the Window class
    /// </summary>
    public readonly nint Handle = windowHandle;

    public float Width;
    public float Height;
    public Vector2 Size
    {
        get { return new Vector2(Width, Height); }
        set { Width = value.X; Height = value.Y; }
    }

    public void SetFullscreen(bool fullscreen)
    {
        SDL.SetWindowFullscreen(Handle, fullscreen);
    }

    public void SetWindowAlwaysOnTop(bool alwaysOnTop)
    {
        SDL.SetWindowAlwaysOnTop(Handle, alwaysOnTop);
    }

    public void SetWindowTitle(string title)
    {
        SDL.SetWindowTitle(Handle, title);
    }

    public void SetWindowMinimumSize(int minimumWidth, int minimumHeight)
    {
        SDL.SetWindowMinimumSize(Handle, minimumWidth, minimumHeight);
    }

    public void SetWindowMaximumSize(int maximumWidth, int maximumHeight)
    {
        SDL.SetWindowMaximumSize(Handle, maximumWidth, maximumHeight);
    }

    public void SetWindowResizable(bool resizable)
    {
        SDL.SetWindowResizable(Handle, resizable);
    }

    public void Dispose()
    {
        SDL.DestroyWindow(Handle);
    }
}
