using Domains.Battle;
using NUnit.Framework;

namespace Tests.Editor.Domains.Battle
{
    public class BattleTimeStateModelTests
    {
        [Test]
        public void InitialState_IsPaused_IsFalse()
        {
            // Arrange & Act
            var stateModel = new BattleTimeStateModel();

            // Assert
            Assert.IsFalse(stateModel.IsPaused.CurrentValue);
        }

        [Test]
        public void SetPause_ChangesIsPausedState()
        {
            // Arrange
            var stateModel = new BattleTimeStateModel();

            // Act
            stateModel.SetPause(true);

            // Assert
            Assert.IsTrue(stateModel.IsPaused.CurrentValue);

            // Act
            stateModel.SetPause(false);

            // Assert
            Assert.IsFalse(stateModel.IsPaused.CurrentValue);
        }
    }
}