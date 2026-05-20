using Domains.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Domains.Session
{
    public class GameSessionModel : IGameSessionModel
    {
        // 撃破済み敵のIDを保持するコレクション (検索が高速なHashSetを採用)
        private readonly HashSet<EnemyId> _defeatedEnemies = new();

        public bool HasSavedPosition { get; private set; } = false;
        public Vector2 SavedPlayerPosition { get; private set; } = Vector2.zero;
        public EnemyId? CurrentEncounterEnemyId { get; private set; } = null;

        public void SavePlayerPosition(Vector2 position)
        {
            SavedPlayerPosition = position;
            HasSavedPosition = true;
        }

        public void SetCurrentEncounter(EnemyId enemyId)
        {
            CurrentEncounterEnemyId = enemyId;
        }

        public void RecordDefeatedEnemy(EnemyId enemyId)
        {
            // 重複を許さないHashSetに追加
            _defeatedEnemies.Add(enemyId);
        }

        public bool IsEnemyDefeated(EnemyId enemyId)
        {
            return _defeatedEnemies.Contains(enemyId);
        }

        public void ClearSessionData()
        {
            HasSavedPosition = false;
            SavedPlayerPosition = Vector2.zero;
            CurrentEncounterEnemyId = null;
        }
    }
}
