using Core.Utilities;
using R3;
using UnityEngine;

namespace Domains.Enemy
{
    public readonly struct EncounterRadius
    {
        /// <summary>
        /// エンカウント判定半径の値オブジェクト
        /// </summary>
        public float Value { get; }

        public EncounterRadius(float value)
        {
            CheckUtil.ZeroOrMore(value);

            Value = value;
        }
    }

    public class EncounterModel : IEncounterModel
    {
        private readonly EncounterRadius _radius;
        private readonly ReactiveProperty<bool> _isEncountered;

        public ReadOnlyReactiveProperty<bool> IsEncountered => _isEncountered;

        public EncounterModel(EncounterRadius radius)
        {
            _radius = radius;
            _isEncountered = new ReactiveProperty<bool>(false);
        }

        public void CheckEncounter(Vector2 playerPosition, Vector2 enemyPosition)
        {
            if (_isEncountered.Value) return;

            var distance = Vector2.Distance(playerPosition, enemyPosition);
            if (distance < _radius.Value)
            {
                _isEncountered.Value = true;
            }
        }
    }
}
