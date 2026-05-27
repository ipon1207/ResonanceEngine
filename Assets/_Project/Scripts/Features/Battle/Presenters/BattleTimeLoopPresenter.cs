using Cysharp.Threading.Tasks;
using Domains.Battle;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

namespace Features.Battle.Presenters
{
    public class BattleTimeLoopPresenter : IAsyncStartable
    {
        private readonly BattlePartyModel _party;
        private readonly BattleEnemyPartyModel _enemyParty;
        private readonly BattleTimeStateModel _state;
        private readonly IActionGaugeCalculator _calculator;

        public BattleTimeLoopPresenter(
            BattlePartyModel party,
            BattleEnemyPartyModel enemyParty,
            BattleTimeStateModel state,
            IActionGaugeCalculator calculator)
        {
            _party = party;
            _enemyParty = enemyParty;
            _state = state;
            _calculator = calculator;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            // Unityのフレームライフサイクルに合わせて毎フレーム時間を進めるメインループ
            while (!cancellation.IsCancellationRequested)
            {
                // 次のフレーム（Updateタイミング）まで待機
                await UniTask.Yield(PlayerLoopTiming.Update, cancellation);

                // 実処理
                UpdateStep(Time.deltaTime);
            }
        }

        public void UpdateStep(float deltaTime)
        {
            // 1. システムによるポーズフラグが立っている場合は時間進行をスキップ
            if (_state.IsPaused.CurrentValue) return;

            // 2. 誰か一人でも行動可能（Ready）なら、ウェイトモードとして時間進行をスキップ
            if (_party.IsAnyCharacterReady || _enemyParty.IsAnyEnemyReady) return;

            // 3. 味方・敵 全員のゲージを進行させる
            _party.TickAllGauges(deltaTime, _calculator);
            _enemyParty.TickAllGauges(deltaTime, _calculator);
        }
    }
}
