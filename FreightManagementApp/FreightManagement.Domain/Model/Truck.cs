using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightManagement.Domain.Model
{
    public class Truck
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required]
        public short CarryingCapacity { get; set; }

        [Required]
        public decimal CostPerKm { get; set; }

        [Required]
        public AvailabilityEnum Status { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    public enum AvailabilityEnum
    {
        Free,
        Busy
    }
}
