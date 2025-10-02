using ShopDongHo.Models.Entities;
using System.Collections.Generic;

namespace ShopDongHo.Models.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> Brands { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SelectedBrand { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SearchKeyword { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public ProductViewModel()
        {
            Products = new List<Product>();
            Categories = new List<Category>();
            Brands = new List<string>();
            PageSize = 12;
            CurrentPage = 1;
        }
    }
}
