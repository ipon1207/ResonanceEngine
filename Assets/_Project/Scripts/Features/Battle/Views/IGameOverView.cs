using R3;

namespace Features.Battle.Views
{
    public interface IGameOverView
    {
        Observable<Unit> OnReturnButtonClicked { get; }
        void Show();
        void Hide();
    }
}
