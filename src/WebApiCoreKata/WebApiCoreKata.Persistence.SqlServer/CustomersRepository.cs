using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCoreKata.Domain.Entities;
using WebApiCoreKata.Domain.ValueObjects;
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

        public Task<bool> Exists(CustomerId id, CancellationToken cancellationToken = default) 
            => context.Customers.AnyAsync(c => c.Id == id.Value, cancellationToken: cancellationToken);

        public async Task<Customer> Get(CustomerId id, CancellationToken cancellationToken = default)
        {
            var customer = await context.Customers
                .FirstOrDefaultAsync(c => c.Id == id.Value, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return customer?.ToDomain();
        }

        public async Task<CustomerId> Save(Customer customer, CancellationToken cancellationToken = default)
        {
            if (customer.Id.IsEmpty)
                return await Insert(customer, cancellationToken);

            var dto = await context.Customers
                .FirstOrDefaultAsync(c => c.Id == customer.Id.Value, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            
            if (dto == null)
                return await Insert(customer, cancellationToken);
            
            dto.Birth = customer.Birth;
            dto.FirstName = customer.FirstName;
            dto.LastName = customer.LastName;
            await context.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }

        private async Task<CustomerId> Insert(Customer customer, CancellationToken cancellationToken = default)
        {
            var customerDto = CustomerDto.From(customer);
            customerDto.Id = Guid.NewGuid();
            await context.AddAsync(customerDto, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return customerDto.Id;
        }
    }
}
