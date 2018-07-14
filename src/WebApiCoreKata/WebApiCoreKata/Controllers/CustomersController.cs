using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreKata.Domain.Entities;
using WebApiCoreKata.Models;
using WebApiCoreKata.Persistence;

namespace WebApiCoreKata.Controllers
{
  [Route("api/[controller]")]
  public class CustomersController : Controller
  {
    private readonly ICustomersRepository customersRepository;

    public CustomersController(ICustomersRepository customersRepository)
    {
      this.customersRepository = customersRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<CustomerPresenter>> GetAll(CancellationToken cancellationToken = default)
    {
      var customers = await customersRepository.GetAll(cancellationToken).ConfigureAwait(false);

      return customers.Select(CustomerPresenter.From);
    }
  }
}