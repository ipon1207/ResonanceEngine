using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Battle.Views
{
    public class BattleCharacterView : MonoBehaviour, IBattleCharacterView
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private Slider _gaugeSlider;

        public void Initialize(string displayName)
        {
            if (_nameText != null)
            {
                _nameText.text = displayName;
            }
        }

        public void UpdateHpBar(float ratio)
        {
            if (_hpSlider != null)
            {
                _hpSlider.value = ratio;
            }
        }

        public void UpdateGaugeBar(float ratio)
        {
            if (_gaugeSlider != null)
            {
                _gaugeSlider.value = ratio;
            }
        }
    }
}
