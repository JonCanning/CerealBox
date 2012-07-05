using System;
using System.Dynamic;
using NUnit.Framework;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_converting_dynamic_to_json
    {
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
            dynamic.cat.name = "Matilda";
        }

        [Test]
        public void Then_json_should_be_valid()
        {
            var correctJson =
                "{\"badger\":{\"name\":\"Steve\",\"age\":3},\"dog\":[{\"name\":\"Rufus\",\"breed\":\"labrador\"},{\"name\":\"Marty\",\"breed\":\"whippet\"}],\"cat\":{\"name\":\"Matilda\"}}";
            var json = ConvertDynamic.ToJson(dynamic);
            Console.WriteLine(json);
            Assert.AreEqual(correctJson, json);
        }
    }
}