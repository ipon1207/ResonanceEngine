using Domains.Enemy;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor.Domains.Enemy
{
    /// <summary>
    /// エンカウント判定に関するテスト
    /// </summary>
    public class EncounterModelTests
    {
        public void CheckEncounter_WithDistance_SetsIsEncounteredTrue()
        {
            var model = new EncounterModel(new EncounterRadius(2.0f));
            var playerPos = new Vector2(0, 0);
            var enemyPos = new Vector2(1.5f, 0);

            model.CheckEncounter(playerPos, enemyPos);

            Assert.IsTrue(model.IsEncountered.CurrentValue);
        }

        [Test]
        public void CheckEncounter_OutsideDistance_StaysFalse()
        {
            var model = new EncounterModel(new EncounterRadius(2.0f));
            var playerPos = new Vector2(0, 0);
            var enemyPos = new Vector2(3.0f, 0);

            model.CheckEncounter(playerPos, enemyPos);

            Assert.IsFalse(model.IsEncountered.CurrentValue);
        }
    }
}