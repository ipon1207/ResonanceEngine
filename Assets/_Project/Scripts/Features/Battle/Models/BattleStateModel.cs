using R3;

namespace Features.Battle.Models
{
    public class BattleStateModel : IBattleStateModel
    {
        private readonly ReactiveProperty<BattleState> _currentState = new(BattleState.CommandSelection);

        public ReadOnlyReactiveProperty<BattleState> CurrentState => _currentState;

        public void SetVictory()
        {
            // 一度決着がついていたら何もしない（早期リターン）
            if (_currentState.Value != BattleState.CommandSelection) return;

            _currentState.Value = BattleState.VictoryResult;
        }

        public void SetGameOver()
        {
            // 一度決着がついていたら何もしない（早期リターン）
            if (_currentState.Value != BattleState.CommandSelection) return;

            _currentState.Value = BattleState.GameOver;
        }
    }
}
