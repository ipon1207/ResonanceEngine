using Core.Utilities;
using Domains.Character;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Domains.Enemy
{
    public interface IEnemyPatrolModel
    {
        EnemyId Id { get; }
        ReadOnlyReactiveProperty<Vector2> CurrentPosition { get; }
        void Tick(float deltaTime);
        void Stop();
    }

    public interface IEncounterModel
    {
        ReadOnlyReactiveProperty<bool> IsEncountered { get; }
        void CheckEncounter(Vector2 playerPosition, Vector2 enemyPosition);
    }

    /// <summary>
    /// 敵の巡回ロジックを管理するModel
    /// Ping-Pong移動
    /// </summary>
    public class EnemyPatrolModel : IEnemyPatrolModel
    {
        public EnemyId Id { get; }
        private readonly IReadOnlyList<Vector2> _waypoints;
        private readonly Speed _speed;
        private ReactiveProperty<Vector2> _currentPosition;

        private int _currentTargetIndex;
        private bool _isMovingForward = true;
        private bool _isStopped = false;

        public ReadOnlyReactiveProperty<Vector2> CurrentPosition => _currentPosition;

        public EnemyPatrolModel(EnemyId id, IReadOnlyList<Vector2> waypoints, Speed speed, Vector2 initialPosition = default)
        {
            CheckUtil.ArgNotNull(waypoints);
            CheckUtil.IsPositive(waypoints.Count);

            Id = id;
            _waypoints = waypoints;
            _speed = speed;
            _currentPosition = new ReactiveProperty<Vector2>(initialPosition);
            // 最初はインデックス1 (次のWaypoint) に向かう
            _currentTargetIndex = _waypoints.Count > 1 ? 1 : 0;
        }

        public void Tick(float deltaTime)
        {
            if (_isStopped) return;

            var targetPos = _waypoints[_currentTargetIndex];
            var currentPos = _currentPosition.Value;
            var distanceToTarget = Vector2.Distance(currentPos, targetPos);
            var moveDistance = _speed.Value * deltaTime;

            if (distanceToTarget <= moveDistance)
            {
                _currentPosition.Value = targetPos;
                UpdateTargetIndex();
            }
            else
            {
                // まだ到達していない場合は、目標に向かって移動
                var direction = (targetPos - currentPos).normalized;
                _currentPosition.Value += direction * moveDistance;
            }
        }

        public void Stop()
        {
            _isStopped = true;
        }

        private void UpdateTargetIndex()
        {
            // Waypointが1つ以下の場合は移動しない
            if (_waypoints.Count <= 1) return;

            if (_isMovingForward)
            {
                _currentTargetIndex++;
                if (_currentTargetIndex >= _waypoints.Count)
                {
                    // 終点に到達したら、一つ手前のインデックスに戻り、逆走を開始
                    _currentTargetIndex = _waypoints.Count - 2;
                    _isMovingForward = false;
                }
            }
            else
            {
                _currentTargetIndex--;
                if (_currentTargetIndex < 0)
                {
                    // 始点に到達したら、次のインデックスに進み、順走を開始
                    _currentTargetIndex = 1;
                    _isMovingForward = true;
                }
            }
        }
    }
}