Convert JSON or XML to dynamic:

var json = @"{""animals"":{""badger"":{""name"":""Steve Badger"",""age"":3},""dog"":[{""name"":""Rufus"",""breed"":""labrador""},{""name"":""Marty"",""breed"":""whippet""}],""cat"":{""name"":""Matilda""}}}";
dynamic dynamic = json.ToDynamic();

Retreive a property:
string name = dynamic.animals.badger.name;
Assert.AreEqual("Steve Badger", name);

Retrieve an array:
dynamic[] dogs = dynamic.animals.dog;
Assert.AreEqual(2, dogs.Length);

Convert dynamic to JSON or XML:

dynamic = new ExpandoObject();
dynamic.badger = new ExpandoObject();
dynamic.badger.name = "Steve";
var json = ConvertDynamic.ToJson(dynamic);
var xml = ConvertDynamic.ToXml(dynamic);

The tests should explain everything:

[https://github.com/JonCanning/CerealBox/tree/master/src/CerealBox.Tests][1]


  [1]: https://github.com/JonCanning/CerealBox/tree/master/src/CerealBox.Tests