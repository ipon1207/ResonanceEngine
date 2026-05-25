using Core.Utilities;
using Domains.Character;
using Domains.MasterData;
using R3;
using System;

namespace Domains.Battle
{
    public class BattleCharacterModel : IDisposable
    {
        public CharacterMasterData MasterData { get; }

        private readonly ReactiveProperty<HitPoint> _currentHp;
        public ReadOnlyReactiveProperty<HitPoint> CurrentHp => _currentHp;

        private readonly ReactiveProperty<ActionGauge> _currentGauge;
        public ReadOnlyReactiveProperty<ActionGauge> CurrentGauge => _currentGauge;

        public BattleCharacterModel(CharacterMasterData masterData)
        {
            CheckUtil.ArgNotNull(masterData);
            MasterData = masterData;

            // 初期化: HPは最大、ゲージは0からスタート
            _currentHp = new ReactiveProperty<HitPoint>(new HitPoint(masterData.Stats.MaxHp, masterData.Stats.MaxHp));
            // ゲージの最大値は仮に1000固定とする（将来的にマスター等から取得可能）
            _currentGauge = new ReactiveProperty<ActionGauge>(new ActionGauge(0, 1000));
        }

        public void ApplyDamage(int damage)
        {
            CheckUtil.ZeroOrMore(damage);
            _currentHp.Value = _currentHp.Value.Damage(damage);
        }

        public void TickGauge(float deltaTime, IActionGaugeCalculator calculator)
        {
            if (_currentGauge.Value.IsFull) return;

            int increment = calculator.CalculateIncrement(MasterData.Stats, deltaTime);
            _currentGauge.Value = _currentGauge.Value.Add(increment);
        }

        public void ResetGauge()
        {
            _currentGauge.Value = _currentGauge.Value.Reset();
        }

        public void Dispose()
        {
            _currentHp.Dispose();
            _currentGauge.Dispose();
        }
    }
}
