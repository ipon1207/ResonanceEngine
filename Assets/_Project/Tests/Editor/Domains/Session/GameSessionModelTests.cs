using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor.Domains.Session
{
    public class GameSessionModelTests : MonoBehaviour
    {
        private IGameSessionModel _sessionModel;

        [SetUp]
        public void SetUp()
        {
            _sessionModel = new GameSessionModel();
        }

        [Test]
        public void InitialState_HasSavedPosition_ShouldBeFalse()
        {
            Assert.IsFalse(_sessionModel.HasSavedPosition);
        }

        [Test]
        public void SavePlayerPosition_ValidPosition_UpdateSavedPosition()
        {
            var testPosition = new Vector2(5f, 10f);

            _sessionModel.SavePlayerPosition(testPosition);

            Assert.IsTrue(_sessionModel.HasSavedPosition);
            Assert.AreEqual(testPosition, _sessionModel.SavedPlayerPosition);
        }

        [Test]
        public void IsEnemyDefeated_UnknownEnemyId_ReturnsFalse()
        {
            var testEnemyId = "enemy_001";

            Assert.IsFalse(_sessionModel.IsEnemyDefeated(testEnemyId));
        }

        [Test]
        public void RecordDefeatedEnemy_ValidEnemyId_MarksEnemyAsDefeated()
        {
            var testEnemyId = "enemy_001";

            _sessionModel.RecordDefeatedEnemy(testEnemyId);

            Assert.IsTrue(_sessionModel.IsEnemyDefeated(testEnemyId));
        }

        [Test]
        public void RecordDefeatedEnemy_MultipleEnemies_TracksAllCorrectly()
        {
            var enemyId1 = "enemy_001";
            var enemyId2 = "enemy_002";

            _sessionModel.RecordDefeatedEnemy(enemyId1);
            _sessionModel.RecordDefeatedEnemy(enemyId2);

            Assert.IsTrue(_sessionModel.IsEnemyDefeated(enemyId1));
            Assert.IsTrue(_sessionModel.IsEnemyDefeated(enemyId2));
            Assert.IsFalse(_sessionModel.IsEnemyDefeated("enemy_003"));
        }
    }
}
