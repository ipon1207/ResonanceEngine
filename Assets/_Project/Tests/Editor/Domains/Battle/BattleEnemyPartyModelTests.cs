using Domains.Battle;
using Domains.Enemy;
using Domains.MasterData;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Editor.Domains.Battle
{
    public class BattleEnemyPartyModelTests
    {
        [Test]
        public void TickAllGauges_WhenAnyEnemyReady_DoesNotIncreaseGauge_WaitMode()
        {
            // Arrange
            var enemy1 = new BattleEnemyModel(
                new EnemyMasterData(new EnemyId("e1"), "E1", new BaseStats(50, 10, 5, 5, 5, 5, 10, 5)));
            var enemy2 = new BattleEnemyModel(
                new EnemyMasterData(new EnemyId("e2"), "E2", new BaseStats(50, 10, 5, 5, 5, 5, 10, 5)));

            var party = new BattleEnemyPartyModel(new[] { enemy1, enemy2 });

            var calculatorMock = Substitute.For<IActionGaugeCalculator>();
            // 1回のTickで満タン(1000)になると設定
            calculatorMock.CalculateIncrement(Arg.Any<BaseStats>(), Arg.Any<float>()).Returns(1000);

            // Act 1: 最初のTickで敵全員が満タンになる
            party.TickAllGauges(1.0f, calculatorMock);

            // Assert 1
            Assert.IsTrue(party.IsAnyEnemyReady);
            Assert.IsTrue(enemy1.CurrentGauge.CurrentValue.IsFull);

            // Act 2: 満タンの状態で、片方のゲージをリセットしてからさらにTickを呼ぶ
            enemy2.ResetGauge();

            // enemy1がReadyなのでウェイトモードが機能し、enemy2のゲージは増えないはず
            party.TickAllGauges(1.0f, calculatorMock);

            // Assert 2
            Assert.AreEqual(0, enemy2.CurrentGauge.CurrentValue.Value, "ウェイトモード中は他の敵キャラのゲージが増加してはいけない");
        }
    }
}