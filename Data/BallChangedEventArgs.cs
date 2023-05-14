namespace Data
{
    public class BallChangedEventArgs : EventArgs
    {
        public IBallData Ball { get; set; }

        public BallChangedEventArgs(IBallData ball)
        {
            Ball = ball;
        }
    }

}