using Domains.Battle;
using Domains.Character;
using Domains.MasterData;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Editor.Domains.Battle
{
    public class BattleCharacterModelTests
    {
        private BattleCharacterModel CreateModel(int agi = 10, int maxHp = 100)
        {
            var id = new CharacterId("hero");
            var stats = new BaseStats(maxHp, 50, 10, 10, 10, 10, agi, 10);
            var master = new CharacterMasterData(id, "Hero", stats);
            return new BattleCharacterModel(master);
        }

        [Test]
        public void TickGauge_IncreasesGaugeBasedOnCalculator()
        {
            // Arrange
            var model = CreateModel();

            // モックの作成（AGIに関わらず常に 100 増加すると設定）
            var calculatorMock = Substitute.For<IActionGaugeCalculator>();
            calculatorMock.CalculateIncrement(Arg.Any<BaseStats>(), Arg.Any<float>()).Returns(100);

            // Act
            model.TickGauge(1.0f, calculatorMock);

            // Assert
            // 初期状態は0、Tick後は100になっているはず
            Assert.AreEqual(100, model.CurrentGauge.CurrentValue.Value);
        }

        [Test]
        public void ApplyDamage_ReducesHpAndClampsToZero()
        {
            // Arrange
            var model = CreateModel(maxHp: 100);

            // Act
            model.ApplyDamage(120);

            // Assert
            Assert.AreEqual(0, model.CurrentHp.CurrentValue.Value);
        }

    }
}
