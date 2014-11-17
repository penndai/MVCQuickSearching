using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTest.Models;
namespace DTTest.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 20;
          private IPersonRepository repository;      

          public HomeController(IPersonRepository personRepository)
        {
            repository = personRepository;
        }

        public ViewResult Index(int race=1, int page = 1)
        {           
            List<RaceSource> source = new List<RaceSource>();
            source.Add(new RaceSource() { RaceName = Enum.GetName(typeof(PersonRace), PersonRace.Angles), Value = (int)PersonRace.Angles });
            source.Add(new RaceSource() { RaceName = Enum.GetName(typeof(PersonRace), PersonRace.Asians), Value = (int)PersonRace.Asians });
            source.Add(new RaceSource() { RaceName = Enum.GetName(typeof(PersonRace), PersonRace.Jutes), Value = (int)PersonRace.Jutes });
            source.Add(new RaceSource() { RaceName = Enum.GetName(typeof(PersonRace), PersonRace.Saxons), Value = (int)PersonRace.Saxons });

            PeopleViewModel viewModel = new PeopleViewModel();
            viewModel.RaceSources = source;

            List<Person> r = repository.Persons.Where(x => (int)x.Race == race && (x.Age %2)==0).ToList();
            viewModel.People = repository.Persons.Where(x=>(int)x.Race == race).OrderBy(x=>x.Age).Skip((page-1)*PageSize).Take(PageSize);
            r = repository.Persons.Where(x => (int)x.Race == race).ToList();
            viewModel.PagingInfo = new PagingInfo() { CurrentPage = page, ItemsPerPage = PageSize, TotalItems =r.Count() };
            viewModel.CurrentRace = race;
          
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult ReturnPeopleJson(int raceid)
        {
            PeopleJsonModel jsonmodel = new PeopleJsonModel();

            jsonmodel.AverageAge = (int)repository.Persons.Where(x => (int)x.Race == raceid).Average(x => x.Age);
            jsonmodel.MaxAge = repository.Persons.Where(x=>(int)x.Race == raceid).Max(x=>x.Age);
            jsonmodel.MinAge = repository.Persons.Where(x=>(int)x.Race == raceid).Min(x=>x.Age);
            jsonmodel.NumberOfPeople = repository.Persons.Count;
      
            var val = repository.Persons                
                .GroupBy(r => r.Race)
                .Select(g=>new Models.RaceCount{ Race=Enum.GetName(typeof(DTTest.Models.PersonRace), g.Key), Count=g.Count()});

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var v in val)
            {
                sb.AppendFormat("Race: {0}, Count: {1}\n",v.Race, v.Count);
            }

            jsonmodel.CountOfEachRace = sb.ToString();
            return Json(jsonmodel, JsonRequestBehavior.AllowGet);
        }
    }
}
