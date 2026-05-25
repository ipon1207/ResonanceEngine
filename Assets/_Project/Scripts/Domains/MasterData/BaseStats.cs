using Core.Utilities;

namespace Domains.MasterData
{
    /// <summary>
    /// キャラクター・エネミー共通のベースステータスを表す値オブジェクト
    /// </summary>
    /// <remarks>
    /// 8つのステータスを1つの概念として凝集させることで、
    /// プリミティブ型への執着を避け、ステータス群の不変性とバリデーションを保証する。
    /// </remarks>
    public readonly struct BaseStats
    {
        /// <summary>最大HP</summary>
        public int MaxHp { get; }
        /// <summary>最大SP</summary>
        public int MaxSp { get; }
        /// <summary>物理攻撃力</summary>
        public int Str { get; }
        /// <summary>魔法攻撃力</summary>
        public int Int { get; }
        /// <summary>物理防御力</summary>
        public int Def { get; }
        /// <summary>魔法防御力</summary>
        public int Mnd { get; }
        /// <summary>素早さ</summary>
        public int Agi { get; }
        /// <summary>運</summary>
        public int Luk { get; }

        public BaseStats(int maxHp, int maxSp, int str, int @int, int def, int mnd, int agi, int luk)
        {
            // 全引数に対して0以上のバリデーション（完全コンストラクタ）
            CheckUtil.ZeroOrMore(maxHp);
            CheckUtil.ZeroOrMore(maxSp);
            CheckUtil.ZeroOrMore(str);
            CheckUtil.ZeroOrMore(@int);
            CheckUtil.ZeroOrMore(def);
            CheckUtil.ZeroOrMore(mnd);
            CheckUtil.ZeroOrMore(agi);
            CheckUtil.ZeroOrMore(luk);

            MaxHp = maxHp;
            MaxSp = maxSp;
            Str = str;
            Int = @int;
            Def = def;
            Mnd = mnd;
            Agi = agi;
            Luk = luk;
        }
    }
}
