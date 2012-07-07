using System.Dynamic;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_converting_dynamic_to_xml_with_attribute
    {
        string xml = @"
<animals>
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
  <cat name=""Matilda""></cat>
</animals>";
        dynamic dynamic;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            dynamic = new ExpandoObject();
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
            dynamic.cat.name_Attribute = "Matilda";
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