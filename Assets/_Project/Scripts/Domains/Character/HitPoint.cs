using System;
using Core.Utilities;

namespace Domains
{
    /// <summary>
    /// 現在のHP（ヒットポイント）と最大HPを管理する値オブジェクト
    /// </summary>
    /// <remarks>このクラスはHPの現在値と最大値を不変として保持します。HPの値は0以上かつ最大HP以下である必要がある</remarks>
    public class HitPoint
    {
        /// <summary>
        /// 現在HP
        /// </summary>
        public int Value { get; private set; }
        /// <summary>
        /// 最大HP
        /// </summary>
        public int MaxValue { get; private set; }

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
    }
}