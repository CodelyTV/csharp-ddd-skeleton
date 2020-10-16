using System.Collections.Concurrent;
using System.Reflection;

namespace CodelyTv.Shared.Helpers
{
    public static class AssemblyHelper
    {
        private static readonly ConcurrentDictionary<string, Assembly> _assemblies =
            new ConcurrentDictionary<string, Assembly>();

        public static Assembly GetInstance(string key)
        {
            return _assemblies.GetOrAdd(key, Assembly.Load(key));
        }
    }

    public static class Assemblies
    {
        public const string Mooc = "CodelyTv.Mooc";
        public const string Backoffice = "CodelyTv.Backoffice";
    }
}
