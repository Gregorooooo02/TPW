using Logic;

namespace Model
{
    // This class represents a WindowModel, which provides a simple interface to access information about a Window object
    public class WindowModel
    {
        private Window _window; // A reference to a Window object
        public int Width => _window.Width; // Property to get the width of the Window
        public int Height => _window.Height; // Property to get the height of the Window

        // Constructor that creates a WindowModel with a reference to a Window object
        public WindowModel(Window window)
        {
            _window = window;
        }
    }
}
