using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace Tests
{
    [TestClass]
    public class ModelAPITest
    {
        [TestMethod]
        public void CreateInstanceTest()
        {
            AbstractModelAPI modelAPI = AbstractModelAPI.CreateInstance();

            Assert.IsNotNull(modelAPI);
        }
    }
}