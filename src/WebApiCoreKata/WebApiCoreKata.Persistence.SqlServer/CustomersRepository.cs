using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCoreKata.Domain.Entities;
using WebApiCoreKata.Persistence.SqlServer.Database;
using WebApiCoreKata.Persistence.SqlServer.Dto;

namespace WebApiCoreKata.Persistence.SqlServer
{
    public class CustomersRepository  : ICustomersRepository
    {
        private readonly KataContext context;

        public CustomersRepository(KataContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyCollection<Customer>> GetAll(CancellationToken cancellationToken = default)
        {
            var customers = await context.Customers.ToListAsync(cancellationToken).ConfigureAwait(false);

            return customers.Select(c => c.ToDomain()).ToList().AsReadOnly();
        }

        public Task Save(Customer customer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
