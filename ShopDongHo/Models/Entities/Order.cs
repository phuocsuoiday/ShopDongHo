using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDongHo.Models.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(255)]
        public string CustomerAddress { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            Status = "Pending";
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
