using System;
using System.ComponentModel.DataAnnotations;

namespace BMS.Models.Entities
{
    public class Accounts
    {
        [Key]
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string PAN { get; set; }
        public string ContactNo { get; set; }
        public DateTime DOB { get; set; }
        public string AccountType { get; set; }

    }
}
