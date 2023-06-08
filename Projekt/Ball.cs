using Data;
using System.ComponentModel;
using System.Numerics;

namespace Logic
{
    internal class Ball : AbstractBallAPI
    {

        public override List<AbstractBallDataAPI> balls { get; }
        private static readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        public override int WindowWidth { get; }
        public override int WindowHeight { get; }

        private AbstractDataAPI data;


        public Ball(AbstractDataAPI data)
        {
            balls = new List<AbstractBallDataAPI>();
            this.data = data;
            this.WindowWidth = data.getWindowWidth();
            this.WindowHeight = data.getWindowHeight();

        }



        public override void SpawnBall()
        {
            AbstractBallDataAPI ball = data.spawnBall(true);
            if (balls.Count <= 0)
            {
                data.LoggerStart(AbstractDataAPI.ballQueue);
                ball.isSimulationRunning = true;
            }
            else
            {
                ball.isSimulationRunning = balls[0].isSimulationRunning;
            }
            balls.Add(ball);
            ball.AddPropertyChangedListener(CheckCollisions);
        }


        private bool CheckCollisionWithOtherBall(AbstractBallDataAPI ball1, AbstractBallDataAPI ball2)
        {
            Vector2 position1 = ball1.Position;
            Vector2 position2 = ball2.Position;
            int distance = (int)Math.Sqrt(Math.Pow((position1.X + ball1.VelocityX) - (position2.X + ball2.VelocityX), 2) + Math.Pow((position1.Y + ball1.VelocityX) - (position2.Y + ball2.VelocityY), 2));
            if (distance <= ball1.Size / 2 + ball2.Size / 2)
            {
                readerWriterLockSlim.EnterWriteLock();
                try
                {
                    int v1x = ball1.VelocityX;
                    int v1y = ball1.VelocityY;
                    int v2x = ball2.VelocityX;
                    int v2y = ball2.VelocityY;

                    int newV1X = (v1x * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2x) / (ball1.Mass + ball2.Mass);
                    int newV1Y = (v1y * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2y) / (ball1.Mass + ball2.Mass);
                    int newV2X = (v2x * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1x) / (ball1.Mass + ball2.Mass);
                    int newV2Y = (v2y * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1y) / (ball1.Mass + ball2.Mass);
                    ball1.setVelocity(newV1X, newV1Y);
                    ball2.setVelocity(newV2X, newV2Y);
                }
                finally
                {
                    readerWriterLockSlim.ExitWriteLock();
                }
                return false;
            }
            return true;

        }

        private void CheckCollisionWithBoard(AbstractBallDataAPI ball)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                int Vx = ball.VelocityX;
                int Vy = ball.VelocityY;
                Vector2 position = ball.Position;

                if (position.X + ball.VelocityX < 0 || position.X + ball.VelocityX >= WindowWidth)
                {
                    Vx = -ball.VelocityX;
                }

                if (position.Y + ball.VelocityY < 0 || position.Y + ball.VelocityY >= WindowHeight)
                {
                    Vy = -ball.VelocityY;
                }

                ball.setVelocity(Vx, Vy);
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }

        private void CheckCollisions(object sender, PropertyChangedEventArgs e)
        {
            AbstractBallDataAPI ball = (AbstractBallDataAPI)sender;
            if (ball != null)
            {
                CheckCollisionWithBoard(ball);

                foreach (var ball2 in balls)
                {
                    if (!ball2.Equals(ball))
                    {
                        CheckCollisionWithOtherBall(ball, ball2);
                    }
                }
            }

        }


        public override int GetPositionX(int index)
        {
            if (index >= 0 && index < balls.Count)
            {
                return (int)balls[index].Position.Y;
            }
            else
            {
                return -1;
            }
        }

        public override int GetPositionY(int index)
        {
            if (index >= 0 && index < balls.Count)
            {
                return (int)balls[index].Position.X;
            }
            else
            {
                return -1;
            }
        }

        public override int GetNumberOfBalls()
        {
            return balls.Count;
        }

        public override void StartSimulation()
        {
            foreach (var ball in balls)
            {
                ball.isSimulationRunning = true;
            }
            data.LoggerStart(AbstractBallDataAPI.BallQueue);
        }

        public override void StopSimulation()
        {
            foreach (var ball in balls)
            {
                ball.isSimulationRunning = false;
            }
            data.LoggerStop();
        }

        public override int GetSize(int i)
        {
            if (i >= 0 && i < balls.Count)
            {
                return balls[i].Size;
            }
            else
            {
                return -1;
            }

        }



    }
}
