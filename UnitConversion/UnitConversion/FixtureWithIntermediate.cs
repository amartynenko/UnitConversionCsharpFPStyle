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
            var result = TheSimplestInput2().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("2 minute = 120 second\r\n3 minute = 180 second\r\n"));
        }

        [Test]
        public void Test3()
        {
            var result = TheSimplestInput3().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("120 second = 2 minute\r\n180 second = 3 minute\r\n"));
        }

        [Test]
        public void Test4()
        {
            var result = TheSimplestInput4().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("2 minute = 120 second\r\n180 second = 3 minute\r\n"));
        }

        #region the simplest input
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

        private IEnumerable<Func<dynamic>> TheSimplestInput2()
        {
            yield return () => new
            {
                Left = new { Amount = 2, UnitName = "minute" },
                Right = new { Amount = 120.0, UnitName = "second" }
            };

            yield return () => new
            {
                Left = new { Amount = 3, UnitName = "minute" },
                Right = new { Amount = new double?(), UnitName = "second" }
            };
        }

        private IEnumerable<Func<dynamic>> TheSimplestInput3()
        {
            yield return () => new
            {
                Left = new { Amount = 120.0, UnitName = "second" },
                Right = new { Amount = 2, UnitName = "minute" }
            };

            yield return () => new
            {
                Left = new { Amount = 180, UnitName = "second" },
                Right = new { Amount = new double?(), UnitName = "minute" }
            };
        }

        private IEnumerable<Func<dynamic>> TheSimplestInput4()
        {
            yield return () => new
            {
                Left = new { Amount = 2, UnitName = "minute" },
                Right = new { Amount = 120.0, UnitName = "second" }
            };

            yield return () => new
            {
                Left = new { Amount = 180, UnitName = "second" },
                Right = new { Amount = new double?(), UnitName = "minute" }
            };
        }
    }
        #endregion
}