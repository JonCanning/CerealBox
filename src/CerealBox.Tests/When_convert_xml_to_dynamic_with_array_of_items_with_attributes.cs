using NUnit.Framework;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_convert_xml_to_dynamic_with_array_of_items_with_attributes
    {
        dynamic dynamic;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var xml = @"
<cats>
    <cat name=""Matilda"" breed=""Persian"" />
    <cat name=""Max"" breed=""Manx"" />
</cats>";
            dynamic = xml.ToDynamic();
        }

        [Test]
        public void Then_first_cat_should_be_called_Matilda()
        {
            string name = dynamic.cats.cat[0].name;
            Assert.AreEqual("Matilda", name);
        }

        [Test]
        public void Then_first_cat_breed_should_be_Persian()
        {
            string breed = dynamic.cats.cat[0].breed;
            Assert.AreEqual("Persian", breed);
        }
    }
}