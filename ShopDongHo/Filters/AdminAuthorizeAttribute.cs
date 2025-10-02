using ShopDongHo.Helpers;
using System.Web.Mvc;

namespace ShopDongHo.Filters
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!AuthHelper.IsAuthenticated() || !AuthHelper.IsAdmin())
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
