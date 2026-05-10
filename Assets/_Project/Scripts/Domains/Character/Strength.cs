using Core.Utilities;

namespace Domains
{
    /// <summary>
    /// 攻撃力を管理する値オブジェクト
    /// </summary>
    public class Strength
    {
        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// 攻撃力を指定して攻撃力を生成
        /// </summary>
        /// <param name="value">攻撃力</param>
        public Strength(int value)
        {
            CheckUtil.ZeroOrMore(value);

            Value = value;
        }
    }
}