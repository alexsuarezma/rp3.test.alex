using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rp3.Test.Mvc.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public short TransactionTypeId { get; set; }
        public int CategoryId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ShortDescription { get; set; }
        [Range(1, 99999), DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public string CategoryName { get; set; }
        public string TransactionType { get; set; }
        public string PersonName { get; set; }
        public string PersonId { get; set; }
    }
}