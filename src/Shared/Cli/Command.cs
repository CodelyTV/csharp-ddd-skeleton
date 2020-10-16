using System;

namespace CodelyTv.Shared.Cli
{
    public abstract class Command
    {
        private const string AnsiReset = "\u001B[0m";
        private const string AnsiRed = "\u001B[31m";
        private const string AnsiCyan = "\u001B[36m";
        private const string AnsiGreen = "\u001B[32m";

        protected void Log(string text)
        {
            Console.WriteLine($"{AnsiGreen}{text}{AnsiReset}");
        }

        protected void Info(string text)
        {
            Console.WriteLine($"{AnsiCyan}{text}{AnsiReset}");
        }

        protected void Error(string text)
        {
            Console.WriteLine($"{AnsiRed}{text}{AnsiReset}");
        }

        public abstract void Execute(string[] args);
    }
}
