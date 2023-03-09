using ProjectLibrary;

namespace ProjectTests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Program prog = new Program();
            Assert.AreEqual(prog.Subtraction(5, 4), 1);
        }
    }
}