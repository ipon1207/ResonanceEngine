using NUnit.Framework;
using R3;
using Features.Battle.Models;

namespace Tests.Editor.Features.Battle.Models
{
    public class BattleStateModelTests
    {
        private IBattleStateModel _model;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _model = new BattleStateModel();
        }

        [Test]
        public void InitialState_IsCommandSelection()
        {
            // Act & Assert
            // バトル開始直後は必ず「コマンド選択中」であることを検証
            Assert.AreEqual(BattleState.CommandSelection, _model.CurrentState.CurrentValue);
        }

        [Test]
        public void SetVictory_ChangesStateToVictoryResult()
        {
            // Act
            _model.SetVictory();

            // Assert
            // 勝利処理を呼ぶと、状態が勝利リザルトに変わることを検証
            Assert.AreEqual(BattleState.VictoryResult, _model.CurrentState.CurrentValue);
        }

        [Test]
        public void SetGameOver_ChangesStateToGameOver()
        {
            // Act
            _model.SetGameOver();

            // Assert
            // 敗北処理を呼ぶと、状態がゲームオーバーに変わることを検証
            Assert.AreEqual(BattleState.GameOver, _model.CurrentState.CurrentValue);
        }

        [Test]
        public void StateChange_OnceResultIsSet_CannotChangeStateAgain()
        {
            // Arrange
            _model.SetVictory();

            // Act
            // 既に勝利が決まっているのに、さらに敗北や勝利を上書きしようとする
            _model.SetGameOver();

            // Assert
            // 状態が上書きされず、最初の「勝利」のままであることを検証（意図しない状態の上書き防止）
            Assert.AreEqual(BattleState.VictoryResult, _model.CurrentState.CurrentValue);
        }
    }
}