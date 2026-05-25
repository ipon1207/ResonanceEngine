using App.MasterData;
using Domains.Enemy;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Editor.App.MasterData
{
    public class ScriptableObjectEnemyMasterRepositoryTests
    {
        private EnemyMasterEntry CreateEntry(
            string id, string name,
            int hp = 50, int sp = 20, int str = 8, int @int = 3,
            int def = 5, int mnd = 2, int agi = 6, int luk = 1)
        {
            return new EnemyMasterEntry
            {
                Id = id,
                DisplayName = name,
                MaxHp = hp,
                MaxSp = sp,
                Str = str,
                Int = @int,
                Def = def,
                Mnd = mnd,
                Agi = agi,
                Luk = luk
            };
        }

        [Test]
        public void FindById_ExistingId_ReturnsCorrectData()
        {
            // Arrange
            var entries = new List<EnemyMasterEntry> { CreateEntry("slime_01", "スライム") };
            var repo = new ScriptableObjectEnemyMasterRepository(entries);

            // Act
            var data = repo.FindById(new EnemyId("slime_01"));

            // Assert
            Assert.AreEqual("slime_01", data.Id.Value);
            Assert.AreEqual("スライム", data.DisplayName);
            Assert.AreEqual(50, data.Stats.MaxHp);
        }

        [Test]
        public void FindById_NonExistentId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var entries = new List<EnemyMasterEntry> { CreateEntry("slime_01", "スライム") };
            var repo = new ScriptableObjectEnemyMasterRepository(entries);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() =>
                repo.FindById(new EnemyId("unknown_id")));
        }

        [Test]
        public void Constructor_DuplicateIds_ThrowsArgumentException()
        {
            // Arrange
            var entries = new List<EnemyMasterEntry>
            {
                CreateEntry("slime_01", "スライムA"),
                CreateEntry("slime_01", "スライムB")
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new ScriptableObjectEnemyMasterRepository(entries));
        }
    }
}
