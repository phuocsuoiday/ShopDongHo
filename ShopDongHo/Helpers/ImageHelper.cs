using System;
using System.IO;
using System.Web;

namespace ShopDongHo.Helpers
{
    public static class ImageHelper
    {
        private const string ProductImageFolder = "~/Content/images/products/";
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private const int MaxFileSize = 5 * 1024 * 1024;

        public static string SaveProductImage(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
                return null;

            if (file.ContentLength > MaxFileSize)
                throw new Exception("File size exceeds 5MB limit");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (Array.IndexOf(AllowedExtensions, extension) == -1)
                throw new Exception("Invalid file type. Only jpg, jpeg, png, gif are allowed");

            var fileName = Guid.NewGuid().ToString() + extension;
            var path = Path.Combine(HttpContext.Current.Server.MapPath(ProductImageFolder), fileName);

            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            file.SaveAs(path);

            return fileName;
        }

        public static void DeleteProductImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var path = Path.Combine(HttpContext.Current.Server.MapPath(ProductImageFolder), fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static string GetProductImageUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "/Content/images/no-image.png";

            return $"/Content/images/products/{fileName}";
        }
    }
}
