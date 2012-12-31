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
                Left = new { Amount = 120.0, UnitName = "second" },
                Right = new { Amount = 2, UnitName = "minute" }
            };

            yield return () => new
            {
                Left = new { Amount = 1, UnitName = "minute" },
                Right = new { UnitName = "second" }
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
                Left = new { Amount = 1, UnitName = "minute" },
                Right = new { UnitName = "second" }
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
                Left = new { Amount = 60, UnitName = "second" },
                Right = new { UnitName = "minute" }
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
                Left = new { Amount = 60, UnitName = "second" },
                Right = new { UnitName = "minute" }
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
                if (equation().Right.Amount == null)
                {
                    Func<Func<dynamic>, Func<dynamic>, Func<dynamic>> findRelation = (unknown, item) =>
                        {
                            if (unknown().Left.UnitName == item().Left.UnitName)
                            {
                                return () => new
                                {
                                    Left = new { unknown().Left.Amount, unknown().Left.UnitName },
                                    Right = new { Amount = unknown().Left.Amount * (item().Right.Amount / item().Left.Amount), unknown().Right.UnitName }
                                };
                            }
                            if (unknown().Right.UnitName == item().Right.UnitName)
                            {
                                return () => new
                                {
                                    Left = new { unknown().Left.Amount, unknown().Left.UnitName },
                                    Right = new { Amount = unknown().Left.Amount / (item().Left.Amount / item().Right.Amount), unknown().Right.UnitName }
                                };
                            }
                            if (unknown().Left.UnitName == item().Right.UnitName)
                            {
                                return () => new
                                {
                                    Left = new { unknown().Left.Amount, unknown().Left.UnitName },
                                    Right = new { Amount = unknown().Left.Amount * (item().Left.Amount / item().Right.Amount), unknown().Right.UnitName }
                                };
                            }
                            if (unknown().Right().UnitName() == item().Left().UnitName())
                            {
                                return () => new
                                {
                                    Left = () => 
                                        new
                                        {
                                            Amount = unknown().Left().Amount, 
                                            UnitName = unknown().Left().UnitName
                                        },
                                    Right = () => {
                                        new
                                        {
                                            Amount = () => { unknown().Left().Amount() * (item().Left().Amount() / item().Right().Amount()) }, 
                                            UnitName = unknown().Right().UnitName
                                        }
                                    }
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
