namespace Data
{
    public abstract class AbstractDataAPI
    {
        public abstract int WindowHeight { get; }
        public abstract int WindowWidth { get; }
        public abstract int BallDiameter { get; }

        public static AbstractDataAPI CreateInstance()
        {
            return new Data();
        }
    }
}