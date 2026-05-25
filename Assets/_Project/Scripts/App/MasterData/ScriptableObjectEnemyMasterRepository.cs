using System;
using System.Collections.Generic;
using Domains.Enemy;
using Domains.MasterData;

namespace App.MasterData
{
    /// <summary>
    /// ScriptableObjectベースのエネミーマスターデータリポジトリ実装
    /// </summary>
    public class ScriptableObjectEnemyMasterRepository : IEnemyMasterRepository
    {
        private readonly Dictionary<EnemyId, EnemyMasterData> _dataMap = new();

        public ScriptableObjectEnemyMasterRepository(IReadOnlyList<EnemyMasterEntry> entries)
        {
            if (entries == null) throw new ArgumentNullException(nameof(entries));

            foreach (var entry in entries)
            {
                var id = new EnemyId(entry.Id);
                var stats = new BaseStats(
                    entry.MaxHp, entry.MaxSp, entry.Str, entry.Int,
                    entry.Def, entry.Mnd, entry.Agi, entry.Luk);
                var data = new EnemyMasterData(id, entry.DisplayName, stats);

                if (_dataMap.ContainsKey(id))
                {
                    throw new ArgumentException($"エネミーマスターデータのID '{id.Value}' が重複しています。");
                }
                _dataMap.Add(id, data);
            }
        }

        public EnemyMasterData FindById(EnemyId id)
        {
            if (!_dataMap.TryGetValue(id, out var data))
            {
                throw new KeyNotFoundException(
                    $"ID '{id.Value}' のエネミーマスターデータが見つかりません。");
            }
            return data;
        }
    }
}
