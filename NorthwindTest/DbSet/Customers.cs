﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTest.DbSet
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        public string CustomerID { get; set; } = "00000";
        public string CompanyName { get; set; } = "test";
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
    }
}
