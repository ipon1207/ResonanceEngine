using Core.Utilities;
using Domains.Character;

namespace Domains.MasterData
{
    /// <summary>
    /// キャラクターのマスターデータレコード
    /// </summary>
    /// <remarks>
    /// 1体のキャラクターのマスター定義（ID、表示名、ステータス）を不変に保持する。
    /// </remarks>
    public class CharacterMasterData
    {
        public CharacterId Id { get; }
        public string DisplayName { get; }
        public BaseStats Stats { get; }

        public CharacterMasterData(CharacterId id, string displayName, BaseStats stats)
        {
            // displayNameのnullチェック（早期リターン / ガード節）
            CheckUtil.ArgNotNull(displayName);

            Id = id;
            DisplayName = displayName;
            Stats = stats;
        }
    }
}
