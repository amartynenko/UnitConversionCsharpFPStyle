using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace UnitConversion
{
    [TestFixture]
    public class MainFixture : InvariantCultureFixture
    {
        [Test]
        public void Default()
        {
            LoadAndRunTestCase("default");
        }

        [Test]
        public void Test()
        {
            LoadAndRunTestCase("test");
        }

        [Test]
        public void Test1()
        {
            LoadAndRunTestCase("test1");
        }

        [Test]
        [ExpectedException]
        public void Bad()
        {
            LoadAndRunTestCase("bad");
        }

        [Test]
        public void Mixed()
        {
            LoadAndRunTestCase("mixed");
        }

        [Test]
        public void Inconsistent()
        {
            LoadAndRunTestCase("inconsistent");
        }

        private void LoadAndRunTestCase(string testName)
        {
            var input = ReadInputForTest(testName);
            var result = Parse(input).CheckAndEvaluateEach().Format().ConcatAll();

            AssertIfHasExpected(result, testName);

            Console.WriteLine(result);
        }

        private static void AssertIfHasExpected(string result, string testName)
        {
            if (File.Exists("TestData\\" + testName + ".out"))
            {
                var expected = File.ReadAllText("TestData\\" + testName + ".out");
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        private static string[] ReadInputForTest(string name)
        {
            return File.ReadAllLines("TestData\\" + name + ".in");
        }

        private IEnumerable<Func<dynamic>> Parse(IEnumerable<string> input)
        {
            foreach (var s in input)
            {
                var splitted = s.Split(' ');

                yield return () => new
                {
                    Left = new { Amount = double.Parse(splitted[0]), UnitName = splitted[1] },
                    Right = new { Amount = parseNullableDouble(splitted[3]), UnitName = splitted[4] },
                };
            }
        }

        private static double? parseNullableDouble(string s)
        {
            double parsed;
            return double.TryParse(s, out parsed) ? parsed : new double?();
        }
    }
}
