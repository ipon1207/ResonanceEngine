using Domains.Character;
using Domains.MasterData;
using NUnit.Framework;
using System;

namespace Tests.Editor.Domains.MasterData
{
    public class CharacterMasterDataTests
    {
        [Test]
        public void Constructor_ValidArguments_CreatesInstance()
        {
            var id = new CharacterId("hero_01");
            var stats = new BaseStats(100, 50, 10, 5, 8, 4, 12, 3);

            var data = new CharacterMasterData(id, "勇者", stats);

            Assert.AreEqual(id, data.Id);
            Assert.AreEqual("勇者", data.DisplayName);
            Assert.AreEqual(100, data.Stats.MaxHp);
        }

        [Test]
        public void Constructor_NullDisplayName_ThrowsArgumentNullException()
        {
            var id = new CharacterId("hero_01");
            var stats = new BaseStats(100, 50, 10, 5, 8, 4, 12, 3);

            Assert.Throws<ArgumentNullException>(() =>
                new CharacterMasterData(id, null, stats));
        }
    }
}
