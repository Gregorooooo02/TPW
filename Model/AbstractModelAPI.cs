using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelAPI
    {
        public abstract void StartSimulation();
        public abstract void StopSimulation();
        public abstract void SpawnBall();
        public abstract ObservableCollection<object> GetBalls();

        public static AbstractModelAPI CreateInstance(int windowHeight, int windowWidth, AbstractBallAPI logicAPI)
        {
            if (logicAPI == null)
            {
                return new Model(AbstractBallAPI.CreateInstance(windowHeight, windowWidth, null));
            }
            else
            {
                return new Model(logicAPI);
            }
        }
    }
}
