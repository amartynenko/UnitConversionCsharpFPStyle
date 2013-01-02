
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnitConversion
{
    [TestFixture]
    public class TheSimplestFixture : InvariantCultureFixture
    {
        [Test]
        public void TheSimplest1()
        {
            var result = TheSimplestInput1().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("3.000000 minute = 180.000000 second\r\n"));
        }

        [Test]
        public void TheSimplest2()
        {
            var result = TheSimplestInput2().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("3.000000 minute = 180.000000 second\r\n"));
        }

        [Test]
        public void TheSimplest3()
        {
            var result = TheSimplestInput3().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("180.000000 second = 3.000000 minute\r\n"));
        }

        [Test]
        public void TheSimplest4()
        {
            var result = TheSimplestInput4().CheckAndEvaluateEach().Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("180.000000 second = 3.000000 minute\r\n"));
        }

        #region the simplest input
        private IEnumerable<Func<dynamic>> TheSimplestInput1()
        {
            yield return () => new
            {
                Left = new { Amount = 120.0, UnitName = "second" },
                Right = new { Amount = 2, UnitName = "minute" }
            };

            yield return () => new
            {
                Left = new { Amount = 3, UnitName = "minute" },
                Right = new { Amount = new double?(), UnitName = "second" }
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
