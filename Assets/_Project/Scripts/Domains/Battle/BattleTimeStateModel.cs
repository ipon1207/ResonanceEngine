using R3;
using System;

namespace Domains.Battle
{
    public class BattleTimeStateModel : IDisposable
    {
        private readonly ReactiveProperty<bool> _isPaused;
        
        public ReadOnlyReactiveProperty<bool> IsPaused => _isPaused;

        public BattleTimeStateModel()
        {
            _isPaused = new ReactiveProperty<bool>(false);
        }

        public void SetPause(bool isPause)
        {
            _isPaused.Value = isPause;
        }

        public void Dispose()
        {
            _isPaused.Dispose();
        }
    }
}
