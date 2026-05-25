using System.Collections.Generic;
using UnityEngine;

namespace App.MasterData
{
    /// <summary>
    /// 全エネミーのマスターデータをリストとして保持するテーブル型ScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyMasterTable", menuName = "MasterData/Enemy Table")]
    public class EnemyMasterTable : ScriptableObject
    {
        [SerializeField] private List<EnemyMasterEntry> _entries = new();

        public IReadOnlyList<EnemyMasterEntry> Entries => _entries;
    }
}
