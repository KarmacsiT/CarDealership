using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221_Models
{
    [Table("Cars")]
    class Cars
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; }
        [Required]
        public int DepartmentID { get; set; }
        [Required]
        public string CarBrand { get; set; }
        [Required]
        public string CarModell { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public int Warranty { get; set; }
        [Required]
        public string EngineDisplacement { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]
        public int Odometer { get; set; }
        [Required]
        public DateTime MOT { get; set; }
        [Required]
        public int LeasePrice { get; set; }
        [Required]
        public int SellingPrice { get; set; }
    }
}
