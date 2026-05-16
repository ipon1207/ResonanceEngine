using Core.Utilities;
using System;

namespace Domains.Enemy
{
    /// <summary>
    /// 敵を一意に識別するための値オブジェクト
    /// </summary>
    public readonly struct EnemyId : IEquatable<EnemyId>
    {
        public string Value { get; }

        public EnemyId(string value)
        {
            // Nullや空文字を許容しない（完全コンストラクタ）
            CheckUtil.ArgNotNull(value);
            Value = value;
        }

        public bool Equals(EnemyId other) => Value == other.Value;
        public override bool Equals(object obj) => obj is EnemyId other && Equals(other);
        public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;
        
        public static bool operator ==(EnemyId left, EnemyId right) => left.Equals(right);
        public static bool operator !=(EnemyId left, EnemyId right) => !left.Equals(right);
        public override string ToString() => Value;
    }
}
