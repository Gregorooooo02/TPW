using Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void CreateModelApiTest()
        {
            AbstractModelAPI modelApi = AbstractModelAPI.CreateInstance();
            Assert.IsNotNull(modelApi);
        }
    }
}