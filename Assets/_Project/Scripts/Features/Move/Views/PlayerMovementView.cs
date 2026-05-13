using R3;
using UnityEngine;

namespace Features.Move.Views
{
    public interface IMovementView 
    {
        Observable<Vector2> OnMoveInput { get; }
        void ApplyMovement(Vector2 idealPosition);
        Vector2 GetActualPosition();
    }

    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementView : MonoBehaviour, IMovementView
    {
        private CharacterController _characterController;
        private readonly Subject<Vector2> _onMoveInput = new();

        public Observable<Vector2> OnMoveInput => _onMoveInput;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            _onMoveInput.OnNext(new Vector2(x, y));
        }

        public void ApplyMovement(Vector2 idealPosition2D)
        {
            // Modelの2D座標(X, Z)をUnityの3D座標(X, 現在のY, Z)に変換
            var idealPosition3D = new Vector3(idealPosition2D.x, transform.position.y, idealPosition2D.y);
            // 現在値から理想の座標への差分ベクトルを計算
            var delta = idealPosition3D - transform.position;
            // CharacterControllerを使って移動
            // （壁に当たれば自動でスライド）
            _characterController.Move(delta);
        }

        public Vector2 GetActualPosition()
        {
            // 衝突補正された実際の3D座標を、Model用の2D座標(X, Z)として返す
            var pos = transform.position;
            return new Vector2(pos.x, pos.z);
        }

        private void OnDestroy()
        {
            _onMoveInput.OnCompleted();
            _onMoveInput.Dispose();
        }
    }
}