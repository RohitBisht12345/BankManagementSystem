using System;
using System.ComponentModel.DataAnnotations;

namespace BMS.Models.Entities
{
    public class Loans
    {
        [Key]
        public Guid LoanID { get; set; }
        public Guid AccountID { get; set; }
        public string LoanType { get; set; }
        public string LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }
        public string InterestRate { get; set; }
        public string LoanDuration { get; set; }
    }
}