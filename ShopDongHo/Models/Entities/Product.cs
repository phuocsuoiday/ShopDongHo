using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDongHo.Models.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? OriginalPrice { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public int Stock { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public Product()
        {
            Stock = 0;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}
