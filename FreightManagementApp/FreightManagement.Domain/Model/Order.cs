using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightManagement.Domain.Model
{
    public class Order
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short Distance { get; set; }

        [StringLength(50), Required]
        public string Country { get; set; }

        [StringLength(50), Required]
        public string City { get; set; }

        [StringLength(150), Required]
        public string Address { get; set; }

        [Required]
        public short Term { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        public int? TruckId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(TruckId))]
        public virtual Truck Truck { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Cargo> Cargoes { get; set; } = new HashSet<Cargo>();

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }

    public enum StatusEnum
    {
        InQueue,
        Processing,
        Ended,
    }
}
