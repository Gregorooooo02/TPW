using Logic; // Importing the namespace Logic, which contains the AbstractLogicAPI class.
using System.ComponentModel;

namespace Model
{
    // Abstract class that implements the IObserver and IObservable interfaces with generic type parameters.
    public abstract class AbstractModelAPI : IObserver<IBall>, IObservable<IBallModel>
    {
        public static AbstractModelAPI CreateInstance(AbstractBallAPI? logic = default)
        {
            return new Model(logic ?? AbstractBallAPI.CreateInstance());
        }

        public abstract void Start(int count);
        public abstract void Stop();

        public abstract IDisposable Subscribe(IObserver<IBallModel> observer);

        public abstract void OnCompleted();
        public virtual void OnError(Exception error) => throw error;
        public abstract void OnNext(IBall value);
    }

    public interface IBallModel : IObserver<IBall>, INotifyPropertyChanged
    {
        public int Diameter { get; }
        public int Radius { get; }
        public Vector2 Velocity { get; }
        public Vector2 Position { get; }
    }
}
