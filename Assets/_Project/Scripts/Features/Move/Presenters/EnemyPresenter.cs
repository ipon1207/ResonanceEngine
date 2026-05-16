using Core.Utilities;
using Domains.Character;
using Domains.Enemy;
using Features.Move.Views;
using R3;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Features.Move.Presenters
{
    public class EnemyPresenter : IInitializable, ITickable, IDisposable
    {
        private readonly IEnemyPatrolModel _patrolModel;
        private readonly IEncounterModel _encounterModel;
        private readonly IEnemyView _view;
        // プレイヤーの座標監視用
        private readonly IMovementModel _playerModel;
        private readonly CompositeDisposable _disposables = new();

        public EnemyPresenter(
            IEnemyPatrolModel patrolModel,
            IEncounterModel encounterModel,
            IEnemyView view,
            IMovementModel playerModel)
        {
            CheckUtil.ArgNotNull(patrolModel);
            CheckUtil.ArgNotNull(encounterModel);
            CheckUtil.ArgNotNull(view);
            CheckUtil.ArgNotNull(playerModel);

            _patrolModel = patrolModel;
            _encounterModel = encounterModel;
            _view = view;
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            // 敵の座標変更をView(Transform)に反映
            _patrolModel.CurrentPosition
                .Subscribe(pos => _view.ApplyMovement(pos))
                .AddTo(_disposables);

            // エンカウント状態になったらパトロールを停止
            _encounterModel.IsEncountered
                .Where(isEncountered => isEncountered)
                .Subscribe(_ =>
                {
                    _patrolModel.Stop();
                    Debug.Log("【Encounter!】バトルシーンへ遷移します...");
                })
                .AddTo(_disposables);
        }

        public void Tick()
        {
            // 既にエンカウント済みなら更新を止める
            if (_encounterModel.IsEncountered.CurrentValue) return;

            _patrolModel.Tick(Time.deltaTime);

            // プレイヤーとの距離判定
            // Modelに依頼
            _encounterModel.CheckEncounter(_playerModel.CurrentPosition.CurrentValue, _patrolModel.CurrentPosition.CurrentValue);
        }

        public void Dispose() => _disposables.Dispose();
    }
}
