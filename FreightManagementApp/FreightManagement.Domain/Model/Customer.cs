using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightManagement.Domain.Model
{
    public class Customer
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [StringLength(50), Required]
        public string Manager { get; set; }

        [StringLength(20), Required]
        public string Telephone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
