using System;

namespace PostgreSqlEntityFramework.Domain.Entities
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string Fullname { get; set; }
    }
}