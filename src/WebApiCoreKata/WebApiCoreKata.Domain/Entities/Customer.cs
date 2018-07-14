using System;
using WebApiCoreKata.Domain.ValueObjects;

namespace WebApiCoreKata.Domain.Entities
{
    public class Customer
    {
        public Customer(CustomerId id, string firstName, string lastName, DateTime birth)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birth = birth;
        }

        public CustomerId Id { get;  }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime Birth { get; }
    }
}
