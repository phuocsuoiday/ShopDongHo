using System.Web;

namespace ShopDongHo.Helpers
{
    public static class AuthHelper
    {
        public static bool IsAuthenticated()
        {
            return HttpContext.Current.Session["UserId"] != null;
        }

        public static int? GetCurrentUserId()
        {
            return HttpContext.Current.Session["UserId"] as int?;
        }

        public static string GetCurrentUsername()
        {
            return HttpContext.Current.Session["Username"] as string;
        }

        public static string GetCurrentUserRole()
        {
            return HttpContext.Current.Session["UserRole"] as string;
        }

        public static bool IsAdmin()
        {
            return GetCurrentUserRole() == "Admin";
        }

        public static void SetUserSession(int userId, string username, string role)
        {
            HttpContext.Current.Session["UserId"] = userId;
            HttpContext.Current.Session["Username"] = username;
            HttpContext.Current.Session["UserRole"] = role;
        }

        public static void ClearUserSession()
        {
            HttpContext.Current.Session.Remove("UserId");
            HttpContext.Current.Session.Remove("Username");
            HttpContext.Current.Session.Remove("UserRole");
        }
    }
}
