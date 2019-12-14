namespace CodelyTv.Shared.Infrastructure
{
    using System;
    using Domain;

    public class CSharpUuidGenerator : IUuidGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}