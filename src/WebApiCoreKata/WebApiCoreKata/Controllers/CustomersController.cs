using System;
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
    [Route("all")]
    public async Task<IEnumerable<CustomerPresenter>> GetAll(CancellationToken cancellationToken = default)
    {
      var customers = await customersRepository.GetAll(cancellationToken).ConfigureAwait(false);

      return customers.Select(CustomerPresenter.From);
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
    {
      var customer = await customersRepository.Get(id, cancellationToken);
      if (customer == null)
        return NotFound();

      return Json(CustomerPresenter.From(customer));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody]CustomerPresenter model, CancellationToken cancellationToken = default)
    {
      var customer = model.ToDomain();
      var customerId = await customersRepository.Save(customer, cancellationToken);

      model.Id = customerId.Value;
      
      return AcceptedAtAction("Get", new {id = customerId.Value}, model);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody]CustomerPresenter model, CancellationToken cancellationToken = default)
    {
      if (!await customersRepository.Exists(model.Id, cancellationToken))
        return NotFound();
      
      var customer = model.ToDomain();
      await customersRepository.Save(customer, cancellationToken);

      return AcceptedAtAction("Get", new {id = customer.Id.Value}, model);
    }
  }
}