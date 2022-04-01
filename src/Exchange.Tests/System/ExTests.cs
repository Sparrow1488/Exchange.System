using ExchangeSystem.Helpers;
using NUnit.Framework;
using System;

namespace Exchange.Tests.System
{
    public class ExTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void ThrowIfTrue_ArgumentExceptionAndSimpleCondition_ThrowArgumentException()
        {
            int argument = -1;
            Assert.Throws<ArgumentException>(() =>
                Ex.ThrowIfTrue<ArgumentException>(() => argument < 0, "Argument was less than zero!"));
        }

        [Test]
        public void ThrowIfNull_ArgumentNullExceptionAndSimpleCondition_ThrowArgumentNullException()
        {
            string argument = null;
            Assert.Throws<ArgumentNullException>(() =>
                Ex.ThrowIfNull(argument, "Argument was null!"));
        }

        [Test]
        public void ThrowIfTrue_ArgumentExceptionAndTrue_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                Ex.ThrowIfTrue<ArgumentException>(1 == 1));
        }
    }
}