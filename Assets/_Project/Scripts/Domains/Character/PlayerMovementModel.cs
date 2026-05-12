using Core.Utilities;
using R3;
using UnityEngine;

namespace Domains.Character
{
    /// <summary>
    /// キャラクターの移動速度を表す値オブジェクト
    /// </summary>
    public readonly struct Speed
    {
        public float Value { get; }

        public Speed(float value)
        {
            CheckUtil.ZeroOrMore(value);

            Value = value;
        }
    }

    /// <summary>
    /// 移動処理に関するModelのインターフェース
    /// </summary>
    public interface IMovementModel
    {
        ReadOnlyReactiveProperty<Vector2> CurrentPosition { get; }
        void Move(Vector2 input, float deltaTime);
        void SetPosition(Vector2 newPosition);
    }

    public class PlayerMovementModel : IMovementModel
    {
        private readonly Speed _speed;
        /// <summary>
        /// 内部では書き換え可能なReactivePropertyを保持
        /// </summary>
        private readonly ReactiveProperty<Vector2> _currentPosition;
        /// <summary>
        /// 外部へは読み取り専用として公開
        /// </summary>
        public ReadOnlyReactiveProperty<Vector2> CurrentPosition => _currentPosition;

        public PlayerMovementModel(Speed speed, Vector2 initialPosition = default)
        {
            _speed = speed;
            _currentPosition = new ReactiveProperty<Vector2>(initialPosition);
        }

        /// <summary>
        /// 入力方向に基づく移動処理
        /// </summary>
        /// <param name="input"></param>
        /// <param name="deltaTime"></param>
        public void Move(Vector2 input, float deltaTime)
        {
            if (input == Vector2.zero) return;
            
            // 入力の正規化
            // （入力ベクトルの長さが1を超える場合のみ正規化することで、スティックの浅い倒しこみに対応できるようにする）
            var normalizedInput = input.magnitude > 1f ? input.normalized : input;
            // 移動量の計算
            var deltaMove = normalizedInput * (_speed.Value * deltaTime);
            // 状態の更新
            _currentPosition.Value += deltaMove;
        }

        public void SetPosition(Vector2 newPosition)
        {
            if (_currentPosition.Value == newPosition) return;

            _currentPosition.Value = newPosition;
        }
    }
}