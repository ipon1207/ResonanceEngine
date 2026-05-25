using System;
using System.Collections.Generic;
using Domains.Character;
using Domains.MasterData;

namespace App.MasterData
{
    /// <summary>
    /// ScriptableObjectベースのキャラクターマスターデータリポジトリ実装
    /// </summary>
    /// <remarks>
    /// コンストラクタでエントリリストをドメインオブジェクトに変換し、
    /// Dictionary にキャッシュすることで高速な検索を実現する。
    /// </remarks>
    public class ScriptableObjectCharacterMasterRepository : ICharacterMasterRepository
    {
        private readonly Dictionary<CharacterId, CharacterMasterData> _dataMap = new();

        public ScriptableObjectCharacterMasterRepository(IReadOnlyList<CharacterMasterEntry> entries)
        {
            if (entries == null) throw new ArgumentNullException(nameof(entries));

            foreach (var entry in entries)
            {
                var id = new CharacterId(entry.Id);
                var stats = new BaseStats(
                    entry.MaxHp, entry.MaxSp, entry.Str, entry.Int,
                    entry.Def, entry.Mnd, entry.Agi, entry.Luk);
                var data = new CharacterMasterData(id, entry.DisplayName, stats);

                // IDの一意性チェック（ドメインルール）
                if (_dataMap.ContainsKey(id))
                {
                    throw new ArgumentException($"キャラクターマスターデータのID '{id.Value}' が重複しています。");
                }
                _dataMap.Add(id, data);
            }
        }

        public CharacterMasterData FindById(CharacterId id)
        {
            if (!_dataMap.TryGetValue(id, out var data))
            {
                throw new KeyNotFoundException(
                    $"ID '{id.Value}' のキャラクターマスターデータが見つかりません。");
            }
            return data;
        }
    }
}
