using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221_Models
{
    [Table("Contracts")]
    class Contracts
    {
        [Key]
        public int ContractID { get; set; }
        public string ContractType { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ContractExpireDate { get; set; }
        public int CustomerID { get; set; }
        public int CarID { get; set; }
    }
}
