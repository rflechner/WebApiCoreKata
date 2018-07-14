using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiCoreKata.Domain.Entities;

namespace WebApiCoreKata.Persistence
{
    public interface ICustomersRepository
    {
        Task<IReadOnlyCollection<Customer>> GetAll(CancellationToken cancellationToken = default);

        Task Save(Customer customer, CancellationToken cancellationToken = default);
    }
}
