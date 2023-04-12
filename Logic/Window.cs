namespace Logic
{
    public class Window
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public Vector2 XBoundry => new Vector2(0, Width);
        public Vector2 YBoundry => new Vector2(0, Height);

        public Window(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
