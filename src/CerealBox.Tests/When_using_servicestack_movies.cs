using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using NUnit.Framework;

namespace CerealBox.Tests
{
    [TestFixture, Ignore]
    class When_using_servicestack_movies
    {
        WebClient client;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            client = new WebClient();
        }

        [Test]
        public void Then_first_film_using_json_should_be_Inception()
        {
            var response = client.DownloadString("http://www.servicestack.net/ServiceStack.MovieRest/movies?format=json");
            dynamic dynamic = response.ToDynamic();
            dynamic[] movies = dynamic.Movies;
            string title = movies[0].Title;
            Assert.AreEqual("Inception", title);
        }

        [Test]
        public void Then_first_film_using_xml_should_be_Inception()
        {
            var response = client.DownloadString("http://www.servicestack.net/ServiceStack.MovieRest/movies?format=xml");
            dynamic dynamic = response.ToDynamic();
            dynamic[] movies = dynamic.Movies;
            string title = movies[0].Title;
            Assert.AreEqual("Inception", title);
        }

        [Test]
        public void Then_movie_should_be_inserted_using_json()
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            dynamic movie = new ExpandoObject();
            movie.Title = "The Muppets Take Manhattan";
            movie.TagLine = "tagline";
            movie.Rating = 6.7;
            movie.ImdbId = "tt0087755";
            movie.Director = "Frank Oz";
            movie.ReleaseDate = new DateTime(1986, 2, 15);
            var json = ConvertDynamic.ToJson(movie);
            client.UploadString("http://www.servicestack.net/ServiceStack.MovieRest/movies", json);
            var response = client.DownloadString("http://www.servicestack.net/ServiceStack.MovieRest/movies?format=json");
            dynamic dynamic = response.ToDynamic();
            dynamic[] movies = dynamic.Movies;
            string title = movies.Last().Title;
            Assert.AreEqual("The Muppets Take Manhattan", title);
        }

        [Test]
        public void Then_movie_should_be_inserted_using_xml()
        {
            //client.Headers.Add(HttpRequestHeader.ContentType, "application/xml");
            dynamic movie = new ExpandoObject();
            movie.Title = "The Muppets Take Manhattan";
            movie.TagLine = "tagline";
            movie.Rating = 6.7;
            movie.ImdbId = "tt0087755";
            movie.Director = "Frank Oz";
            movie.ReleaseDate = new DateTime(1986, 2, 15);
            var xml = ConvertDynamic.ToXml(movie, "Movie");
            Console.WriteLine(xml);
            client.UploadString("http://www.servicestack.net/ServiceStack.MovieRest/movies", xml);
            var response = client.DownloadString("http://www.servicestack.net/ServiceStack.MovieRest/movies?format=xml");
            dynamic dynamic = response.ToDynamic();
            dynamic[] movies = dynamic.Movies;
            string title = movies.Last().Title;
            Assert.AreEqual("The Muppets Take Manhattan", title);
        }

    }
}