using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UnitConversion
{
    [TestFixture]
    public class TheSimplestFixture
    {
        [Test]
        public void TheSimplest1()
        {
            var result = TheSimplestInput1().Print(new List<Func<dynamic>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        [Test]
        public void TheSimplest2()
        {
            var result = TheSimplestInput2().Print(new List<Func<dynamic>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        [Test]
        public void TheSimplest3()
        {
            var result = TheSimplestInput3().Print(new List<Func<dynamic>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        [Test]
        public void TheSimplest4()
        {
            var result = TheSimplestInput4().Print(new List<Func<dynamic>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        #region the simplest input
        private IEnumerable<Func<dynamic>> TheSimplestInput1()
        {
            yield return () => new
            {
                Item1 = new Tuple<double?, string>(120.0, "second"),
                Item2 = new Tuple<double?, string>(2, "minute")
            };

            yield return () => new
            {
                Item1 = new Tuple<double?, string>(1, "minute"),
                Item2 = new Tuple<double?, string>(null, "second")
            };
        }

        private IEnumerable<Func<dynamic>> TheSimplestInput2()
        {
            yield return () => new
            {
                Item1 = new Tuple<double?, string>(2, "minute"),
                Item2 = new Tuple<double?, string>(120.0, "second")
            };

            yield return () => new
            {
                Item1 = new Tuple<double?, string>(1, "minute"),
                Item2 = new Tuple<double?, string>(null, "second")
            };
        }

        private IEnumerable<Func<dynamic>> TheSimplestInput3()
        {
            yield return () => new
            {
                Item1 = new Tuple<double?, string>(120.0, "second"),
                Item2 = new Tuple<double?, string>(2, "minute")
            };

            yield return () => new
            {
                Item1 = new Tuple<double?, string>(60, "second"),
                Item2 = new Tuple<double?, string>(null, "minute")
            };
        }

        private IEnumerable<Func<dynamic>> TheSimplestInput4()
        {
            yield return () => new
            {
                Item1 = new Tuple<double?, string>(2, "minute"),
                Item2 = new Tuple<double?, string>(120.0, "second")
            };

            yield return () => new
            {
                Item1 = new Tuple<double?, string>(60, "second"),
                Item2 = new Tuple<double?, string>(null, "minute")
            };
        }
    }
        #endregion

    static class SomeExtensions
    {
        public static IEnumerable<Func<dynamic>> Print(this IEnumerable<Func<dynamic>> input, List<Func<dynamic>> accumulator)
        {
            foreach (var equation in input)
            {
                if (equation().Item2.Item1 == null)
                {
                    Func<Func<dynamic>, Func<dynamic>, Func<dynamic>> findRelation = (unknown, item) =>
                        {
                            if (unknown().Item1.Item2 == item().Item1.Item2)
                            {
                                return () => new
                                {
                                    Item1 = new Tuple<double?, string>(unknown().Item1.Item1, unknown().Item1.Item2),
                                    Item2 = new Tuple<double?, string>(unknown().Item1.Item1 * (item().Item2.Item1 / item().Item1.Item1), unknown().Item2.Item2)
                                };
                            }
                            if (unknown().Item2.Item2 == item().Item2.Item2)
                            {
                                return () => new
                                {
                                    Item1 = new Tuple<double?, string>(unknown().Item1.Item1, unknown().Item1.Item2),
                                    Item2 = new Tuple<double?, string>(unknown().Item1.Item1 / (item().Item1.Item1 / item().Item2.Item1), unknown().Item2.Item2)
                                };
                            }
                            if (unknown().Item1.Item2 == item().Item2.Item2)
                            {
                                return () => new
                                {
                                    Item1 = new Tuple<double?, string>(unknown().Item1.Item1, unknown().Item1.Item2),
                                    Item2 = new Tuple<double?, string>(unknown().Item1.Item1 * (item().Item1.Item1 / item().Item2.Item1), unknown().Item2.Item2)
                                };
                            }
                            if (unknown().Item2.Item2 == item().Item1.Item2)
                            {
                                return () => new
                                {
                                    Item1 = new Tuple<double?, string>(5.6, unknown().Item1.Item2),
                                    Item2 = new Tuple<double?, string>(null, item().Item2.Item2)
                                };
                            }
                            return () => null;
                        };

                    foreach (var resolvedEquation in accumulator)
                    {
                        var newRelation = findRelation(equation, resolvedEquation);
                        if (newRelation != null)
                        {
                            accumulator.Add(newRelation);
                            break;
                        }
                    }
                }
                else
                {
                    accumulator.Add(equation);
                }
            }

            foreach (var result in accumulator)
            {
                yield return result;
            }
        }
    }
}
