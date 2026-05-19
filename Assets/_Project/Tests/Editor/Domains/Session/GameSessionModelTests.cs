using NUnit.Framework;
using UnityEngine;
using Domains.Session;
using Domains.Enemy;

namespace Tests.Editor.Domains.Session
{
    public class GameSessionModelTests
    {
        private IGameSessionModel _sessionModel;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _sessionModel = new GameSessionModel();
        }

        [Test]
        public void InitialState_HasSavedPosition_ShouldBeFalse()
        {
            // Act & Assert
            Assert.IsFalse(_sessionModel.HasSavedPosition);
        }

        [Test]
        public void SavePlayerPosition_ValidPosition_UpdatesSavedPosition()
        {
            // Arrange
            var testPosition = new Vector2(5f, 10f);

            // Act
            _sessionModel.SavePlayerPosition(testPosition);

            // Assert
            Assert.IsTrue(_sessionModel.HasSavedPosition);
            Assert.AreEqual(testPosition, _sessionModel.SavedPlayerPosition);
        }

        [Test]
        public void IsEnemyDefeated_UnknownEnemyId_ReturnsFalse()
        {
            // Arrange
            var testEnemyId = new EnemyId("enemy_001");

            // Act & Assert
            Assert.IsFalse(_sessionModel.IsEnemyDefeated(testEnemyId));
        }

        [Test]
        public void RecordDefeatedEnemy_ValidEnemyId_MarksEnemyAsDefeated()
        {
            // Arrange
            var testEnemyId = new EnemyId("enemy_001");

            // Act
            _sessionModel.RecordDefeatedEnemy(testEnemyId);

            // Assert
            Assert.IsTrue(_sessionModel.IsEnemyDefeated(testEnemyId));
        }

        [Test]
        public void RecordDefeatedEnemy_MultipleEnemies_TracksAllCorrectly()
        {
            // Arrange
            var enemyId1 = new EnemyId("enemy_001");
            var enemyId2 = new EnemyId("enemy_002");
            var enemyId3 = new EnemyId("enemy_003");

            // Act
            _sessionModel.RecordDefeatedEnemy(enemyId1);
            _sessionModel.RecordDefeatedEnemy(enemyId2);

            // Assert
            Assert.IsTrue(_sessionModel.IsEnemyDefeated(enemyId1));
            Assert.IsTrue(_sessionModel.IsEnemyDefeated(enemyId2));
            Assert.IsFalse(_sessionModel.IsEnemyDefeated(enemyId3));
        }

        [Test]
        public void ClearSavedPosition_WhenPositionIsSaved_ClearsDataAndFlag()
        {
            // Arrange
            var testPosition = new Vector2(5f, 10f);
            _sessionModel.SavePlayerPosition(testPosition);

            // 事前確認（保存されていること）
            Assert.IsTrue(_sessionModel.HasSavedPosition);

            // Act
            _sessionModel.ClearSavedPosition();

            // Assert
            // フラグがFalseになり、座標が(0,0)などの初期値にリセットされていることを検証
            Assert.IsFalse(_sessionModel.HasSavedPosition);
            Assert.AreEqual(Vector2.zero, _sessionModel.SavedPlayerPosition);
        }
    }
}