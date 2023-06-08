using Logic;
using System.Collections.ObjectModel;

namespace Model
{

    public abstract class AbstractModelAPI
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract void CreateBall();
        public abstract ObservableCollection<object> GetBalls();
        public static AbstractModelAPI CreateModelAPI(int boardWidht, int boardHeight,AbstractBallAPI logicAPI)
        {
            if (logicAPI == null)
            {
                return new Model( AbstractBallAPI.CreateInstance(boardWidht,boardHeight,null));
            }
            else
            {
                return new Model(logicAPI);
            }
            
        }
    }
    
}
