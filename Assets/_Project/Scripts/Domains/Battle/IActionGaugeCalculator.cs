using Domains.MasterData;

namespace Domains.Battle
{
    public interface IActionGaugeCalculator
    {
        int CalculateIncrement(BaseStats stats, float deltaTime);
    }
}
