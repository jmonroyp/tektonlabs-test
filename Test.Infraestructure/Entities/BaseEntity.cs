using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Infraestructure.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}