using Data;

namespace Logic
{
    public abstract class AbstractLogicAPI : IObservable<IEnumerable<Ball>>
    {
        public abstract IEnumerable<Ball> Balls { get; }

        public abstract void SpawnBalls(int numberOfBalls);
        public abstract void Simulation();
        public abstract void StartSim();
        public abstract void StopSim();
        public abstract IDisposable Subscribe(IObserver<IEnumerable<Ball>> observer);

        public static AbstractLogicAPI CreateInstance(AbstractDataAPI? data = default)
        {
            return new SimulationController(data ?? AbstractDataAPI.CreateInstance());
        }
    }
}
