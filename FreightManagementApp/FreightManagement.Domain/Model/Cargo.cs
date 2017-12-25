using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightManagement.Domain.Model
{
    public class Cargo
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150), Required]
        public string Description { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public StatusCargoEnum Status { get; set; }

        public int? OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public override string ToString()
        {
            return $"{Description}";
        }
    }

    public enum StatusCargoEnum
    {
        Waiting,
        InOrder
    }
}
