namespace Data
{
    // Define the AbstractDataAPI class as abstract
    public abstract class AbstractDataAPI
    {
        // Define abstract properties for the window height, window width, and ball diameter.
        public abstract int WindowHeight { get; }
        public abstract int WindowWidth { get; }
        public abstract int BallDiameter { get; }

        // Define a static method to create an instance of the AbstractDataAPI class.
        public static AbstractDataAPI CreateInstance()
        {
            // Return a new instance of the Data class.
            return new Data();
        }
    }
}
