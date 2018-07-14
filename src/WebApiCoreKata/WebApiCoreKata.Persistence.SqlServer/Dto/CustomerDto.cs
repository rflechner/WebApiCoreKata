using System;
using WebApiCoreKata.Domain.Entities;

namespace WebApiCoreKata.Persistence.SqlServer.Dto
{
  public class CustomerDto
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birth { get; set; }
    
    public static CustomerDto From(Customer model)
    {
      return new CustomerDto
      {
        Id = model.Id.Value,
        Birth = model.Birth,
        FirstName = model.FirstName,
        LastName = model.LastName
      };
    }

    public Customer ToDomain() => new Customer(Id, FirstName, LastName, Birth);
  }
}