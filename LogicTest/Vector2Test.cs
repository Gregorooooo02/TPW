using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Vector2Test
    {
        private const float Delta = 1E-6f;
        private const float A = 2f;
        private const float B = 0.4f;
        private Vector2 Vect1 = new Vector2(A, B);
        private Vector2 Vect2 = new Vector2(B, A);

        [TestMethod]
        public void ConstructorTest()
        {
            // Check Vector1 values
            Assert.AreEqual(A, Vect1.X);
            Assert.AreEqual(B, Vect1.Y);

            // Check Vector2 values
            Assert.AreEqual(B, Vect2.X);
            Assert.AreEqual(A, Vect2.Y);
        }

        [TestMethod]
        public void EqualsTest()
        {
            Vector2 vector1 = new Vector2(A, B);
            Vector2 vector2 = new Vector2(A, B);

            Assert.AreEqual(vector1, vector2);
        }

        [TestMethod]
        public void AdditionTest()
        {
            Assert.AreEqual(new Vector2(2.4f, 2.4f), Vect1 + Vect2);
        }

        [TestMethod]
        public void SubstractionTest()
        {
            Assert.AreEqual(new Vector2(1.6f, -1.6f), Vect1 - Vect2);
            Assert.AreEqual(new Vector2(-1.6f, 1.6f), Vect2 - Vect1);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            Assert.AreEqual(new Vector2(0.8f, 0.8f), Vect1 * Vect2);
        }

        [TestMethod]
        public void DivisionTest()
        {
            Assert.AreEqual(new Vector2(5f, 0.2f), Vect1 / Vect2);
        }

        [TestMethod]
        public void DeconstructTest()
        {
            var (a, b) = Vect1;
            Assert.AreEqual(a, A);
            Assert.AreEqual(b, B);
        }

        [TestMethod]
        public void DistanceSquaredTest()
        {
            Assert.AreEqual(5.12f, Vector2.DistanceSquared(Vect1, Vect2), Delta);
        }

        [TestMethod]
        public void DistanceTest()
        {
            Assert.AreEqual(2.2627417f, Vector2.Distance(Vect1, Vect2), Delta);
        }
    }
}
