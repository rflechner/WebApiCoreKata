using System;

namespace WebApiCoreKata.Domain.ValueObjects
{
    public struct CustomerId
    {
        public CustomerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
        public bool IsEmpty => Value == Guid.Empty;

        public static implicit operator CustomerId(Guid value) => new CustomerId(value);
    }
}