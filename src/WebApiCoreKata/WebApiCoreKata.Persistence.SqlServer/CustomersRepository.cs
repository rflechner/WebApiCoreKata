using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCoreKata.Domain.Entities;

namespace WebApiCoreKata.Persistence.SqlServer
{
    public class CustomersRepository  : ICustomersRepository
    {
        public Task<IReadOnlyCollection<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Save(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
