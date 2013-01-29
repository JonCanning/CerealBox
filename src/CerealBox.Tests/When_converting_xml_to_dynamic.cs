using NUnit.Framework;
using System.Linq;

namespace CerealBox.Tests
{
    [TestFixture]
    class When_converting_xml_to_dynamic
    {
        dynamic dynamic;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var xml = @"
<animals>
  <badger>
    <name>Steve</name>
    <age>3</age>
  </badger>
  <dog class=""Pedigree"">
    <name>Rufus</name>
    <breed>labrador</breed>
  </dog>
  <dog>
    <name>Marty</name>
    <breed>whippet</breed>
  </dog>
    <cat name=""Matilda"" breed=""Persian"">
    <foods>biscuits</foods>
    <foods>meat</foods>
</cat>
</animals>";
            dynamic = xml.ToDynamic();
        }

        [Test]
        public void Then_badger_name_should_be_Steve()
        {
            string name = dynamic.animals.badger.name;
            Assert.AreEqual("Steve", name);
        }

        [Test]
        public void Then_badger_name_ToString_should_be_Steve()
        {
            Assert.AreEqual("Steve", dynamic.animals.badger.name.ToString());
        }

        [Test]
        public void Then_badger_age_should_be_3()
        {
            int age = dynamic.animals.badger.age;
            Assert.AreEqual(3, age);
        }

        [Test]
        public void Then_there_should_be_2_dogs()
        {
            dynamic[] dogs = dynamic.animals.dog;
            Assert.AreEqual(2, dogs.Length);
        }

        [Test]
        public void Then_the_first_dogs_name_should_be_Rufus()
        {
            string name = dynamic.animals.dog[0].name;
            Assert.AreEqual("Rufus", name);
        }

        [Test]
        public void Then_the_first_dogs_class_should_be_Pedigree()
        {
            string @class = dynamic.animals.dog[0].@class;
            Assert.AreEqual("Pedigree", @class);
        }

        [Test]
        public void Then_the_second_dog_should_be_a_whippet()
        {
            string breed = dynamic.animals.dog[1].breed;
            Assert.AreEqual("whippet", breed);
        }

        [Test]
        public void Then_the_second_dogs_class_should_be_a_Mongrel()
        {
            string @class = dynamic.animals.dog[1].@class;
            Assert.AreEqual(null, @class);
        }

        [Test]
        public void Then_the_cats_name_should_be_Matilda()
        {
            string name = dynamic.animals.cat.name;
            Assert.AreEqual("Matilda", name);
        }

        [Test]
        public void Then_the_cats_breed_should_be_Persian()
        {
            string breed = dynamic.animals.cat.breed;
            Assert.AreEqual("Persian", breed);
        }

        [Test]
        public void Then_cat_should_have_foods()
        {
            dynamic[] foods = dynamic.animals.cat.foods;
            var foodsString = foods.Select(x => x.ToString());
            Assert.AreEqual(new[] { "biscuits", "meat" }, foodsString);
        }
    }
}
