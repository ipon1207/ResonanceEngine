using Core.Utilities;
using System;

namespace Domains.Character
{
    /// <summary>
    /// キャラクターを一意に識別するための値オブジェクト
    /// </summary>
    public readonly struct CharacterId : IEquatable<CharacterId>
    {
        public string Value { get; }

        public CharacterId(string value)
        {
            // Nullを許容しない（完全コンストラクタ）
            CheckUtil.ArgNotNull(value);
            Value = value;
        }

        public bool Equals(CharacterId other) => Value == other.Value;
        public override bool Equals(object obj) => obj is CharacterId other && Equals(other);
        public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

        public static bool operator ==(CharacterId left, CharacterId right) => left.Equals(right);
        public static bool operator !=(CharacterId left, CharacterId right) => !left.Equals(right);
        public override string ToString() => Value;
    }
}
