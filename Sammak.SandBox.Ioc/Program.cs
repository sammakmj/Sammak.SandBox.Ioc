using System;
using System.Linq;
using StructureMap;
namespace Sammak.SandBox.Ioc
{
    /// <summary>
    /// See http://stackoverflow.com/questions/6777671/setting-up-structure-map-in-a-c-sharp-console-application
    /// Updated for SM 4: http://ardalis.com/using-structuremap-4-in-a-console-app
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // this is required to wire up your types - it should always happen when you app is starting up
            var container = InitIoC();
            Console.WriteLine("StructureMap Initialized.");

            // this takes advantage of the WithDefaultConventions() feature of StructureMap and will result in the Message type coming back.
            IMessage myMessage = container.GetInstance<IMessage>();
            Console.WriteLine(myMessage.Hello("Steve"));

            // this uses the config.For<> syntax in the InitIoC() method
            var myPerson = container.GetInstance<Person>(); // getting a Person will get all dependencies specified in its constructor
            myPerson.Name = "Steve";
            myPerson.Age = 38;
            Console.WriteLine("Person:");
            Console.WriteLine(myPerson);

            Console.ReadLine();
        }

        private static Container InitIoC()
        {
            var container = new Container(cfg =>
            {
                // DefaultConventions
                cfg.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                });

                // specific implementation for certain interface
                cfg.For<IPersonFormatter>().Use<CapsPersonFormatter>();

                // config of same interface the last one wins.  
                // in this case even though the For<IPersonFormatter> 
                // implementaion of CapsPersonFormatter was defined earlier, 
                // the Use<SimplePersonFormatter> will win
                cfg.For<IPersonFormatter>().Use<SimplePersonFormatter>();
            });
            return container;
        }
    }

    public interface IMessage
    {
        string Hello(string person);
    }

    public class Message : IMessage
    {
        public string Hello(string person)
        {
            return String.Format("HELLO {0}!!!", person);
        }
    }

    public interface IPersonFormatter
    {
        string Format(Person person);
    }

    public class Person
    {
        private readonly IPersonFormatter _personFormatter;

        public Person(IPersonFormatter personFormatter)
        {
            _personFormatter = personFormatter;
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return _personFormatter.Format(this);
        }
    }

    public class SimplePersonFormatter : IPersonFormatter
    {
        public string Format(Person person)
        {
            return $"{person.Name}, {person.Age} years old.";
        }
    }

    public class CapsPersonFormatter : IPersonFormatter
    {
        public string Format(Person person)
        {
            return $"{person.Name}, {person.Age} years old.".ToUpper();
        }
    }
}
