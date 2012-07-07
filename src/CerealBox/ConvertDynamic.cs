using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Text;

namespace CerealBox
{
    public static class ConvertDynamic
    {
        static readonly IDictionary<string, string> AttributeDictionary = new Dictionary<string, string>();

        public static string ToXml(IDictionary<string, object> dictionary, string elementName)
        {
            foreach (var kvp in dictionary.Where(x => x.Value is IDictionary<string, object>))
            {
                var objects = (IDictionary<string, object>)kvp.Value;
                var attributeMarker = "_Attribute";
                var attributeKvps = objects.Where(x => x.Key.EndsWith(attributeMarker));
                attributeKvps.Select(x => new KeyValuePair<string, string>(kvp.Key, @" {0}=""{1}""".Fmt(x.Key.Replace(attributeMarker, string.Empty), x.Value))).ToList().ForEach(AttributeDictionary.Add);
                attributeKvps.Select(x => x.Key).ToList().ForEach(x => objects.Remove(x));
            }
            string attributes = null;
            if (AttributeDictionary.ContainsKey(elementName))
            {
                attributes = AttributeDictionary[elementName];
                AttributeDictionary.Remove(elementName);
            }
            var stringBuilder = new StringBuilder("<{0}{1}>".Fmt(elementName, attributes));
            foreach (var kvp in dictionary)
            {
                if (kvp.Value is IDictionary<string, object>)
                    stringBuilder.Append(ToXml((IDictionary<string, object>)kvp.Value, kvp.Key));
                else if (kvp.Value is dynamic[])
                {
                    foreach (var dyn in (dynamic[])kvp.Value)
                    {
                        stringBuilder.Append(ToXml(dyn, kvp.Key));
                    }
                }
                else if (kvp.Value.GetType().IsArray || (kvp.Value.GetType().IsGenericType && kvp.Value is IEnumerable))
                {
                    foreach (var value in (IEnumerable)kvp.Value)
                    {
                        stringBuilder.Append("<{0}>{1}</{0}>".Fmt(kvp.Key, value));
                    }
                }
                else
                    stringBuilder.Append("<{0}>{1}</{0}>".Fmt(kvp.Key, kvp.Value));
            }
            stringBuilder.Append("</{0}>".Fmt(elementName));
            return stringBuilder.ToString();
        }

        public static string ToJson(IDictionary<string, object> dictionary, string objectName = null)
        {
            if (!string.IsNullOrWhiteSpace(objectName))
                objectName = "{{\"{0}\":".Fmt(objectName);
            var stringBuilder = new StringBuilder(objectName);
            for (var i = 0; i < dictionary.Count; i++)
            {
                var kvp = dictionary.ElementAt(i);
                if (i == 0) stringBuilder.Append("{");
                if (kvp.Value is IDictionary<string, object>)
                {
                    stringBuilder.Append("\"{0}\":".Fmt(kvp.Key));
                    stringBuilder.Append(ToJson((IDictionary<string, object>)kvp.Value));
                }
                else if (kvp.Value is dynamic[])
                {
                    stringBuilder.Append("\"{0}\":[".Fmt(kvp.Key));
                    foreach (var dyn in (dynamic[])kvp.Value)
                    {
                        stringBuilder.Append(ToJson(dyn));
                    }
                    stringBuilder.Append("],");
                }
                else
                {
                    stringBuilder.Append("\"{0}\":{1}".Fmt(kvp.Key, JsonSerializer.SerializeToString(kvp.Value)));
                    stringBuilder.Append(i < dictionary.Count - 1 ? "," : "");
                }
                if (i == dictionary.Count - 1) stringBuilder.Append("}");
            }
            if (!string.IsNullOrWhiteSpace(objectName))
                stringBuilder.Append("}");
            return stringBuilder.ToString().Replace("}{", "},{").Replace("}\"", "},\"");
        }
    }
}