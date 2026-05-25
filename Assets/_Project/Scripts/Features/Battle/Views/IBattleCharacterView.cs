namespace Features.Battle.Views
{
    public interface IBattleCharacterView
    {
        void Initialize(string displayName);
        void UpdateHpBar(float ratio);
        void UpdateGaugeBar(float ratio);
    }
}
