using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rp3.Test.Common.Models
{
    public class BalanceView
    {
        public int AccountId { get; set; }
        public string Categoria { get; set; }
        public decimal Saldo { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
    }
}
