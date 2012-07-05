using System.Dynamic;
using System.Linq;
using ServiceStack.Text;

namespace CerealBox
{
    public class DynamicJson : DynamicObject
    {
        readonly JsonObject jsonObject;

        public DynamicJson(string json) : this(JsonObject.Parse(json)) { }

        DynamicJson(string json, string name)
        {
            jsonObject = JsonObject.Parse(json).All(x => x.Value == null) ? new JsonObject { { name, json } } : JsonObject.Parse(json);
        }

        DynamicJson(JsonObject jsonObject)
        {
            this.jsonObject = jsonObject;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (jsonObject.All(x => x.Key != binder.Name))
                return false;

            var value = jsonObject.Get(binder.Name);
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