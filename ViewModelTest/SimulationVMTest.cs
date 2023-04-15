using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using ViewModel;

namespace Tests
{
    [TestClass]
    public class SimulationVMTest
    {
        private readonly SimulationViewModel simulationViewModel = new SimulationViewModel();

        [TestMethod]
        public void StartStopSimulationTest()
        {
            // On start of the simulation
            Assert.IsFalse(simulationViewModel.IsSimulationRunning);

            // After starting it
            simulationViewModel.StartSimulation();
            Assert.IsTrue(simulationViewModel.IsSimulationRunning);

            // Stopping simulation
            simulationViewModel.StopSimulation();
            Assert.IsFalse(simulationViewModel.IsSimulationRunning);
        }

        [TestMethod]
        public void BallCountVariableTest()
        {
            bool ballCountFlag = false;
            simulationViewModel.PropertyChanged += (object? sender, PropertyChangedEventArgs e)
                => ballCountFlag = true;

            Assert.IsFalse(ballCountFlag);

            simulationViewModel.BallsCount = 7;
            Assert.IsTrue(ballCountFlag);
        }

        [TestMethod]
        public void UpdateBalls()
        {
            var collectionBefore = simulationViewModel.Balls;

            bool ballsChangesFlag = false;
            simulationViewModel.PropertyChanged += (object? sender, PropertyChangedEventArgs e)
                => ballsChangesFlag = true;

            Assert.IsFalse(ballsChangesFlag);

            simulationViewModel.OnNext(collectionBefore);
            Assert.IsTrue(ballsChangesFlag);

            var collectionAfter = simulationViewModel.Balls;

            Assert.AreNotSame(collectionBefore, collectionAfter);
        }
    }
}