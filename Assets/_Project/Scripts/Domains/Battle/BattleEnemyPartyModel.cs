using System;
using System.Collections.Generic;
using System.Linq;

namespace Domains.Battle
{
    public class BattleEnemyPartyModel : IDisposable
    {
        public IReadOnlyList<BattleEnemyModel> Enemies { get; }

        public bool IsAnyEnemyReady => Enemies.Any(e => e.IsReady);

        public BattleEnemyPartyModel(IEnumerable<BattleEnemyModel> enemies)
        {
            if (enemies == null) throw new ArgumentNullException(nameof(enemies));
            
            Enemies = enemies.ToList();
        }

        public void TickAllGauges(float deltaTime, IActionGaugeCalculator calculator)
        {
            if (IsAnyEnemyReady) return;

            foreach (var enemy in Enemies)
            {
                enemy.TickGauge(deltaTime, calculator);
            }
        }

        public void Dispose()
        {
            foreach (var enemy in Enemies)
            {
                enemy.Dispose();
            }
        }
    }
}
