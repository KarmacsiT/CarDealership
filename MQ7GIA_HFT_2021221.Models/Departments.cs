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
    [Table("Departments")]
    public class Departments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Address { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Cars> CarCollection { get; set; }

        public Departments()
        {
            CarCollection = new HashSet<Cars>();
        }
    }
}
