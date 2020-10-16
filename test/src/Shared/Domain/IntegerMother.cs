namespace CodelyTv.Test.Shared.Domain
{
    public static class IntegerMother
    {
        public static int Between(int min, int max = int.MaxValue)
        {
            return MotherCreator.Random().Number(min, max);
        }

        public static int LessThan(int max)
        {
            return Between(1, max);
        }

        public static int MoreThan(int min)
        {
            return Between(min);
        }

        public static int Random()
        {
            return Between(1);
        }
    }
}
