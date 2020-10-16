using System;
using System.Collections.Generic;
using System.Linq;

namespace CodelyTv.Test.Shared.Domain
{
    public class Repeater<T>
    {
        private static IEnumerable<T> Repeat(Func<T> method, int quantity)
        {
            return Enumerable.Repeat(method(), quantity);
        }

        public static IEnumerable<T> RepeateLessThan(Func<T> method, int max)
        {
            return Repeat(method, IntegerMother.LessThan(max));
        }

        public static IEnumerable<T> Random(Func<T> method)
        {
            return Repeat(method, IntegerMother.LessThan(5));
        }
    }
}
