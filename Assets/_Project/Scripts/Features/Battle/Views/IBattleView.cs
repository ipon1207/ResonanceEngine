using R3;

namespace Features.Battle.Views
{
    public interface IBattleView
    {
        /// <summary>
        /// マップへ戻るボタンが押された時のイベント
        /// </summary>
        Observable<Unit> OnReturnButtonClicked { get; }
    }
}
