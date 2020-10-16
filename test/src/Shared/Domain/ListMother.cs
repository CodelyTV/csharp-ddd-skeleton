using System;
using System.Collections.Generic;

namespace CodelyTv.Test.Shared.Domain
{
    public static class ListMother<T>
    {
        public static List<T> Create(int size, Func<T> creator)
        {
            var list = new List<T>();

            for (var i = 0; i < size; i++) list.Add(creator());

            return list;
        }

        public static List<T> Random(Func<T> creator)
        {
            return Create(IntegerMother.Between(1, 10), creator);
        }

        public static List<T> One(T element)
        {
            return new List<T> {element};
        }
    }
}
