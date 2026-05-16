using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Battle.Views
{
    /// <summary>
    /// バトルシーンのUIを管理するパッシブビュー
    /// </summary>
    public class BattleView : MonoBehaviour, IBattleView
    {
        [SerializeField]
        private Button _returnToMapButton;

        // ButtonのクリックイベントをR3のObservableとして公開
        public Observable<Unit> OnReturnButtonClicked => _returnToMapButton.OnClickAsObservable();
    }
}
