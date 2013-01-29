using System.Xml.Linq;

namespace CerealBox
{
    public static class XNameExtensions
    {
        public static string DynamicCompatableName(this XName input)
        {
            return input.LocalName.ToDynamicCompatableString();
        }


    }
}
