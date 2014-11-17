using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DTTest.Models;
using Kendo.Mvc.UI;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;

namespace DTTest.Controllers
{
    public class PersonController : Controller
    {
        private IPersonRepository repository;
        public PersonController(IPersonRepository personRepository)
        {
            repository = personRepository;
        }

        public ViewResult List()
        {
            IQueryable<Person> people = repository.Persons.AsQueryable<Person>();

            return View(people);
        }

        public ActionResult PopulateRace()
        {
            List<RaceSource> source = new List<RaceSource>();
            source.Add(new RaceSource() { RaceName=Enum.GetName(typeof(PersonRace), PersonRace.Angles), Value=(int)PersonRace.Angles });
            source.Add(new RaceSource() { RaceName=Enum.GetName(typeof(PersonRace), PersonRace.Asians), Value=(int)PersonRace.Asians });
            source.Add(new RaceSource() { RaceName = Enum.GetName(typeof(PersonRace), PersonRace.Jutes), Value = (int)PersonRace.Jutes });
            source.Add(new RaceSource() { RaceName = Enum.GetName(typeof(PersonRace), PersonRace.Saxons), Value = (int)PersonRace.Saxons });

            ViewData["races"] = source.OrderBy(x => x.Value);
            return View();
        }

        [GridAction]
        public ActionResult ShowPersons([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Person> people = repository.Persons.AsQueryable<Person>();

            string id = "1";

            if (!string.IsNullOrEmpty(id))
            {
                people = (from o in people
                          where (int)o.Race == int.Parse(id)
                          select o);
            }

            return View(new GridModel(people));
            //DataSourceResult result = people.ToDataSourceResult(request);
            //return View(new GridModel(people));
          
        }
    }
}
