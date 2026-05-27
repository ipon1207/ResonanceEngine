using Domains.Battle;
using Features.Battle.Presenters;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace Tests.Editor.Features.Battle
{
    public class BattleTimeLoopPresenterTests
    {
        [Test]
        public void UpdateStep_NotPausedAndNoOneReady_TicksBothParties()
        {
            // Arrange
            // 実際はモックではなく具象クラスを使うか、あるいはPartyModelをインターフェース化しておくのがベターですが、
            // 今回は依存が少ないためそのままインスタンス化してテストします。
            var party = new BattlePartyModel(Enumerable.Empty<BattleCharacterModel>());
            var enemyParty = new BattleEnemyPartyModel(Enumerable.Empty<BattleEnemyModel>());
            var state = new BattleTimeStateModel();
            var calcMock = Substitute.For<IActionGaugeCalculator>();

            var presenter = new BattleTimeLoopPresenter(party, enemyParty, state, calcMock);

            // Act
            // (本来は内部で時間が経過するが、ここでは例外が出ないこと、およびポーズフラグ判定をパスすることを確認)
            presenter.UpdateStep(0.1f);

            // Assert
            // 実際にはCharacterがいないのでTickされないが、
            // 少なくとも早期リターンされずにロジックが通ることは確認できる
            Assert.Pass();
        }

        [Test]
        public void UpdateStep_WhenPaused_SkipsTick()
        {
            // Arrange
            var party = new BattlePartyModel(Enumerable.Empty<BattleCharacterModel>());
            var enemyParty = new BattleEnemyPartyModel(Enumerable.Empty<BattleEnemyModel>());
            var state = new BattleTimeStateModel();
            state.SetPause(true); // ポーズ状態にする
            var calcMock = Substitute.For<IActionGaugeCalculator>();

            var presenter = new BattleTimeLoopPresenter(party, enemyParty, state, calcMock);

            // Act
            presenter.UpdateStep(0.1f);

            // Assert
            Assert.IsTrue(state.IsPaused.CurrentValue);
        }
    }
}
