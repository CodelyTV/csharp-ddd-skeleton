namespace CodelyTv.Test.Shared.Domain
{
    using Bogus;

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