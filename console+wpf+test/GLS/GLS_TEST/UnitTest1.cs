using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GLS_CLI;

namespace GLS_Tests
{
    [TestClass]
    public class AutóAdatokTests
    {
        [TestMethod]
        public void ÁtlagosFogyasztásSzámítás_10LiterPer100Km_Returns10()
        {
            // Arrange
            int fogyasztás = 10;
            int megtettKm = 100;
            double expected = 10.0;

            // Act
            double actual = AutóAdatok.ÁtlagosFogyasztásSzámítás(fogyasztás, megtettKm);

            // Assert
            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void ÁtlagosFogyasztásSzámítás_16LiterPer200Km_Returns8()
        {
            // Arrange
            int fogyasztás = 16;
            int megtettKm = 200;
            double expected = 8.0;

            // Act
            double actual = AutóAdatok.ÁtlagosFogyasztásSzámítás(fogyasztás, megtettKm);

            // Assert
            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void ÁtlagosFogyasztásSzámítás_0LiterPer0Km_Returns0()
        {
            // Arrange
            int fogyasztás = 0;
            int megtettKm = 0;
            double expected = 0.0;

            // Act
            double actual = AutóAdatok.ÁtlagosFogyasztásSzámítás(fogyasztás, megtettKm);

            // Assert
            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void ÁtlagosFogyasztásSzámítás_NegativValues_Returns0()
        {
            // Arrange
            int fogyasztás = -5;
            int megtettKm = 100;
            double expected = 0.0;

            // Act
            double actual = AutóAdatok.ÁtlagosFogyasztásSzámítás(fogyasztás, megtettKm);

            // Assert
            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void ÁtlagosFogyasztásSzámítás_ZeroKm_Returns0()
        {
            // Arrange
            int fogyasztás = 10;
            int megtettKm = 0;
            double expected = 0.0;

            // Act
            double actual = AutóAdatok.ÁtlagosFogyasztásSzámítás(fogyasztás, megtettKm);

            // Assert
            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void ÁtlagosFogyasztásSzámítás_ZeroConsumption_Returns0()
        {
            // Arrange
            int fogyasztás = 0;
            int megtettKm = 100;
            double expected = 0.0;

            // Act
            double actual = AutóAdatok.ÁtlagosFogyasztásSzámítás(fogyasztás, megtettKm);

            // Assert
            Assert.AreEqual(expected, actual, 0.001);
        }
    }
}