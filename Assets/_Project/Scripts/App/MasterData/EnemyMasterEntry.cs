using System;
using UnityEngine;

namespace App.MasterData
{
    /// <summary>
    /// Unityエディタでシリアライズ可能なエネミーマスターデータのエントリ
    /// </summary>
    [Serializable]
    public class EnemyMasterEntry
    {
        public string Id;
        public string DisplayName;
        public int MaxHp;
        public int MaxSp;
        public int Str;
        public int Int;
        public int Def;
        public int Mnd;
        public int Agi;
        public int Luk;
    }
}
