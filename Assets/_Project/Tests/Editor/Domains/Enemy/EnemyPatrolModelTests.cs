using Domains.Character;
using Domains.Enemy;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.Editor.Domains.Enemy
{
    /// <summary>
    /// 敵の巡回ロジックに関するテスト
    /// </summary>
    public class EnemyPatrolModelTests
    {
        [Test]
        public void Tick_MovesTowardsFirstWaypoint()
        {
            var waypoint = new List<Vector2> { new(0, 0), new(10, 0) };
            var model = new EnemyPatrolModel(waypoint, new Speed(5f), Vector2.zero);

            model.Tick(1.0f);

            // 右方向に speed 分移動しているはず
            Assert.AreEqual(5f, model.CurrentPosition.CurrentValue.x, 0.001f);
            Assert.AreEqual(0f, model.CurrentPosition.CurrentValue.y, 0.001f);
        }

        [Test]
        public void Tick_ReachedWaypoint_SwitchesToNext()
        {
            var waypoint = new List<Vector2> { new(0, 0), new(5, 0), new(5, 5) };
            var model = new EnemyPatrolModel(waypoint, new Speed(5f), Vector2.zero);

            model.Tick(1.0f);
            model.Tick(1.0f);

            // (5, 0) から上方向に 5f 移動して (5, 5) になっているはず
            Assert.AreEqual(5f, model.CurrentPosition.CurrentValue.x, 0.001f);
            Assert.AreEqual(5f, model.CurrentPosition.CurrentValue.y, 0.001f);
        }

        [Test]
        public void Tick_ReachedLastWaypoint_ReversesDirection_PingPong()
        {
            var waypoints = new List<Vector2> { new(0, 0), new(10, 0) };
            // 初期設計通り、1秒で10f進み、即座に(10,0)に到達させるために速度を10fに修正
            var model = new EnemyPatrolModel(waypoints, new Speed(10f), Vector2.zero);

            model.Tick(1.0f);
            model.Tick(0.5f);

            // (5, 0) に戻っているはず
            Assert.AreEqual(5f, model.CurrentPosition.CurrentValue.x, 0.001f);
        }

        [Test]
        public void Stop_PreventFurtherMovement()
        {
            var waypoints = new List<Vector2> { new(0, 0), new(10, 0) };
            var model = new EnemyPatrolModel(waypoints, new Speed(5f));
            var posBeforeStop = model.CurrentPosition.CurrentValue;

            model.Stop();
            model.Tick(1.0f);

            // 停止後はTickを呼んでも座標が変わらないはず
            Assert.AreEqual(posBeforeStop.x, model.CurrentPosition.CurrentValue.x);
        }
    }
}