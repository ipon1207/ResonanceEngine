using UnityEngine;

namespace Features.Move.Views
{
    public interface IEnemyView
    {
        void ApplyMovement(Vector2 idealPosition);
        Vector2 GetActualPosition();
    }

    /// <summary>
    /// 敵キャラクターのView
    /// 物理的な移動や衝突判定は行わず、Modelから指示された座標を描画するだけ
    /// </summary>
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public void ApplyMovement(Vector2 idealPosition2D)
        {
            // Modelの2D座標(X, Z)をUnityの3D空間(X, 現在のY, Z)に変換して直接代入
            transform.position = new Vector3(idealPosition2D.x, transform.position.y, idealPosition2D.y);
        }

        public Vector2 GetActualPosition()
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }
}
