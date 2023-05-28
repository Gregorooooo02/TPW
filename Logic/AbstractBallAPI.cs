using Data;

namespace Logic
{
    /*
     * Define a public abstract class for a Logic API that implements IObservable and IDisposable
     */
    public abstract class AbstractBallAPI : IObservable<IBall>, IDisposable
    {
        public static AbstractBallAPI CreateInstance(AbstractDataAPI? data = default)
        {
            return new Logic(data ?? AbstractDataAPI.CreateInstance());
        }

        // Define an abstract method to create a given number of IBall instances
        public abstract IEnumerable<IBall> CreateBalls(int count);

        // Define abstract implementations of IDisposable and IObservable
        public abstract IDisposable Subscribe(IObserver<IBall> observer);
        public abstract void Dispose();
    }

    /*
     * Define an interface for an IBall instance that implements IObservable, IObserver, and IDisposable
     */
    public interface IBall : IObservable<IBall>, IObserver<IBallData>, IDisposable
    {
        int Diameter { get; }
        int Radius { get; }
        Vector2 Position { get; }
        Vector2 Velocity { get; set; }

        void Move(float scaler); // move the ball based on the scaler
        bool Touches(IBall ball); // determine whether the ball touches another ball
    }
}