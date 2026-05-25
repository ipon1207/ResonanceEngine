using Domains.Battle;
using Domains.Character;
using Domains.MasterData;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Editor.Domains.Battle
{
    public class BattlePartyModelTests
    {
        [Test]
        public void TickAllGauges_WhenAnyCharacterReady_DoesNotIncreaseGauge_WaitMode()
        {
            // Arrange
            var hero1 = new BattleCharacterModel(
                new CharacterMasterData(new CharacterId("h1"), "H1", new BaseStats(100, 50, 10, 10, 10, 10, 10, 10)));
            var hero2 = new BattleCharacterModel(
                new CharacterMasterData(new CharacterId("h2"), "H2", new BaseStats(100, 50, 10, 10, 10, 10, 10, 10)));

            var party = new BattlePartyModel(new[] { hero1, hero2 });

            var calculatorMock = Substitute.For<IActionGaugeCalculator>();
            calculatorMock.CalculateIncrement(Arg.Any<BaseStats>(), Arg.Any<float>()).Returns(1000); // 1回で満タンになる設定

            // Act 1: 最初のTickでhero1とhero2が両方満タンになる（厳密にはシステム次第だが、ここでは両方満タンと仮定）
            party.TickAllGauges(1.0f, calculatorMock);

            // Assert 1
            Assert.IsTrue(party.IsAnyCharacterReady);
            Assert.IsTrue(hero1.CurrentGauge.CurrentValue.IsFull);

            // Act 2: 満タンの状態でさらにTickを呼ぶ
            // 一旦hero2のゲージを0にリセットしたと仮定（直接リセットできなければ新しいモック構成などでテスト）
            hero2.ResetGauge();

            // ウェイトモードが正しく機能していれば、hero1がReadyなため、hero2のゲージは増えないはず
            party.TickAllGauges(1.0f, calculatorMock);

            // Assert 2
            Assert.AreEqual(0, hero2.CurrentGauge.CurrentValue.Value, "ウェイトモード中は他のキャラのゲージが増加してはいけない");
        }

    }
}
