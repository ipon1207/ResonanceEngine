namespace Domains.Battle
{
    public interface IBattleParticipant
    {
        bool IsReady { get; }
        void TickGauge(float deltaTime, IActionGaugeCalculator calculator);
    }
}
