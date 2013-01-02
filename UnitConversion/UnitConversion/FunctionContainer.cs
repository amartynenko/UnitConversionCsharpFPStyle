using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitConversion
{
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
                if (fact == null)
                    yield return "Can not calculate";
                else
                    yield return string.Format("{0} {1} = {2} {3}\r\n",
                        fact().Left.Amount, fact().Left.UnitName,
                        fact().Right.Amount, fact().Right.UnitName);
            }
        }

        public static IEnumerable<Func<dynamic>> CheckAndEvaluateEach(this IEnumerable<Func<dynamic>> input)
        {
            var evaluatedAlready = new List<Func<dynamic>>();

            foreach (var equation in input)
            {
                if (equation().Right.Amount == null)
                    yield return equation.TryEvaluate(evaluatedAlready);
                else
                {
                    evaluatedAlready.Add(equation);
                    yield return equation;
                }
            }
        }

        public static Func<dynamic> ConvertTo(this Func<dynamic> from, Func<dynamic> to)
        {
            if (from.LeftMatchesLeft(to))
                return from.ConvertRightPartToRight(to);

            if (from.LeftMatchesLeft(to.Invert()))
                return from.ConvertRightPartToRight(to.Invert());

            if (from.Invert().LeftMatchesLeft(to))
                return from.Invert().ConvertRightPartToRight(to);

            if (from.Invert().LeftMatchesLeft(to.Invert()))
                return from.Invert().ConvertRightPartToRight(to.Invert());

            return null;
        }

        private static Func<dynamic> ConvertRightPartToRight(this Func<dynamic> from, Func<dynamic> to)
        {
            return () => new
            {
                Left = new { Amount = from().Left.Amount * (to().Right.Amount / to().Left.Amount), to().Right.UnitName },
                from().Right 
            };
        }

        public static Func<dynamic> Invert(this Func<dynamic> fact)
        {
            return () => new { Left = fact().Right, Right = fact().Left };
        }

        public static Func<dynamic> Evaluate(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            if (unknown.FullMatch(fact))
                return unknown.CalculateFrom(fact);

            if (unknown.FullMatch(fact.Invert()))
                return unknown.CalculateFrom(fact.Invert());

            return null;
        }

        private static Func<dynamic> CalculateDerivedFrom(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return () => new
            {
                Left = new { Amount = unknown().Left.Amount * (fact().Right.Amount / fact().Left.Amount), fact().Right.UnitName },
                unknown().Right
            };
        }

        private static Func<dynamic> CalculateFrom(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return () => new
                       {
                           unknown().Left,
                           Right = new { Amount = unknown().Left.Amount * (fact().Right.Amount / fact().Left.Amount), unknown().Right.UnitName }
                       };
        }

        private static Func<dynamic> GetDerivedUnknown(this Func<dynamic> fact, Func<dynamic> unknown)
        {
            if (unknown.LeftMatchesLeft(fact))
                return unknown.CalculateDerivedFrom(fact);

            if (unknown.LeftMatchesLeft(fact.Invert()))
                return unknown.CalculateDerivedFrom(fact.Invert());

            return null;
        }

        private static bool LeftMatchesLeft(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return (unknown().Left.UnitName == fact().Left.UnitName);
        }

        private static bool FullMatch(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return (unknown().Left.UnitName == fact().Left.UnitName)
                && (unknown().Right.UnitName == fact().Right.UnitName);
        }

        public static Func<dynamic> TryEvaluate(this Func<dynamic> unknown, IEnumerable<Func<dynamic>> alreadyEvaluated)
        {
            foreach (var fact in alreadyEvaluated)
            {
                var evaluator = unknown.Evaluate(fact);

                if (evaluator != null)
                    return evaluator;

                var convertedUnknown = fact.GetDerivedUnknown(unknown);

                if (convertedUnknown != null)
                {
                    evaluator = convertedUnknown.TryEvaluate(alreadyEvaluated.Except(new[] { fact }));

                    if (evaluator != null)
                    {
                        return evaluator.ConvertTo(fact);
                    }
                }
            }

            return null;
        }
    }
}