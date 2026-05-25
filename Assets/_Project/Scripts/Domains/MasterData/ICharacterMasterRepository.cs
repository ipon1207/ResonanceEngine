using Domains.Character;

namespace Domains.MasterData
{
    /// <summary>
    /// キャラクターマスターデータのリポジトリインターフェース
    /// </summary>
    /// <remarks>
    /// データソース（ScriptableObject, JSON等）の詳細を隠蔽し、
    /// ドメイン層からはこのインターフェースのみに依存する（DIP）。
    /// </remarks>
    public interface ICharacterMasterRepository
    {
        /// <summary>
        /// 指定したIDのキャラクターマスターデータを取得する。
        /// 存在しないIDの場合はKeyNotFoundExceptionをスローする。
        /// </summary>
        CharacterMasterData FindById(CharacterId id);
    }
}
