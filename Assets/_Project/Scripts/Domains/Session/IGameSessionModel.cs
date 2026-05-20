using Domains.Enemy;
using UnityEngine;

namespace Domains.Session
{
    public interface IGameSessionModel
    {
        /// <summary>保存されたプレイヤー座標が存在するかどうか</summary>
        bool HasSavedPosition { get; }
        
        /// <summary>保存されたプレイヤー座標</summary>
        Vector2 SavedPlayerPosition { get; }

        /// <summary>現在エンカウントしている（戦闘中の）敵のID</summary>
        EnemyId? CurrentEncounterEnemyId { get; }

        /// <summary>プレイヤーの座標を保存する</summary>
        void SavePlayerPosition(Vector2 position);

        /// <summary>エンカウントした敵のIDを一時保存する</summary>
        void SetCurrentEncounter(EnemyId enemyId);

        /// <summary>敵を撃破済みとして記録する</summary>
        void RecordDefeatedEnemy(EnemyId enemyId);

        /// <summary>指定した敵が撃破済みかどうかを判定する</summary>
        bool IsEnemyDefeated(EnemyId enemyId);

        /// <summary>保存されたプレイヤー座標や一時保存されたエンカウント情報を破棄し、初期状態からの再開を促す</summary>
        void ClearSessionData();
    }
}
