using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CodelyTv.Shared.Infrastructure.Persistence.EntityFramework.EntityConfigurations
{
    public static class ConvertConfiguration
    {
        public static List<TObject> ObjectFromJson<TObject>(string json) where TObject : class
        {
            var jsonList = JsonConvert.DeserializeObject<List<string>>(json);

            var type = typeof(TObject);
            var ctor = type.GetConstructor(new[] {typeof(string)});

            return jsonList.Select(x => (TObject) ctor.Invoke(new object[] {x})).ToList();
        }

        public static string ObjectToJson<T>(List<T> objects)
        {
            return JsonConvert.SerializeObject(objects.Select(x => x.ToString()));
        }
    }
}
