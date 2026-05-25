using Core.Utilities;
using Domains.Enemy;

namespace Domains.MasterData
{
    /// <summary>
    /// エネミーのマスターデータレコード
    /// </summary>
    /// <remarks>
    /// 1体のエネミーのマスター定義（ID、表示名、ステータス）を不変に保持する。
    /// 将来的にドロップ情報、AIパターンID等が追加される予定。
    /// </remarks>
    public class EnemyMasterData
    {
        public EnemyId Id { get; }
        public string DisplayName { get; }
        public BaseStats Stats { get; }

        public EnemyMasterData(EnemyId id, string displayName, BaseStats stats)
        {
            CheckUtil.ArgNotNull(displayName);

            Id = id;
            DisplayName = displayName;
            Stats = stats;
        }
    }
}
