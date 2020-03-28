namespace CodelyTv.Apps.Mooc.Backend.Helper
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyHelper
    {
        private const string AssemblyName = "CodelyTv.Mooc";
        private static Assembly _instance;

        public static Assembly Instance()
        {
            if (_instance == null)
                _instance = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(x => x.FullName.Contains(AssemblyName));

            return _instance;
        }
    }
}