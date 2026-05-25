using NUnit.Framework;

namespace Tests.Editor.Domains.Battle
{
    public class ActionGaugeTests
    {
        [Test]
        public void Constructor_ValidValues_CreatesInstance()
        {
            // Arrange & Act
            var gauge = new ActionGauge(500, 1000);

            // Assert
            Assert.AreEqual(500, gauge.Value);
            Assert.AreEqual(1000, gauge.Max);
            Assert.AreEqual(0.5f, gauge.Ratio);
            Assert.IsFalse(gauge.IsFull);
        }

        [Test]
        public void Add_NormalAmount_IncreasesValue()
        {
            // Arrange
            var gauge = new ActionGauge(0, 1000);

            // Act
            var nextGauge = gauge.Add(300);

            // Assert
            Assert.AreEqual(300, nextGauge.Value);
            // 元のインスタンス（不変）は変更されていないこと
            Assert.AreEqual(0, gauge.Value);
        }

        [Test]
        public void Add_ExceedMax_ClampsToMaxAndBecomesFull()
        {
            // Arrange
            var gauge = new ActionGauge(800, 1000);

            // Act
            var nextGauge = gauge.Add(500); // 800 + 500 = 1300

            // Assert
            Assert.AreEqual(1000, nextGauge.Value);
            Assert.IsTrue(nextGauge.IsFull);
        }

        [Test]
        public void Reset_SetsValueToZero()
        {
            // Arrange
            var gauge = new ActionGauge(1000, 1000);

            // Act
            var nextGauge = gauge.Reset();

            // Assert
            Assert.AreEqual(0, nextGauge.Value);
            Assert.IsFalse(nextGauge.IsFull);
        }
    }
}
