using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreKata.Domain.Entities;
using WebApiCoreKata.Persistence;

namespace WebApiCoreKata.Controllers
{
  [Route("api/[controller]")]
  public class CustomersController : Controller
  {
    private ICustomersRepository customersRepository;

    public CustomersController(ICustomersRepository customersRepository)
    {
      this.customersRepository = customersRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Customer>> GetAll(CancellationToken cancellationToken)
    {
      return null;
    }
  }
}