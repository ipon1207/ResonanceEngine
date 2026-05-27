using Core.Utilities;
using Domains.Character;
using Domains.MasterData;
using R3;
using System;

namespace Domains.Battle
{
    public class BattleEnemyModel : IBattleParticipant, IDisposable
    {
        public EnemyMasterData MasterData { get; }

        private readonly ReactiveProperty<HitPoint> _currentHp;
        public ReadOnlyReactiveProperty<HitPoint> CurrentHp => _currentHp;

        private readonly ReactiveProperty<ActionGauge> _currentGauge;
        public ReadOnlyReactiveProperty<ActionGauge> CurrentGauge => _currentGauge;

        public bool IsReady => _currentGauge.Value.IsFull;

        public BattleEnemyModel(EnemyMasterData masterData)
        {
            CheckUtil.ArgNotNull(masterData);
            MasterData = masterData;

            _currentHp = new ReactiveProperty<HitPoint>(new HitPoint(masterData.Stats.MaxHp, masterData.Stats.MaxHp));
            _currentGauge = new ReactiveProperty<ActionGauge>(new ActionGauge(0, 1000));
        }

        public void ApplyDamage(int damage)
        {
            CheckUtil.ZeroOrMore(damage);
            _currentHp.Value = _currentHp.Value.Damage(damage);
        }

        public void TickGauge(float deltaTime, IActionGaugeCalculator calculator)
        {
            if (IsReady) return;

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
