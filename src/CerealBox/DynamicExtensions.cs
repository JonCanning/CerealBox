namespace CerealBox
{
    public static class DynamicExtensions
    {
        public static dynamic ToDynamic(this string input)
        {
            input = input.FlattenXml();
            if (input.StartsWith("<"))
                return new DynamicXml(input);
            return new DynamicJson(input);
        }
    }
}