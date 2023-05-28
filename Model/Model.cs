using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    internal class Model : AbstractModelAPI
    {
        private AbstractBallAPI _ballAPI;

        public Model (AbstractBallAPI ballAPI)
        {
            _ballAPI = ballAPI;
        }

        public override ObservableCollection<object> GetBalls()
        {
            ObservableCollection<object> _balls = new ObservableCollection<object>();

            foreach (object _ball in _ballAPI.BallsList)
            {
                _balls.Add(_ball);
            }

            return _balls;
        }

        public override void SpawnBall()
        {
            _ballAPI.SpawnBall();
        }

        public override void StartSimulation()
        {
            _ballAPI.StartSimulation();
        }

        public override void StopSimulation()
        {
            _ballAPI.StopSimulation();
        }
    }
}