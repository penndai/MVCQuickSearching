using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTTest.Models
{
    public interface IPersonRepository
    {
        List<Person> Persons { get; }
    }
}
