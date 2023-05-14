namespace Data
{
    public interface IBallData : IObservable<IBallData>
    {
        int Diameter { get; init; }
        float SpeedX { get; }
        float SpeedY { get; }
        float PositionX { get; }
        float PositionY { get; }

        Task SetSpeed(float speedX, float speedY);
        Task Move(float moveX, float moveY);
    }
}
