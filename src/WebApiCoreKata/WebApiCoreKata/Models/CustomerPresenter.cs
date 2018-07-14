using System;
using WebApiCoreKata.Domain.Entities;

namespace WebApiCoreKata.Models
{
  public class CustomerPresenter
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birth { get; set; }
    
    public static CustomerPresenter From(Customer model)
    {
      return new CustomerPresenter
      {
        Id = model.Id.Value,
        Birth = model.Birth,
        FirstName = model.FirstName,
        LastName = model.LastName
      };
    }
  }
}