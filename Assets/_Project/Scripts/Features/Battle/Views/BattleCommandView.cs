using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Battle.Views
{
    public class BattleCommandView : MonoBehaviour, IBattleCommandView
    {
        [SerializeField] private Button _winButton;
        [SerializeField] private Button _loseButton;

        public Observable<Unit> OnWinButtonClicked => _winButton.OnClickAsObservable();
        public Observable<Unit> OnLoseButtonClicked => _loseButton.OnClickAsObservable();

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
