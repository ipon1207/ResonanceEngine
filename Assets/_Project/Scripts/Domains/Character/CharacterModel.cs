using Core.Utilities;

namespace Domains
{
    public class CharacterModel
    {
        /// <summary>
        /// HP
        /// </summary>
        public HitPoint HP { get; private set; }
        /// <summary>
        /// 物理攻撃力
        /// </summary>
        public Strength STR { get; private set; }
        /// <summary>
        /// 素早さ
        /// </summary>
        public Agility AGI { get; private set; }

        /// <summary>
        /// HP、物理攻撃力、素早さを指定してキャラクターモデルを生成
        /// </summary>
        /// <param name="hp">HP</param>
        /// <param name="str">物理攻撃力</param>
        /// <param name="agi">素早さ</param>
        public CharacterModel(HitPoint hp, Strength str, Agility agi)
        {

            CheckUtil.ArgNotNull(str);
            CheckUtil.ArgNotNull(agi);

            HP = hp;
            STR = str;
            AGI = agi;
        }
    }
}