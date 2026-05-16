using Domains.Character;
using Domains.Enemy;
using Features.Move.Presenters;
using Features.Move.Views;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Features.Move
{
    public class EnemyLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private string _enemyId = "enemy_001";
        [SerializeField]
        private EnemyView _enemyView;
        [SerializeField]
        private float _speed = 3f;
        [SerializeField]
        private float _encounterRadius = 2f;

        [Header("Patrol Settings")]
        [Tooltip("巡回するワールド座標を順に指定する")]
        [SerializeField]
        private List<Vector2> _waypoints = new();

        protected override void Configure(IContainerBuilder builder)
        {
            // シーン配置時の現在座標を初期位置とする
            var initialPos = new Vector2(transform.position.x, transform.position.z);

            // waypointsが未設定の場合は、その場に留まるように設定
            var validWaypoints = _waypoints.Count > 0 ? _waypoints : new List<Vector2> { initialPos };

            // PatrolModelの登録
            builder.Register<IEnemyPatrolModel>(resolver =>
               new EnemyPatrolModel(new EnemyId(_enemyId), validWaypoints, new Speed(_speed), initialPos),
                Lifetime.Scoped);

            // EncounterModelの登録
            builder.Register<IEncounterModel>(resolver =>
                new EncounterModel(new EncounterRadius(_encounterRadius)),
                Lifetime.Scoped);

            // Viewの登録
            builder.RegisterComponent<IEnemyView>(_enemyView);

            // Presenterの登録
            // IMovementModel (Player) は、親のMoveLifetimeScopeから自動敵に注入
            builder.RegisterEntryPoint<EnemyPresenter>(Lifetime.Scoped);
        }
    }
}
