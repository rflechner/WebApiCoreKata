using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiCoreKata.Domain.Entities;
using WebApiCoreKata.Domain.ValueObjects;

namespace WebApiCoreKata.Persistence
{
    public interface ICustomersRepository
    {
        Task<IReadOnlyCollection<Customer>> GetAll(CancellationToken cancellationToken = default);
        Task<CustomerId> Save(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer> Get(CustomerId id, CancellationToken cancellationToken = default);
        Task<bool> Exists(CustomerId id, CancellationToken cancellationToken = default);
    }
}
