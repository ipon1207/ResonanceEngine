using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Battle.Views
{
    public class GameOverView : MonoBehaviour, IGameOverView
    {
        [SerializeField] private Button _returnButton;

        public Observable<Unit> OnReturnButtonClicked => _returnButton.OnClickAsObservable();

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
