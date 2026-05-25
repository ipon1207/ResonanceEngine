using System;
using Core.Utilities;

namespace Domains.Battle
{
    public readonly struct ActionGauge : IEquatable<ActionGauge>
    {
        public int Value { get; }
        public int Max { get; }

        public float Ratio => Max == 0 ? 0 : (float)Value / Max;
        public bool IsFull => Value >= Max;

        public ActionGauge(int value, int max)
        {
            CheckUtil.ZeroOrMore(value);
            CheckUtil.ZeroOrMore(max);
            
            Value = value;
            Max = max;
        }

        public ActionGauge Add(int amount)
        {
            CheckUtil.ZeroOrMore(amount);
            var nextValue = Math.Min(Max, Value + amount);
            return new ActionGauge(nextValue, Max);
        }

        public ActionGauge Reset()
        {
            return new ActionGauge(0, Max);
        }

        public bool Equals(ActionGauge other) => Value == other.Value && Max == other.Max;
        public override bool Equals(object obj) => obj is ActionGauge other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Value, Max);
        public static bool operator ==(ActionGauge left, ActionGauge right) => left.Equals(right);
        public static bool operator !=(ActionGauge left, ActionGauge right) => !left.Equals(right);
    }
}
