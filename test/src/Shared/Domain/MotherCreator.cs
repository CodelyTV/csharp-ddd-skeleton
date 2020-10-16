using Bogus;

namespace CodelyTv.Test.Shared.Domain
{
    public static class MotherCreator
    {
        public static Faker<T> Random<T>() where T : class
        {
            return new Faker<T>();
        }

        public static Randomizer Random()
        {
            return new Faker().Random;
        }
    }
}
