using Domains.Character;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor.Domains.Character
{
    public class PlayerMovementModelTests
    {
        private Speed DefaultSpeed = new Speed(5f);

        /// <summary>
        /// 前方への入力があった際、正しく座標が更新されるかを検証
        /// 基本的な移動ロジックが動作することを確認
        /// </summary>
        [Test]
        public void Move_InputForward_PositionIncreasesInZ()
        {
            var model = new PlayerMovementModel(DefaultSpeed);
            var initialPosition = model.CurrentPosition.CurrentValue;
            var input = new Vector2(0, 1); // Wキー（前）
            var deltaTime = 1.0f;

            model.Move(input, deltaTime);

            Assert.AreEqual(initialPosition.y + DefaultSpeed.Value, model.CurrentPosition.CurrentValue.y, 0.001f);
        }

        /// <summary>
        /// Step 0のドメインルール「入力の正規化」を検証
        /// 斜め入力時に移動速度が√2倍にならないよう、内部でベクトルが正規化されていることを確認
        /// </summary>
        [Test]
        public void Move_DiagonalInput_IsNormalized()
        {
            // 斜め入力は (1, 1) は長さが √2 になる
            var model = new PlayerMovementModel(DefaultSpeed);
            var input = new Vector2(1, 1);
            var deltaTime = 1.0f;

            model.Move(input, deltaTime);

            // 正規化されている場合、移動距離は DefaultSpeed と等しくなるはず
            // 初期位置 (0, 0) からの距離が DefaultSpeed かどうか
            var distance = model.CurrentPosition.CurrentValue.magnitude;
            Assert.AreEqual(DefaultSpeed.Value, distance, 0.0001f);
        }

        /// <summary>
        /// 壁との衝突後にView側から座標を書き戻す（同期する）ための機能を検証
        /// </summary>
        [Test]
        public void SetPosition_UpdatesCurrentPosition()
        {
            var model = new PlayerMovementModel(DefaultSpeed);
            var newPosition = new Vector2(10, 20);

            model.SetPosition(newPosition);

            Assert.AreEqual(newPosition, model.CurrentPosition.CurrentValue);
        }
    }
}