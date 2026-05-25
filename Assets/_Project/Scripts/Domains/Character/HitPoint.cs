using System;
using Core.Utilities;

namespace Domains
{
    /// <summary>
    /// 現在のHP（ヒットポイント）と最大HPを管理する値オブジェクト
    /// </summary>
    /// <remarks>このクラスはHPの現在値と最大値を不変として保持します。HPの値は0以上かつ最大HP以下である必要がある</remarks>
    public readonly struct HitPoint : IEquatable<HitPoint>
    {
        /// <summary>
        /// 現在HP
        /// </summary>
        public int Value { get; }
        /// <summary>
        /// 最大HP
        /// </summary>
        public int MaxValue { get; }

        public float Ratio => MaxValue == 0 ? 0 : (float)Value / MaxValue;

        /// <summary>
        /// 現在HPと最大HPを指定してHPを生成
        /// </summary>
        /// <param name="value">現在HP</param>
        /// <param name="maxValue">最大HP</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public HitPoint(int value, int maxValue)
        {
            CheckUtil.ZeroOrMore(value);
            CheckUtil.ZeroOrMore(maxValue);
            if (value > maxValue) throw new ArgumentOutOfRangeException("現在HPが最大HPを超えています");

            Value = value;
            MaxValue = maxValue;
        }

        public HitPoint Damage(int amount)
        {
            CheckUtil.ZeroOrMore(amount);
            var nextValue = Math.Max(0, Value - amount);
            return new HitPoint(nextValue, MaxValue);
        }

        public HitPoint Recover(int amount)
        {
            CheckUtil.ZeroOrMore(amount);
            var nextValue = Math.Min(MaxValue, Value + amount);
            return new HitPoint(nextValue, MaxValue);
        }

        public bool Equals(HitPoint other) => Value == other.Value && MaxValue == other.MaxValue;
        public override bool Equals(object obj) => obj is HitPoint other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Value, MaxValue);
        public static bool operator ==(HitPoint left, HitPoint right) => left.Equals(right);
        public static bool operator !=(HitPoint left, HitPoint right) => !left.Equals(right);
    }
}