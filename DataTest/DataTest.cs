using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void CreateInstanceTest()
        {
            AbstractDataAPI abstractDataAPI = AbstractDataAPI.CreateInstance();

            Assert.IsNotNull(abstractDataAPI);
        }

        [TestMethod]
        public void VariablesTest()
        {
            AbstractDataAPI data = AbstractDataAPI.CreateInstance();

            Assert.AreNotEqual(data.WindowHeight, default);
            Assert.AreNotEqual(data.WindowWidth, default);
            Assert.AreNotEqual(data.BallDiameter, default);
        }
    }
}