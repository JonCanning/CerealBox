using System.Text.RegularExpressions;
using NUnit.Framework;
using System.Linq;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_converting_xml_with_dash_in_proprty_name_to_dynamic
    {
        dynamic dynamic;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var xml = @"
<animals>
  <crazy-mad-badger>
    <name>Steve</name>
    <age>3</age>
  </crazy-mad-badger>
</animals>";
            dynamic = xml.ToDynamic();
        }

       
        [Test]
        public void Then_crazymadbadger_name_should_be_Steve()
        {
            string name = dynamic.animals.crazymadbadger.name;
            Assert.AreEqual("Steve", name);
        }

        [Test]
        public void Then_crazymadbadger_name_ToString_should_be_Steve()
        {
            Assert.AreEqual("Steve", dynamic.animals.crazymadbadger.name.ToString());
        }

        [Test]
        public void Then_crazymadbadger_age_should_be_3()
        {
            int age = dynamic.animals.crazymadbadger.age;
            Assert.AreEqual(3, age);
        }
    }
    
}
