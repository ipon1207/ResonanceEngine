using Domains.Enemy;

namespace Domains.MasterData
{
    /// <summary>
    /// エネミーマスターデータのリポジトリインターフェース
    /// </summary>
    public interface IEnemyMasterRepository
    {
        /// <summary>
        /// 指定したIDのエネミーマスターデータを取得する。
        /// 存在しないIDの場合はKeyNotFoundExceptionをスローする。
        /// </summary>
        EnemyMasterData FindById(EnemyId id);
    }
}
