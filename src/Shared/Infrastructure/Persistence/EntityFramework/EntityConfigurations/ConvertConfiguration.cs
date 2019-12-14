namespace CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;

    public static class ConvertConfiguration
    {
        public static List<TObject> ObjectFromJson<TObject>(string json) where TObject : class
        {
            Type type = typeof(TObject);
            ConstructorInfo ctor = type.GetConstructor(new[] {typeof(string)});

            var jsonList = JsonConvert.DeserializeObject<List<string>>(json);

            return jsonList.Select(x => (TObject) ctor.Invoke(new object[] {x})).ToList();
        }

        public static string ObjectToJson<T>(List<T> courseIds)
        {
            return JsonConvert.SerializeObject(courseIds.Select(x => x.ToString()));
        }
    }
}