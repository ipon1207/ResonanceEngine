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

        /// <summary>プレイヤーの座標を保存する</summary>
        void SavePlayerPosition(Vector2 position);

        /// <summary>敵を撃破済みとして記録する</summary>
        void RecordDefeatedEnemy(EnemyId enemyId);

        /// <summary>指定した敵が撃破済みかどうかを判定する</summary>
        bool IsEnemyDefeated(EnemyId enemyId);

        /// <summary>保存されたプレイヤー座標を破棄し、初期位置からの再開を促す</summary>
        void ClearSavedPosition();
    }
}
