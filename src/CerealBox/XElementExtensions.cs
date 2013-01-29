using System.Xml.Linq;

namespace CerealBox
{
    public static class XElementExtensions
    {
        public static string DynamicCompatableName(this XElement input)
        {
            return input.Name.DynamicCompatableName();
        }
    }
}