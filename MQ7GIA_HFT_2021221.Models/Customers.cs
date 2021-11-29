using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Models
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; } //Fictional email address and email providers

        [Required]
        public long PhoneNumber { get; set; } //Fictional 11Digit phonenumbers

        [NotMapped]
        [JsonIgnore]
        public virtual Contracts Contract { get; set; }
        
        
        public int ContractId { get; set; }
    }
}
