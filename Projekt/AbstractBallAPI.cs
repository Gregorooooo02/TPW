using Data;


namespace Logic
{

    public abstract class AbstractBallAPI
    {
        public abstract List<AbstractBallDataAPI> balls { get; }
        public abstract int WindowWidth { get; }
        public abstract int WindowHeight { get; }
        public abstract void SpawnBall();
        public abstract void StartSimulation();
        public abstract void StopSimulation();

        public abstract int GetPositionX(int i);
        public abstract int GetPositionY(int i);
        public abstract int GetSize(int i);
        public abstract int GetNumberOfBalls();


        public static AbstractBallAPI CreateInstance(int windowWidth, int windowHeight, AbstractDataAPI data)
        {
            if (data == null)
            {

                return new Ball(AbstractDataAPI.CreateInstance(windowWidth, windowHeight));

            }
            else
            {
                return new Ball(data);
            }

        }
    }

}