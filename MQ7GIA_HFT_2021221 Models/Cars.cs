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
    public class Cars
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string CarModell { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        public int? Warranty { get; set; }
        [Required]
        public string EngineDisplacement { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public string Transmission { get; set; }

        [Required]
        public int Odometer { get; set; }

        public DateTime? MOTUntil { get; set; }

        [Required]
        public int LeasePrice { get; set; }

        [Required]
        public int SellingPrice { get; set; }


        [NotMapped]
        public virtual Departments Department { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
    }
}
