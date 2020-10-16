using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CodelyTv.Shared.Domain
{
    public static class ReflectionHelper
    {
        public static Assembly GetAssemblyByName(string name)
        {
            if (name == null) return null;

            name = name.ToUpper(CultureInfo.InvariantCulture);
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.ToUpper(CultureInfo.InvariantCulture)
                    .Contains(name, StringComparison.InvariantCulture));
        }

        public static Type GetType(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .FirstOrDefault(type => type.Name.Equals(name, StringComparison.InvariantCulture));
        }

        public static Type GetType(string assemblyName, string name)
        {
            if (string.IsNullOrEmpty(assemblyName) && string.IsNullOrEmpty(name)) return null;

            var assembly = GetAssemblyByName(assemblyName);

            return GetType(assembly, name);
        }

        public static Type GetType(Assembly assembly, string name)
        {
            if (assembly == null) return null;

            return assembly.GetTypes()
                .FirstOrDefault(type => type.Name.Equals(name, StringComparison.InvariantCulture));
        }
    }
}
