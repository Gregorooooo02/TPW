using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    internal class Model : AbstractModelAPI
    {
        private AbstractBallAPI _BallsAPI;

        public Model(AbstractBallAPI _BallsAPI)
        {
            this._BallsAPI = _BallsAPI;
        }

        public override void Start()
        {
            _BallsAPI.StartSimulation();
        }

        public override void Stop()
        {
            _BallsAPI.StopSimulation();
        }

        public override void CreateBall()
        {
            _BallsAPI.SpawnBall();
        }
        public override ObservableCollection<object> GetBalls()
        {
            ObservableCollection<object> balls = new ObservableCollection<object>();
            foreach (object ball in _BallsAPI.balls)
                balls.Add(ball);
            return balls;
        }
    }
}
