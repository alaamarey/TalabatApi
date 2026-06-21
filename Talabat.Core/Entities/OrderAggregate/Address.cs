using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class Address
    {

        public Address()
        { }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
        public Address(string firstName, string lastName, string country, string city, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;
            Street = street;
        }

    }
}
