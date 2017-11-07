using System;
using System.Linq;
using Starcounter;

namespace HelloWorld2
{
    [Database]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Program
    {
        static void Main()
        {

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            Db.Transact(() =>
            {
                var person = Db.SQL<Person>("SELECT p FROM Person p").FirstOrDefault();
                if (person == null)
                {
                    new Person
                    {
                        FirstName = "John",
                        LastName = "Doe"
                    };
                }
            });


            Handle.GET("/HelloWorld2", () =>
            {
                Session.Ensure();
                var person = Db.SQL<Person>("SELECT p FROM Person p").FirstOrDefault();
                return new PersonJson { Data = person };
            });
        }
    }
}