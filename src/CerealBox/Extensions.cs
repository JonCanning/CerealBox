using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CerealBox
{
    public static class StringExtensions
    {
        public static string FlattenXml(this string input)
        {
            input = Regex.Replace(input, "(\r|\n)", string.Empty);
            input = Regex.Replace(input, @">\s*<", "><").Trim();
            return input;
        }

        public static string ToDynamicCompatableString(this string xml)
        {
            return Regex.Replace(xml, @"(?<=.)-(?=.)", string.Empty, RegexOptions.Multiline).Trim();
        }
    }

    public static class XElementExtensions
    {
        public static string DynamicCompatableName(this XElement input)
        {
            return input.Name.DynamicCompatableName();
        }
    }

    public static class XNameExtensions
    {
        public static string DynamicCompatableName(this XName input)
        {
            return input.LocalName.ToDynamicCompatableString();
        }

        
    }
}
