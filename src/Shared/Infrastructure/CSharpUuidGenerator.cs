using System;
using CodelyTv.Shared.Domain;

namespace CodelyTv.Shared.Infrastructure
{
    public class CSharpUuidGenerator : IUuidGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
