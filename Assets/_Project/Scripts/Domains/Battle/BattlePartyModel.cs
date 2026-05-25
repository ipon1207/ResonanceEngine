using System;
using System.Collections.Generic;
using System.Linq;

namespace Domains.Battle
{
    public class BattlePartyModel : IDisposable
    {
        public IReadOnlyList<BattleCharacterModel> Characters { get; }

        public bool IsAnyCharacterReady => Characters.Any(c => c.CurrentGauge.CurrentValue.IsFull);

        public BattlePartyModel(IEnumerable<BattleCharacterModel> characters)
        {
            if (characters == null) throw new ArgumentNullException(nameof(characters));
            
            Characters = characters.ToList();
        }

        public void TickAllGauges(float deltaTime, IActionGaugeCalculator calculator)
        {
            // ウェイトモード: 誰か一人でもゲージが満タンなら全体の時間を止める
            if (IsAnyCharacterReady) return;

            foreach (var character in Characters)
            {
                character.TickGauge(deltaTime, calculator);
            }
        }

        public void Dispose()
        {
            foreach (var character in Characters)
            {
                character.Dispose();
            }
        }
    }
}
