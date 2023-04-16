namespace Data
{
    // Define the Data class as internal and subclass AbstractDataAPI.
    internal class Data : AbstractDataAPI
    {
        // Override the abstract WindowHeight property with a value of 478.
        public override int WindowHeight => 478;

        // Override the abstract WindowWidth property with a value of 798.
        public override int WindowWidth => 798;

        // Override the abstract BallDiameter property with a value of 20.
        public override int BallDiameter => 20;
    }
}
