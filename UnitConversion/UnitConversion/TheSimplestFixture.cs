using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitConversion
{
    [TestFixture]
    public class TheSimplestFixture
    {
        [Test]
        public void TheSimplest1()
        {
            var result = TheSimplestInput1().CheckAndEvaluateEach(new List<Func<dynamic>>()).Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("120 second = 2 minute\r\n3 minute = 180 second\r\n"));
        }

        [Test]
        public void TheSimplest2()
        {
            var result = TheSimplestInput2().CheckAndEvaluateEach(new List<Func<dynamic>>()).Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("2 minute = 120 second\r\n3 minute = 180 second\r\n"));
        }

        [Test]
        public void TheSimplest3()
        {
            var result = TheSimplestInput3().CheckAndEvaluateEach(new List<Func<dynamic>>()).Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("120 second = 2 minute\r\n180 second = 3 minute\r\n"));
        }

        [Test]
        public void TheSimplest4()
        {
            var result = TheSimplestInput4().CheckAndEvaluateEach(new List<Func<dynamic>>()).Format().ConcatAll();

            Console.WriteLine(result);
            Assert.That(result, Is.EqualTo("2 minute = 120 second\r\n180 second = 3 minute\r\n"));
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

    static class FunctionContainer
    {
        public static string ConcatAll(this IEnumerable<string> strings)
        {
            var sb = new StringBuilder();

            foreach (var s in strings)
                sb.Append(s);

            return sb.ToString();
        }

        public static IEnumerable<string> Format(this IEnumerable<Func<dynamic>> facts)
        {
            foreach (var fact in facts)
            {
                yield return string.Format("{0} {1} = {2} {3}\r\n",
                    fact().Left.Amount, fact().Left.UnitName,
                    fact().Right.Amount, fact().Right.UnitName);
            }
        }

        public static IEnumerable<Func<dynamic>> CheckAndEvaluateEach(this IEnumerable<Func<dynamic>> input, List<Func<dynamic>> evaluatedAlready)
        {
            foreach (var equation in input)
            {
                if (equation().Right.Amount == null)
                    evaluatedAlready.AddRange(equation.TryEvaluate(evaluatedAlready, new List<Func<dynamic>>()).Where(x => x() != null));
                else
                    evaluatedAlready.Add(equation);
            }

            foreach (var evaluatedEquation in evaluatedAlready)
                yield return evaluatedEquation;
        }

        public static Func<dynamic> Evaluate(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            if (unknown.LeftUnitMatchesFactLeftUnit(fact))
                return () => unknown.LeftEvaluate(fact);

            if (unknown.LeftUnitMatchesFactRightUnit(fact))
                return () => unknown.RightEvaluate(fact);

            return () => null;
        }

        private static object RightEvaluate(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return new
            {
                unknown().Left,
                Right = new { Amount = unknown().Left.Amount * (fact().Left.Amount / fact().Right.Amount), unknown().Right.UnitName }
            };
        }

        private static object LeftEvaluate(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return new
            {
                unknown().Left,
                Right = new { Amount = unknown().Left.Amount * (fact().Right.Amount / fact().Left.Amount), unknown().Right.UnitName }
            };
        }

        private static dynamic LeftUnitMatchesFactLeftUnit(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return unknown().Left.UnitName == fact().Left.UnitName;
        }

        private static dynamic LeftUnitMatchesFactRightUnit(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return unknown().Left.UnitName == fact().Right.UnitName;
        }

        public static List<Func<dynamic>> TryEvaluate(this Func<dynamic> unknown, IEnumerable<Func<dynamic>> alreadyEvaluated, List<Func<dynamic>> accumulator)
        {
            foreach (var fact in alreadyEvaluated)
                accumulator.Add(unknown.Evaluate(fact));

            return accumulator;
        }
    }
}
