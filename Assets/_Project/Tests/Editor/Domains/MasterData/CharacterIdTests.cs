using Domains.Character;
using NUnit.Framework;
using System;

namespace Tests.Editor.Domains.MasterData
{
    public class CharacterIdTests
    {
        [Test]
        public void Constrcutor_ValidString_CreatesInstance()
        {
            var id = new CharacterId("hero_01");

            Assert.AreEqual("hero_01", id.Value);
        }

        [Test]
        public void Constructor_NullString_ThrowAgrumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CharacterId(null));
        }

        [Test]
        public void Equals_SameValue_ReturnsTrue()
        {
            var id1 = new CharacterId("hero_01");
            var id2 = new CharacterId("hero_01");

            Assert.IsTrue(id1.Equals(id2));
            Assert.IsTrue(id1 == id2);
        }
    }
}