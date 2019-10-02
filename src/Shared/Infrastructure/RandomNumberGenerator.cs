namespace src.Shared.Infrastructure
{
    using System;

    public sealed class RandomNumberGenerator
    {
        private readonly Random _random = new Random();

        public int Generate()
        {
            return _random.Next(1, 5);
        }
    }
}