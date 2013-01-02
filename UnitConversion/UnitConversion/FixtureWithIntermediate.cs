using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnitConversion
{
    [TestFixture]
    public class FixtureWithIntermediate
    {
        [Test]
        public void Test1()
        {
            var result = Input1().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("120 second = 2 minute\r\n4 hour = 240 minute\r\n3600 second = 1 hour\r\n"));
        }

        [Test]
        public void Test2()
        {
            var result = Input2().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("120 second = 2 minute\r\n4 hour = 240 minute\r\n1 hour = 3600 second\r\n"));
        }

        #region the input with intermediate equations
        private IEnumerable<Func<dynamic>> Input1()
        {
            yield return () => new
            {
                Left = new { Amount = 120.0, UnitName = "second" },
                Right = new { Amount = 2, UnitName = "minute" }
            };

            yield return () => new
            {
                Left = new { Amount = 4.0, UnitName = "hour" },
                Right = new { Amount = 240, UnitName = "minute" }
            };

            yield return () => new
            {
                Left = new { Amount = 3600, UnitName = "second" },
                Right = new { Amount = new double?(), UnitName = "hour" }
            };
        }

        private IEnumerable<Func<dynamic>> Input2()
        {
            yield return () => new
            {
                Left = new { Amount = 120.0, UnitName = "second" },
                Right = new { Amount = 2, UnitName = "minute" }
            };

            yield return () => new
            {
                Left = new { Amount = 4.0, UnitName = "hour" },
                Right = new { Amount = 240, UnitName = "minute" }
            };

            yield return () => new
            {
                Left = new { Amount = 1, UnitName = "hour" },
                Right = new { Amount = new double?(), UnitName = "second" }
            };
        }
    }
        #endregion
}