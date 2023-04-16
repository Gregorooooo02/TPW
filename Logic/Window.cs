namespace Logic
{
    // Represents a window with a specific width and height
    public class Window
    {
        public int Width { get; init; }

        public int Height { get; init; }

        // Gets a Vector2 representing the boundaries of the window on the X-axis
        // The X component of the Vector2 is 0, and the Y component is equal to the Width property
        public Vector2 XBoundry => new Vector2(0, Width);

        // Gets a Vector2 representing the boundaries of the window on the Y-axis
        // The X component of the Vector2 is 0, and the Y component is equal to the Height property
        public Vector2 YBoundry => new Vector2(0, Height);

        // Initializes a new instance of the Window class with the specified width and height
        public Window(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
