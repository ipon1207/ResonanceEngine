using Domains.Battle;
using Domains.Enemy;
using Domains.MasterData;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Editor.Domains.Battle
{
    public class BattleEnemyModelTests
    {
        private BattleEnemyModel CreateModel(int agi = 10, int maxHp = 50)
        {
            var id = new EnemyId("slime");
            var stats = new BaseStats(maxHp, 20, 5, 5, 5, 5, agi, 5);
            var master = new EnemyMasterData(id, "Slime", stats);
            return new BattleEnemyModel(master);
        }

        [Test]
        public void TickGauge_IncreasesGaugeBasedOnCalculator()
        {
            // Arrange
            var model = CreateModel();

            var calculatorMock = Substitute.For<IActionGaugeCalculator>();
            // 計算結果として常に100増加すると設定
            calculatorMock.CalculateIncrement(Arg.Any<BaseStats>(), Arg.Any<float>()).Returns(100);

            // Act
            model.TickGauge(1.0f, calculatorMock);

            // Assert
            Assert.AreEqual(100, model.CurrentGauge.CurrentValue.Value);
        }
    }
}