using NUnit.Framework;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_converting_json_with_dash_in_property_name_to_dynamic
    {
        dynamic dynamic;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var json = @"{""animals"":{""crazy-mad-badger"":{""name"":""Steve Badger"",""age"":3}}}";
            dynamic = json.ToDynamic();
        }

        [Test]
        public void Then_crazymadbadger_name_should_be_Steve()
        {
            string name = dynamic.animals.crazymadbadger.name;
            Assert.AreEqual("Steve Badger", name);
        }

        [Test]
        public void Then_crazymadbadger_name_ToString_should_be_Steve()
        {
            Assert.AreEqual("Steve Badger", dynamic.animals.crazymadbadger.name.ToString());
        }

        [Test]
        public void Then_crazymadbadger_age_should_be_3()
        {
            int age = dynamic.animals.crazymadbadger.age;
            Assert.AreEqual(3, age);
        }

    }
}