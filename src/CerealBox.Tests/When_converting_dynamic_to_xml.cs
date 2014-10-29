using NUnit.Framework;
using System.Dynamic;
using System.Xml.Linq;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_converting_dynamic_to_xml
    {
        string xml = @"
<animals location=""zoo"">
  <badger>
    <name>Steve</name>
    <age>3</age>
  </badger>
  <dog>
    <name>Rufus</name>
    <breed>labrador</breed>
  </dog>
  <dog>
    <name>Marty</name>
    <breed>whippet</breed>
  </dog>
  <cat breed=""Persian"" diet=""Low Sodium"">
<name>Matilda</name>
</cat>
</animals>";
        dynamic dynamic;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            dynamic = new ExpandoObject();
            dynamic.location_Attribute = "zoo";
            dynamic.badger = new ExpandoObject();
            dynamic.badger.name = "Steve";
            dynamic.badger.age = 3;
            dynamic dog1 = new ExpandoObject();
            dog1.name = "Rufus";
            dog1.breed = "labrador";
            dynamic dog2 = new ExpandoObject();
            dog2.name = "Marty";
            dog2.breed = "whippet";
            var dogs = new[] { dog1, dog2 };
            dynamic.dog = dogs;
            dynamic.cat = new ExpandoObject();
            dynamic.cat.name = "Matilda";
            dynamic.cat.breed_Attribute = "Persian";
            dynamic.cat.diet_Attribute = "Low Sodium";
        }

        [Test]
        public void Then_xml_should_be_valid()
        {
            var dynamicXml = ConvertDynamic.ToXml(dynamic, "animals");
            var originalXml = XElement.Parse(xml);
            var convertedXml = XElement.Parse(dynamicXml);
            Assert.AreEqual(originalXml.ToString(), convertedXml.ToString());
        }
    }
}