//using System;
//using System.Collections.Generic;
//using System.Linq;
//using NUnit.Framework;

//namespace UnitConversion
//{
//    public interface Input : IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> { }

//    public class Unit
//    {
//        private readonly double value;
//        private readonly string unitName;

//        private readonly Func<Unit> parent;

//        private readonly List<Func<Unit>> relatedUnits = new List<Func<Unit>>();

//        public Unit(Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>> equation, Func<Unit> parent)
//        {
//            this.parent = parent;
//            relatedUnits.Add(() => new Unit(equation().Item1.Item1.Value, equation.Item1.Item2, parent));

//            var unit = Solve(equation);
//            value = equation.Item2.Item1.Value;
//            unitName = equation.Item2.Item2;

//            var parentUnit = parent();
//            if (parentUnit != null)
//            {
//                relatedUnits.AddRange(parentUnit.RelatedUnits());
//            }
//        }

//        public Unit(Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>> equation)
//            : this(equation, null)
//        {
//        }

//        public Unit(Unit unit, Func<Unit> parent)
//        {
//            this.parent = parent;
//            relatedUnits.AddRange(unit.RelatedUnits());
//            relatedUnits.Add(parent);
//        }

//        public Unit(double value, string unitName, Func<Unit> parent)
//        {
//            this.value = value;
//            this.unitName = unitName;
//            this.parent = parent;
//        }

//        private IEnumerable<Func<Unit>> RelatedUnits()
//        {
//            return relatedUnits;
//        }

//        public Unit Solve(Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>> equation)
//        {
//            if (equation().Item2.Item1 != null)
//                return new Unit();

//            foreach (var relatedUnit in relatedUnits)
//            {
//                if (relatedUnit().unitName == equation().Item1.Item2)
//                {
//                    return new Unit(relatedUnit().value, relatedUnit().unitName, () => this);
//                }
//            }

//            return null;
//        }

//        public override string ToString()
//        {
//            return parent().value + " " + parent().unitName + " = " + value + " " + unitName;
//        }
//    }

//    [TestFixture]
//    public class Foo
//    {
//        [Test]
//        public void Run()
//        {
//            Func<Func<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>, Tuple<IEnumerable<Unit>, List<string>>, Tuple<IEnumerable<Unit>, List<string>>>, IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>>, Tuple<IEnumerable<Unit>, List<string>>, IEnumerable<string>> a = (foreachEquation, x, accumulator) =>
//                {
//                    foreach (var equation in x)
//                    {
//                        accumulator = foreachEquation(equation, accumulator);
//                    }
//                    return null;
//                };

//            Func<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>, Tuple<IEnumerable<Unit>, List<string>>, Tuple<IEnumerable<Unit>, List<string>>> c = (equation, accumulator) =>
//                {
//                    var tree = new List<Unit>();
//                    var list = new List<string>(accumulator.Item2.Select(result => result));

//                    foreach (var unit in accumulator.Item1.Select(unit => unit))
//                    {
//                        var result = unit.Solve(equation);
//                        if (result != null)
//                        {
//                            tree.Add(result);
//                            if (equation.Item2 == null)
//                                accumulator.Item2.Add(result.ToString());

//                            return new Tuple<IEnumerable<Unit>, List<string>>(tree, list);
//                        }

//                        accumulator.Item2.Add("Can not be resolved");
//                    }

//                    tree.Add(new Unit(equation, () => null));

//                    return new Tuple<IEnumerable<Unit>, List<string>>(tree, list);
//                };

//            var whatever = a(c, TheSimplest(), new Tuple<IEnumerable<Unit>, List<string>>(new List<Unit>(), new List<string>()));
//            Console.WriteLine();
//        }

//        //private List<string> Calculate<Equation, List<string>>()
//        //{

//        //}

//        private IEnumerable<Func<Tuple<Tuple<double?, string>, Tuple<double?, string>>>> TheSimplest()
//        {
//            //7200.0 second = 2 hour
//            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
//                new Tuple<double?, string>(7200.0, "second"),
//                new Tuple<double?, string>(2, "hour"));
//            //1 minute = 60 second
//            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
//                new Tuple<double?, string>(1, "minute"),
//                new Tuple<double?, string>(60, "second"));
//            //5.6 second = ? hour
//            yield return () => new Tuple<Tuple<double?, string>, Tuple<double?, string>>(
//                new Tuple<double?, string>(5.6, "seconds"),
//                new Tuple<double?, string>(null, "hour"));
//        }

//        //private IEnumerable<Equation> DefaultIn()
//        //{
//        //    //7200.0 second = 2 hour
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(7200.0, "second"),
//        //        new Tuple<double?, string>(2, "hour"));
//        //    //10.0 glob = 1 decaglob
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(10.0, "glob"),
//        //        new Tuple<double?, string>(1, "decaglob"));
//        //    //1 day = 24.0 hour
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(1, "day"),
//        //        new Tuple<double?, string>(24.0, "hour"));
//        //    //1 minute = 60 second
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(1, "minute"),
//        //        new Tuple<double?, string>(60, "second"));
//        //    //1 glob = 10 centiglob
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(1, "glob"),
//        //        new Tuple<double?, string>(10, "centiglob"));
//        //    //1 day = 24 hour
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(1, "day"),
//        //        new Tuple<double?, string>(24, "hour"));
//        //    //1 year = 365.25 day
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(1, "year"),
//        //        new Tuple<double?, string>(365.25, "day"));
//        //    //50 centiglob = ? decaglob
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(50, "centiglob"),
//        //        new Tuple<double?, string>(null, "decaglob"));
//        //    //5.6 second = ? hour
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(5.6, "seconds"),
//        //        new Tuple<double?, string>(null, "hour"));
//        //    //3 millisecond = ? hour
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(3, "millisecond"),
//        //        new Tuple<double?, string>(null, "hour"));
//        //    //5.6 second = ? day
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(5.6, "second"),
//        //        new Tuple<double?, string>(null, "day"));
//        //    //1 day = ? glob
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(1, "day"),
//        //        new Tuple<double?, string>(null, "glob"));
//        //    //1 hour = ? second
//        //    yield return new Equation(
//        //        new Tuple<double?, string>(1, "hour"),
//        //        new Tuple<double?, string>(null, "second"));
//        //    //1 year = ? second
//        //    yield return new Equation(
//        //       new Tuple<double?, string>(1, "year"),
//        //       new Tuple<double?, string>(null, "second"));
//        //}
//    }

//    internal class Bar
//    {

//    }
//}
