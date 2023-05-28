namespace Data
{
    // Define the AbstractDataAPI class as abstract
    public abstract class AbstractDataAPI
    {
        // Define abstract methods for Window Height and Window Width
        public abstract int WindowHeight { get; }
        public abstract int WindowWidth { get; }

        // Define abstract method for spawning balls in randomized position
        public abstract AbstractBallDataAPI spawnBalls(bool isRunning);

        // Define a static method to create an instance of the AbstractDataAPI class.
        public static AbstractDataAPI CreateInstance(int windowHeight, int windowWidth)
        {
            // Return a new instance of the Data class.
            return new Data(windowHeight, windowWidth);
        }
    }
}
