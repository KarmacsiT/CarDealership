using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ7GIA_HFT_2021221.Models
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

        public int? Warranty { get; set; } //Warranty measured in years

        public double? EngineDisplacement { get; set; }

        [Required]
        public string FuelType { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public string Transmission { get; set; }

        [Required]
        public int Mileage { get; set; }

        public DateTime? MOTUntil { get; set; }

        [Required]
        public int LeasePrice { get; set; } //Lease Price per mounth in HUF(Ft) [based of Használtautó.hu data and common sense]

        [Required]
        public int SellingPrice { get; set; } //Selling price in HUF(Ft) [based of Használtautó.hu data and common sense]


        [NotMapped]
        public virtual Departments Department { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
    }
}
