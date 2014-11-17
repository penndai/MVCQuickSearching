using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTTest.Models
{
    public class PeopleViewModel
    {       
        public IEnumerable<RaceSource> RaceSources { get; set; }
        public IEnumerable<Person> People { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurrentRace { get; set; }
    }
}