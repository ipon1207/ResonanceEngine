using Domains.MasterData;
using UnityEngine;

namespace Domains.Battle
{
    public class SimpleActionGaugeCalculator : IActionGaugeCalculator
    {
        public int CalculateIncrement(BaseStats stats, float deltaTime)
        {
            // 仮の計算式: 基礎値 + AGI * 係数 をフレームごとの時間で掛ける
            float baseSpeed = 10f;
            float agiFactor = 2f;
            float speed = baseSpeed + (stats.Agi * agiFactor);
            
            // 1フレームあたりの増加量を返す (ここではdeltaTimeに依存させる)
            return Mathf.RoundToInt(speed * deltaTime * 60f);
        }
    }
}
