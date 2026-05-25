using Domains.MasterData;
using NUnit.Framework;
using System;

namespace Tests.Editor.Domains.MasterData
{
    public class BaseStatsTests
    {
        [Test]
        public void Constructor_ValidValues_CreatesInstance()
        {
            var stats = new BaseStats(100, 50, 10, 5, 8, 4, 12, 3);

            Assert.AreEqual(100, stats.MaxHp);
            Assert.AreEqual(50, stats.MaxSp);
            Assert.AreEqual(10, stats.Str);
            Assert.AreEqual(5, stats.Int);
            Assert.AreEqual(8, stats.Def);
            Assert.AreEqual(4, stats.Mnd);
            Assert.AreEqual(12, stats.Agi);
            Assert.AreEqual(3, stats.Luk);
        }

        [TestCase(-1, 0, 0, 0, 0, 0, 0, 0)] // MaxHpが負
        [TestCase(0, -1, 0, 0, 0, 0, 0, 0)] // MaxMpが負
        [TestCase(0, 0, -1, 0, 0, 0, 0, 0)] // Strが負
        [TestCase(0, 0, 0, 0, 0, 0, 0, -1)] // Lukが負
        public void Constructor_NegativeValues_ThrowsArgumentOutOfRangeException(
            int hp, int mp, int str, int mat, int def, int mdf, int agi, int luk)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new BaseStats(hp, mp, str, mat, def, mdf, agi, luk));
        }
    }
}
