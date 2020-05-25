namespace CodelyTv.Mooc.Helper
{
    using System.Reflection;
    using CodelyTv.Shared.Domain;

    public static class MoocAssemblyHelper
    {
        private const string AssemblyName = "CodelyTv.Mooc";
        private static Assembly _instance;

        public static Assembly Instance()
        {
            if (_instance == null)
                _instance = ReflectionHelper.GetAssemblyByName(AssemblyName);

            return _instance;
        }
    }
}