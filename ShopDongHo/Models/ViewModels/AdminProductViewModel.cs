using System.ComponentModel.DataAnnotations;

namespace ShopDongHo.Models.ViewModels
{
    public class AdminProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(0, 999999999, ErrorMessage = "Giá không hợp lệ")]
        public decimal Price { get; set; }

        [Range(0, 999999999, ErrorMessage = "Giá gốc không hợp lệ")]
        public decimal? OriginalPrice { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(0, 99999, ErrorMessage = "Số lượng không hợp lệ")]
        public int Stock { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
