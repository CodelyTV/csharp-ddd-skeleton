namespace SharedTest.src.Domain
{
    using System;

    public static class UuidMother
    {
        public static string Random()
        {
            return Guid.NewGuid().ToString();
        }
    }
}