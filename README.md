# WebApiCoreKata

In progress ...

## Goal

Create a WebApi Kata to develop a Web API using:

- TDD
- Hexagonal Architecture
- `Microsoft.AspNetCore.Mvc.Testing`
- `Microsoft.EntityFrameworkCore`

## EF steps

Create `KataContext` (EF DbContext):

```shell
dotnet ef dbcontext scaffold "Server=(local)\SQLEXPRESS;Database=WebApiCoreKata;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Database -c KataContext
```

---

Add customers to the context:

```csharp
public DbSet<CustomerDto> Customers { get; set; }
```

---

Create the migration:

```shell
dotnet ef migrations add add_customers
```

---

Apply migration:

```shell
dotnet ef database update
```
