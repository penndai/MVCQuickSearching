using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
namespace DTTest.Infrastructure
{
    public class PersonControllerFactory : DefaultControllerFactory
    {
        private IKernel personKernal;
        private Nullable<DateTime> _previousTime = null;

        private static List<Models.Person> _data;

        public PersonControllerFactory(DateTime datetime)
        {
            personKernal = new StandardKernel();

            
             AddCustomBindings(datetime);
            
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)personKernal.Get(controllerType);
        }

        private void AddCustomBindings(DateTime datetime)
        {
            Mock<DTTest.Models.IPersonRepository> personMock = new Mock<Models.IPersonRepository>();


            personMock.Setup(m => m.Persons).Returns(() => FuncData(datetime));

            personKernal.Bind<Models.IPersonRepository>().ToConstant(personMock.Object);
        }

        private List<Models.Person> FuncData(DateTime datetime)
        {
            // mock list of person
            List<Models.Person> people = new  List<Models.Person>();
            Random personRdm = new Random();
            Random raceRdm = new Random();

            if (_previousTime == null)
            {
                PopulatePeople(people, personRdm, raceRdm);

                // add 1 year to each person age
                people.ForEach(x => x.Age++);

                _data = people;
                _previousTime = datetime;
            }
            else
            {
                // rebulid people list after half an hour
                if((_previousTime.Value.AddHours(0.5).CompareTo(datetime)<0))
                {
                    PopulatePeople(people, personRdm, raceRdm);

                    // add 1 year to each person age
                    people.ForEach(x => x.Age++);

                    _data = people;
                    _previousTime = datetime;
                }
            }

            return _data;
        }

        private static void PopulatePeople(List<Models.Person> people, Random personRdm, Random raceRdm)
        {
            for (int idx = 0; idx < 10000; idx++)
            {
                people.Add(
                    new Models.Person()
                    {
                        Id = idx + 1,
                        Name = string.Format("Person # {0}", idx.ToString()),
                        Age = personRdm.Next(1, 99),
                        Race = (Models.PersonRace)raceRdm.Next((int)Models.PersonRace.Angles, (int)Models.PersonRace.Asians + 1)
                    });
            }
        }
    }
}