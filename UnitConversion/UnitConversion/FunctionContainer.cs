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

        public static Func<dynamic> Evaluate(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            if (unknown.MatchesFact(fact))
                return () => unknown.CalculateFrom(fact);

            if (unknown.MatchesInvertedFact(fact))
                return () => unknown.CalculateFromInverted(fact);

            return () => null;
        }

        private static dynamic CalculateFromInverted(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return new
                       {
                           unknown().Left,
                           Right = new { Amount = unknown().Left.Amount * (fact().Left.Amount / fact().Right.Amount), unknown().Right.UnitName }
                       };
        }

        private static dynamic CalculateFrom(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return new
                       {
                           unknown().Left,
                           Right = new { Amount = unknown().Left.Amount * (fact().Right.Amount / fact().Left.Amount), unknown().Right.UnitName }
                       };
        }

        private static bool MatchesFact(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return (unknown().Left.UnitName == fact().Left.UnitName) 
                && (unknown().Right.UnitName == fact().Right.UnitName);
        }

        private static bool MatchesInvertedFact(this Func<dynamic> unknown, Func<dynamic> fact)
        {
            return (unknown().Left.UnitName == fact().Right.UnitName) 
                && (unknown().Right.UnitName == fact().Left.UnitName);
        }

        public static Func<dynamic> TryEvaluate(this Func<dynamic> unknown, List<Func<dynamic>> alreadyEvaluated)
        {
            foreach (var fact in alreadyEvaluated)
            {
                var evaluator = unknown.Evaluate(fact);

                if (evaluator() != null)
                    return evaluator;
            }

            return () => null;
        }
    }
}