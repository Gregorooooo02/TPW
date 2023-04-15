using Logic;

namespace Model
{
    public class WindowModel
    {
        private Window _window;
        public int Width => _window.Width;
        public int Height => _window.Height;

        public WindowModel(Window window)
        {
            _window = window;
        }
    }
}
