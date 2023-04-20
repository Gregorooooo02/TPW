using Data;
using System.ComponentModel;

namespace Logic
{
    // Define the AbstractLogicAPI class as public and implement the IObservable interface for an IEnumerable of Ball objects.
    public abstract class AbstractLogicAPI : IObservable<IEnumerable<IBall>>
    {
        // Define an abstract property for an IEnumerable of Ball objects.
        internal abstract IEnumerable<Ball> Balls { get; }

        // Define abstract methods for spawning balls, simulating behavior, starting and stopping the simulation, and subscribing to changes in the list of balls.
        public abstract void SpawnBalls(int numberOfBalls);
        public abstract void Simulation();
        public abstract void StartSim();
        public abstract void StopSim();
        public abstract IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer);

        // Define a static method to create an instance of the AbstractLogicAPI class, with an optional AbstractDataAPI parameter.
        public static AbstractLogicAPI CreateInstance(AbstractDataAPI? data = default)
        {
            // Return a new instance of the SimulationController class, passing in the provided AbstractDataAPI instance or creating a new instance of AbstractDataAPI if no instance is provided.
            return new SimulationController(data ?? AbstractDataAPI.CreateInstance());
        }
    }

    public interface IBall
    {
        Vector2 Velocity { get; }
        Vector2 Position { get; }
        int Radius { get; }
        int Diameter { get; }
    }
}
