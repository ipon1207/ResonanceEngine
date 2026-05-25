using App.MasterData;
using Domains.Character;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Editor.App.MasterData
{
    public class ScriptableObjectCharacterMasterRepositoryTests
    {
        private CharacterMasterEntry CreateEntry(
            string id, string name,
            int hp = 100, int sp = 50, int str = 10, int @int = 5,
            int def = 8, int mnd = 4, int agi = 12, int luk = 3)
        {
            return new CharacterMasterEntry
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
            var entries = new List<CharacterMasterEntry> { CreateEntry("hero_01", "勇者") };
            var repo = new ScriptableObjectCharacterMasterRepository(entries);

            // Act
            var data = repo.FindById(new CharacterId("hero_01"));

            // Assert
            Assert.AreEqual("hero_01", data.Id.Value);
            Assert.AreEqual("勇者", data.DisplayName);
            Assert.AreEqual(100, data.Stats.MaxHp);
            Assert.AreEqual(50, data.Stats.MaxSp);
            Assert.AreEqual(10, data.Stats.Str);
            Assert.AreEqual(5, data.Stats.Int);
            Assert.AreEqual(8, data.Stats.Def);
            Assert.AreEqual(4, data.Stats.Mnd);
            Assert.AreEqual(12, data.Stats.Agi);
            Assert.AreEqual(3, data.Stats.Luk);
        }

        [Test]
        public void FindById_NonExistentId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var entries = new List<CharacterMasterEntry> { CreateEntry("hero_01", "勇者") };
            var repo = new ScriptableObjectCharacterMasterRepository(entries);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() =>
                repo.FindById(new CharacterId("unknown_id")));
        }

        [Test]
        public void Constructor_DuplicateIds_ThrowsArgumentException()
        {
            // Arrange
            var entries = new List<CharacterMasterEntry>
            {
                CreateEntry("hero_01", "勇者A"),
                CreateEntry("hero_01", "勇者B")
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new ScriptableObjectCharacterMasterRepository(entries));
        }

        [Test]
        public void FindById_MultipleEntries_ReturnsCorrectOne()
        {
            // Arrange
            var entries = new List<CharacterMasterEntry>
            {
                CreateEntry("hero_01", "勇者", hp: 150),
                CreateEntry("mage_01", "魔法使い", hp: 80)
            };
            var repo = new ScriptableObjectCharacterMasterRepository(entries);

            // Act
            var data = repo.FindById(new CharacterId("mage_01"));

            // Assert
            Assert.AreEqual("魔法使い", data.DisplayName);
            Assert.AreEqual(80, data.Stats.MaxHp);
        }
    }
}
