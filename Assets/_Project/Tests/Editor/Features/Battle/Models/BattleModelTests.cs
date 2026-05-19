using Features.Battle.Models;
using NUnit.Framework;
using UnityEditor.VersionControl;

namespace Tests.Editor.Features.Battle.Models
{
    public class BattleModelTests
    {
        private IBattleStateModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new BattleStateModel();
        }

        [Test]
        public void InitialState_IsCommandSelection()
        {
            // バトル開始直後は必ず「コマンド選択」状態であることを検証
            Assert.AreEqual(BattleState.CommandSelection, _model.CurrentState.CurrentValue);
        }

        [Test]
        public void SetVictory_ChangeStateToVictoryResult()
        {
            _model.SetVictory(); 

            // 勝利処理を呼ぶと、状態が勝利リザルトに変わることを検証
            Assert.AreEqual(BattleState.VictoryResult, _model.CurrentState.CurrentValue);
        }

        [Test]
        public void SetGameOver_ChangeStateToGameOver()
        {
            _model.SetGameOver();

            // 敗北処理を呼ぶと、状態がゲームオーバーに変わることを検証
            Assert.AreEqual(BattleState.GameOver, _model.CurrentState.CurrentValue);
        }

        [Test]
        public void StateChange_OnceResultIsSet_CannotChangesStateAgain()
        {
            _model.SetVictory();

            _model.SetGameOver();

            // 状態が上書きされず、最初の「勝利」のままであることを検証
            // （意図しない状態の上書き防止）
            Assert.AreEqual(BattleState.VictoryResult, _model.CurrentState.CurrentValue);
        }
    }
}