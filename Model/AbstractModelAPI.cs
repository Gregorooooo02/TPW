using Logic; // Importing the namespace Logic, which contains the AbstractLogicAPI class.

namespace Model
{
    // Abstract class that implements the IObserver and IObservable interfaces with generic type parameters.
    public abstract class AbstractModelAPI : IObserver<IEnumerable<Ball>>, IObservable<IEnumerable<BallModel>>
    {
        // Abstract methods that must be implemented by the derived classes:
        public abstract void SpawnBalls(int numberOfBalls);

        public abstract void Start();

        public abstract void Stop();

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(IEnumerable<Ball> balls);

        public abstract IDisposable Subscribe(IObserver<IEnumerable<BallModel>> observer);

        // Static method that creates an instance of the Model class, passing a logic instance if provided.
        public static AbstractModelAPI CreateInstance(AbstractLogicAPI? logic = default)
        {
            return new Model(logic ?? AbstractLogicAPI.CreateInstance());
        }
    }
}
