using Core.Utilities;

namespace Domains
{
    /// <summary>
    /// 素早さを管理する値オブジェクト
    /// </summary>
    public class Agility
    {
        /// <summary>
        /// 素早さ
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// 素早さを指定して素早さを生成
        /// </summary>
        /// <param name="value">素早さ</param>
        public Agility(int value)
        {
            CheckUtil.ZeroOrMore(value);

            Value = value;
        }
    }
}
