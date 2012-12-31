using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UnitConversion
{
    public interface Input : IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> { }

    public class Another
    {
        //private readonly double value;
        //private readonly string unitName;

        //private readonly Func<Unit> parent;

        //private readonly List<Func<Unit>> relatedUnits = new List<Func<Unit>>();

        //public Unit(Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>> equation, Func<Unit> parent)
        //{
        //    this.parent = parent;
        //    relatedUnits.Add(() => new Unit(equation().Item1.Item1.Value, equation.Item1.Item2, parent));

        //    var unit = Solve(equation);
        //    value = equation.Item2.Item1.Value;
        //    unitName = equation.Item2.Item2;

        //    var parentUnit = parent();
        //    if (parentUnit != null)
        //    {
        //        relatedUnits.AddRange(parentUnit.RelatedUnits());
        //    }
        //}

        //public Unit(Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>> equation)
        //    : this(equation, null)
        //{
        //}

        //public Unit(Unit unit, Func<Unit> parent)
        //{
        //    this.parent = parent;
        //    relatedUnits.AddRange(unit.RelatedUnits());
        //    relatedUnits.Add(parent);
        //}

        //public Unit(double value, string unitName, Func<Unit> parent)
        //{
        //    this.value = value;
        //    this.unitName = unitName;
        //    this.parent = parent;
        //}

        //private IEnumerable<Func<Unit>> RelatedUnits()
        //{
        //    return relatedUnits;
        //}

        //public Unit Solve(Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>> equation)
        //{
        //    if (equation().Item2.Item1 != null)
        //        return new Unit();

        //    foreach (var relatedUnit in relatedUnits)
        //    {
        //        if (relatedUnit().unitName == equation().Item1.Item2)
        //        {
        //            return new Unit(relatedUnit().value, relatedUnit().unitName, () => this);
        //        }
        //    }

        //    return null;
        //}

        //public override string ToString()
        //{
        //    return parent().value + " " + parent().unitName + " = " + value + " " + unitName;
        //}
    }

    [TestFixture]
    public class Foo
    {
        //[Test]
        //public void Run()
        //{
        //    Func<Func<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>, Tuple<IEnumerable<Unit>, List<string>>, Tuple<IEnumerable<Unit>, List<string>>>, IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>>, Tuple<IEnumerable<Unit>, List<string>>, IEnumerable<string>> a = (foreachEquation, x, accumulator) =>
        //    {
        //        foreach (var equation in x)
        //        {
        //            accumulator = foreachEquation(equation, accumulator);
        //        }
        //        return null;
        //    };

        //    Func<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>, Tuple<IEnumerable<Unit>, List<string>>, Tuple<IEnumerable<Unit>, List<string>>> c = (equation, accumulator) =>
        //    {
        //        var tree = new List<Unit>();
        //        var list = new List<string>(accumulator.Item2.Select(result => result));

        //        foreach (var unit in accumulator.Item1.Select(unit => unit))
        //        {
        //            var result = unit.Solve(equation);
        //            if (result != null)
        //            {
        //                tree.Add(result);
        //                if (equation.Item2 == null)
        //                    accumulator.Item2.Add(result.ToString());

        //                return new Tuple<IEnumerable<Unit>, List<string>>(tree, list);
        //            }

        //            accumulator.Item2.Add("Can not be resolved");
        //        }

        //        tree.Add(new Unit(equation, () => null));

        //        return new Tuple<IEnumerable<Unit>, List<string>>(tree, list);
        //    };

        //    var whatever = a(c, TheSimplest(), new Tuple<IEnumerable<Unit>, List<string>>(new List<Unit>(), new List<string>()));
        //    Console.WriteLine();
        //}

        ////private List<string> Calculate<Equation, List<string>>()
        ////{

        ////}



        [Test]
        public void TheSimplest1()
        {
            var result = TheSimplestInput1().Print(new List<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        [Test]
        public void TheSimplest2()
        {
            var result = TheSimplestInput2().Print(new List<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        [Test]
        public void TheSimplest3()
        {
            var result = TheSimplestInput3().Print(new List<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        [Test]
        public void TheSimplest4()
        {
            var result = TheSimplestInput4().Print(new List<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>>()).Where(x => x != null);
            foreach (var s in result)
            {
                Console.WriteLine(s().ToString());
            }
            Console.ReadLine();
        }

        private IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> TheSimplestInput1()
        {
            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(120.0, "second"),
                new Tuple<double?, string>(2, "minute"));
            
            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(1, "minute"),
                new Tuple<double?, string>(null, "second"));
        }

        private IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> TheSimplestInput2()
        {
            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(2, "minute"),
                new Tuple<double?, string>(120.0, "second"));
            
            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(1, "minute"),
                new Tuple<double?, string>(null, "second"));
        }

        private IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> TheSimplestInput3()
        {
            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(120.0, "second"),
                new Tuple<double?, string>(2, "minute"));

            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(60, "second"),
                new Tuple<double?, string>(null, "minute"));
        }

        private IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> TheSimplestInput4()
        {
            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(2, "minute"),
                new Tuple<double?, string>(120.0, "second"));

            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                new Tuple<double?, string>(60, "second"),
                new Tuple<double?, string>(null, "minute"));
        }
    }

    static class SomeExtensions
    {
        public static IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> Print(this IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> input, List<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> accumulator)
        {
            foreach (var equation in input)
            {
                if (equation().Item2.Item1 == null)
                {
                    Func<
                        Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>,
                        Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>,
                        Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>>
                    findRelation = (unknown, item) =>
                        {
                            ////120.0 second = 2 minute
                            //yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                            //    new Tuple<double?, string>(2, "minute"),
                            //    new Tuple<double?, string>(120.0, "second"));
                            ////1 minute = ? second
                            //yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                            //    new Tuple<double?, string>(1, "minute"),
                            //    new Tuple<double?, string>(null, "second"));
                            if (unknown().Item1.Item2 == item().Item1.Item2)
                            {
                                return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                                    new Tuple<double?, string>(unknown().Item1.Item1, unknown().Item1.Item2),
                                    new Tuple<double?, string>(unknown().Item1.Item1 * (item().Item2.Item1 / item().Item1.Item1), unknown().Item2.Item2));
                            }
                            if (unknown().Item2.Item2 == item().Item2.Item2)
                            {
                                return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                                    new Tuple<double?, string>(unknown().Item1.Item1, unknown().Item1.Item2),
                                    new Tuple<double?, string>(unknown().Item1.Item1 / (item().Item1.Item1 / item().Item2.Item1), unknown().Item2.Item2));
                            }
                            //
                            ////120.0 second = 2 minute
                            //yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                            //    new Tuple<double?, string>(120.0, "second"),
                            //    new Tuple<double?, string>(2, "minute"));
                            ////1 minute = ? second
                            //yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                            //    new Tuple<double?, string>(1, "minute"),
                            //    new Tuple<double?, string>(null, "second"));
                            if (unknown().Item1.Item2 == item().Item2.Item2)
                            {
                                return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                                    new Tuple<double?, string>(unknown().Item1.Item1, unknown().Item1.Item2),
                                    new Tuple<double?, string>(unknown().Item1.Item1 * (item().Item1.Item1 / item().Item2.Item1), unknown().Item2.Item2));
                            }
                            if (unknown().Item2.Item2 == item().Item1.Item2)
                            {
                                return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
                                    new Tuple<double?, string>(5.6, unknown().Item1.Item2),
                                    new Tuple<double?, string>(null, item().Item2.Item2));
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



    internal class Bar
    {

    }
}
