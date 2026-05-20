using Core.Utilities;
using Domains.Character;
using Domains.Enemy;
using Domains.Session;
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
        private readonly IMovementModel _playerModel;
        private readonly IGameSessionModel _sessionModel;
        private readonly ISceneTransitionService _transitionService;
        private readonly CompositeDisposable _disposables = new();

        public EnemyPresenter(
            IEnemyPatrolModel patrolModel,
            IEncounterModel encounterModel,
            IEnemyView view,
            IMovementModel playerModel,
            IGameSessionModel sessionModel,
            ISceneTransitionService transitionService)
        {
            CheckUtil.ArgNotNull(patrolModel);
            CheckUtil.ArgNotNull(encounterModel);
            CheckUtil.ArgNotNull(view);
            CheckUtil.ArgNotNull(playerModel);
            CheckUtil.ArgNotNull(sessionModel);
            CheckUtil.ArgNotNull(transitionService);

            _patrolModel = patrolModel;
            _encounterModel = encounterModel;
            _view = view;
            _playerModel = playerModel;
            _sessionModel = sessionModel;
            _transitionService = transitionService;
        }

        public void Initialize()
        {
            // 既に倒されている敵なら、Viewを非表示にして以降の処理をしない
            if (_sessionModel.IsEnemyDefeated(_patrolModel.Id))
            {
                _view.Hide();
                return;
            }

            // 敵の座標変更をView(Transform)に反映
            _patrolModel.CurrentPosition
                .Subscribe(pos => _view.ApplyMovement(pos))
                .AddTo(_disposables);

            // エンカウント状態になったらパトロールを停止し、遷移処理を行う
            _encounterModel.IsEncountered
                .Where(isEncountered => isEncountered)
                .Subscribe(_ =>
                {
                    _patrolModel.Stop();

                    // 現在のプレイヤー座標を保存
                    _sessionModel.SavePlayerPosition(_playerModel.CurrentPosition.CurrentValue);
                    
                    // 今から戦う敵のIDを一時保存（まだ撃破リストには入れない）
                    _sessionModel.SetCurrentEncounter(_patrolModel.Id);

                    Debug.Log("【Encounter!】バトルシーンへ遷移します...");
                    _transitionService.LoadBattleScene();
                })
                .AddTo(_disposables);
        }

        public void Tick()
        {
            // 既にエンカウント済みか、あるいは倒されている場合は更新を止める
            if (_encounterModel.IsEncountered.CurrentValue || _sessionModel.IsEnemyDefeated(_patrolModel.Id)) return;

            _patrolModel.Tick(Time.deltaTime);

            // プレイヤーとの距離判定
            _encounterModel.CheckEncounter(_playerModel.CurrentPosition.CurrentValue, _patrolModel.CurrentPosition.CurrentValue);
        }

        public void Dispose() => _disposables.Dispose();
    }
}
