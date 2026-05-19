using R3;

namespace Features.Battle.Views
{
    public interface IBattleCommandView
    {
        Observable<Unit> OnWinButtonClicked { get; }
        Observable<Unit> OnLoseButtonClicked { get; }
        void Show();
        void Hide();
    }
}
