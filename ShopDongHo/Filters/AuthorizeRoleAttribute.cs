using ShopDongHo.Helpers;
using System.Web.Mvc;

namespace ShopDongHo.Filters
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        public string Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!AuthHelper.IsAuthenticated())
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            if (!string.IsNullOrEmpty(Role) && AuthHelper.GetCurrentUserRole() != Role)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
