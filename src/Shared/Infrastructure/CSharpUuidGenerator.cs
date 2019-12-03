namespace CodelyTv.Shared.Infrastructure
{
    using System;

    public class CSharpUuidGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}