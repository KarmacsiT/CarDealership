using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MQ7GIA_HFT_2021221.Models
{
    [Table("Contracts")]
    public class Contracts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractID { get; set; }

        [Required]
        public string ContractType { get; set; } //Contract Types: Lease, Sell

        [Required]
        public DateTime ContractDate { get; set; }

        public DateTime? ContractExpiryDate { get; set; } //Expiry Date of Lease Contracts


        [NotMapped]
        [JsonIgnore]
        public virtual Customers Customer { get; set; }
        public int CustomerID { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Cars Car { get; set; }
        
        public int CarID { get; set; }
    }
}
