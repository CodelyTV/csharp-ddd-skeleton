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
            var jsonList = JsonConvert.DeserializeObject<List<string>>(json);

            Type type = typeof(TObject);
            ConstructorInfo ctor = type.GetConstructor(new[] {typeof(string)});

            return jsonList.Select(x => (TObject) ctor.Invoke(new object[] {x})).ToList();
        }

        public static string ObjectToJson<T>(List<T> objects)
        {
            return JsonConvert.SerializeObject(objects.Select(x => x.ToString()));
        }
    }
}