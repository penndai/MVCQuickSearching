using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTTest.Models
{
    public class PeopleJsonModel
    {
        public int NumberOfPeople
        {
            get;
            set;
        }

        public int AverageAge
        {
            get;
            set;
        }

        public int MaxAge
        {
            get;
            set;
        }

        public int MinAge
        {
            get;
            set;
        }

        public string CountOfEachRace
        {
            get;
            set;
        }
    }
}