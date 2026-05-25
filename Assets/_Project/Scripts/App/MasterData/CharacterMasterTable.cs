using System.Collections.Generic;
using UnityEngine;

namespace App.MasterData
{
    /// <summary>
    /// 全キャラクターのマスターデータをリストとして保持するテーブル型ScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterMasterTable", menuName = "MasterData/Character Table")]
    public class CharacterMasterTable : ScriptableObject
    {
        [SerializeField] private List<CharacterMasterEntry> _entries = new();

        public IReadOnlyList<CharacterMasterEntry> Entries => _entries;
    }
}
