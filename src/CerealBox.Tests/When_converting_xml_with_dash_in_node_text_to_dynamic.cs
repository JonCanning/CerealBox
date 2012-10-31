using NUnit.Framework;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_converting_xml_with_dash_in_node_text_to_dynamic
    {
        dynamic dynamic;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var xml = @"
<animals>
  <badger>
    <name>Steve-the-badger</name>
    <age>3</age>
  </badger>
</animals>";
            dynamic = xml.ToDynamic();
        }


        [Test]
        public void Then_badger_name_should_be_Steve_the_badger()
        {
            string name = dynamic.animals.badger.name;
            Assert.AreEqual("Steve-the-badger", name);
        }

        [Test]
        public void Then_badger_name_ToString_should_be_Steve_the_badger()
        {
            Assert.AreEqual("Steve-the-badger", dynamic.animals.badger.name.ToString());
        }
    }
}