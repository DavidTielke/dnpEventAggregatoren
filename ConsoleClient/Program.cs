using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper.Contract;
using DataClasses;
using DataStoring.Contract;
using DataStoring.Contract.Messages;
using EventAggregation.Contract;
using Mappings;
using Ninject;
using PersonManagement.Contract;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new Aggregator().Mappings);

            var bootstrapper = kernel.Get<IBootstrapper>();
            bootstrapper.ActivateAll();

            var eventMapper = kernel.Get<IEventMapper>();
            eventMapper.Map<EntityModifiedMessage>((aggregator, message) =>
            {
                Console.WriteLine(message.Status);
            });

            var manager = kernel.Get<IPersonManager>();
            var repository = kernel.Get<IRepository<Person>>();

            Console.WriteLine("### Erwachsene ###");
            manager.GetAllAdults().ToList().ForEach(p => Console.WriteLine($"{p.Id,-5} {p.Vorname,-15} {p.Nachname, -15} {p.IsMale,-5} {p.Age,-5}"));

            Console.WriteLine("### Kinder ###");
            manager.GetAllChildren().ToList().ForEach(p => Console.WriteLine($"{p.Id,-5} {p.Vorname,-15} {p.Nachname,-15} {p.IsMale, -5} {p.Age,-5}"));

            var entity = new Person
            {
                Vorname = "test",
                Age = 99
            };
            repository.Insert(entity);

            Console.ReadKey();
            bootstrapper.DeactivateAll();
        }
    }
}
