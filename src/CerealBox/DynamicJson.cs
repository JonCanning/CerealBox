using ServiceStack.Text;
using System.Dynamic;
using System.Linq;

namespace CerealBox
{
    public class DynamicJson : DynamicObject
    {
        readonly JsonObject jsonObject;

        public DynamicJson(string json) : this(JsonObject.Parse(json)) { }

        DynamicJson(string json, string name)
        {
            jsonObject = json.StartsWith("{") ? JsonObject.Parse(json) : new JsonObject { { name, json } };
        }

        DynamicJson(JsonObject jsonObject)
        {
            this.jsonObject = jsonObject;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (jsonObject.All(x => x.Key.ToDynamicCompatableString() != binder.Name))
                return true;

            var value = jsonObject.Single(x => x.Key.ToDynamicCompatableString() == binder.Name).Value;
            if (value.StartsWith("[{\"") && value.EndsWith("}]"))
                result = JsonArrayObjects.Parse(value).Select(x => new DynamicJson(x)).ToArray();
            else
                result = new DynamicJson(value, binder.Name);

            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = JsonSerializer.DeserializeFromString(jsonObject.First().Value, binder.ReturnType);
            return true;
        }

        public override string ToString()
        {
            return jsonObject.First().Value;
        }
    }
}