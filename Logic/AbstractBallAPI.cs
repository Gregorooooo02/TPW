using Data;

namespace Logic
{
    public abstract class AbstractBallAPI
    {
        // Define BallAPI properties
        public abstract int WindowHeight { get; }
        public abstract int WindowWidth { get; }
        public abstract List<AbstractBallDataAPI> BallsList { get; }

        public abstract void SpawnBall();
        public abstract void StartSimulation();
        public abstract void StopSimulation();

        public abstract int GetPositionX(int index);
        public abstract int GetPositionY(int index);
        public abstract int GetRadius(int index);
        public abstract int GetNumberOfBalls();


        public static AbstractBallAPI CreateApi(int windowHeight, int windowWidth, AbstractDataAPI data)
        {
            if (data == null)
            {

                return new Ball(AbstractDataAPI.CreateInstance(windowHeight, windowWidth));

            }
            else
            {
                return new Ball(data);
            }

        }
    }
}