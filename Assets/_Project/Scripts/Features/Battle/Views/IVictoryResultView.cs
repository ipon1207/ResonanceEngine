using R3;

namespace Features.Battle.Views
{
    public interface IVictoryResultView
    {
        Observable<Unit> OnReturnButtonClicked { get; }
        void Show();
        void Hide();
    }
}
