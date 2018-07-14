using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCoreKata.Domain.Entities;

namespace WebApiCoreKata.Persistence
{
    public interface ICustomersRepository
    {
        Task<IReadOnlyCollection<Customer>> GetAll();

        Task Save(Customer customer);
    }
}
