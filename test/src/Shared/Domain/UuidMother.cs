using System;

namespace CodelyTv.Test.Shared.Domain
{
    public static class UuidMother
    {
        public static string Random()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
