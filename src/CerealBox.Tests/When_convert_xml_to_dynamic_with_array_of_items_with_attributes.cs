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
    <cat name=""Matilda"" />
    <cat name=""Max"" />
</cats>";
            dynamic = xml.ToDynamic();
        }

        [Test]
        public void Then_first_cat_should_be_called_Matilda()
        {
            string name = dynamic.cats.cat[0].name;
            Assert.AreEqual("Matilda", name);
        }
    }
}