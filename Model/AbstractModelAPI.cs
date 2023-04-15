using Logic;

namespace Model
{
    public abstract class AbstractModelAPI : IObserver<IEnumerable<Ball>>, IObservable<IEnumerable<BallModel>>
    {
        public abstract void SpawnBalls(int numberOfBalls);
        public abstract void Start();
        public abstract void Stop();
        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(IEnumerable<Ball> balls);
        public abstract IDisposable Subscribe(IObserver<IEnumerable<BallModel>> observer);

        public static AbstractModelAPI CreateInstance(AbstractLogicAPI? logic = default)
        {
            return new Model(logic ?? AbstractLogicAPI.CreateInstance());
        }
    }
}
