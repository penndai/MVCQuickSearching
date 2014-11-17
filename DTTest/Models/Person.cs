using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTTest.Models
{
    public class Person
    {
        private double _Height;

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public PersonRace Race
        {
            get;
            set;
        }

        public double Height
        {
            get 
            {
                if (Age > 0)
                {
                    switch (Race)
                    {
                        case PersonRace.Angles:
                        case PersonRace.Saxons:
                            _Height = 1.5 + (Age * 0.45);
                            break;
                        case PersonRace.Asians:
                            _Height = (1.7 + ((Age + 2) * 0.23));
                            break;
                        case PersonRace.Jutes:
                            _Height = ((Age * 1.6) / 2);
                            break;
                        default:
                            _Height = 0;
                            break;
                    }
                }

                return _Height;
            }
        }

        public override string ToString()
        {
            return string.Format("{0},{1} years old, height {2}", Name, Age, Height);
        }
    }
}