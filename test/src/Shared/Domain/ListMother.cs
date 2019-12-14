namespace CodelyTv.Test.Shared.Domain
{
    using System;
    using System.Collections.Generic;

    public static class ListMother<T>
    {
        public static List<T> Create(int size, Func<T> creator)
        {
            List<T> list = new List<T>();

            for (int i = 0; i < size; i++)
            {
                list.Add(creator());
            }

            return list;
        }

        public static List<T> Random(Func<T> creator)
        {
            return Create(IntegerMother.Between(1, 10), creator);
        }

        public static List<T> One(T element)
        {
            return new List<T>() {element};
        }
    }
}