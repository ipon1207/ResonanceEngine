using System;
using UnityEngine;

namespace App.MasterData
{
    /// <summary>
    /// Unityエディタでシリアライズ可能なキャラクターマスターデータのエントリ
    /// </summary>
    /// <remarks>
    /// ドメインクラス（CharacterMasterData）を直接 [Serializable] にすると
    /// Unity依存が混入するため、エディタ用のデータ転送クラスとして分離している。
    /// </remarks>
    [Serializable]
    public class CharacterMasterEntry
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
