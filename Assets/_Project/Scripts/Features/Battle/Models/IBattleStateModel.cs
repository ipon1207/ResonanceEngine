using R3;

namespace Features.Battle.Models
{
    public interface IBattleStateModel
    {
        ReadOnlyReactiveProperty<BattleState> CurrentState { get; }
        void SetVictory();
        void SetGameOver();
    }
}
