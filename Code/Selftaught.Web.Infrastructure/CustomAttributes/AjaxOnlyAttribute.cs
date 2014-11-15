namespace Selftaught.Web.Infrastructure.CustomAttributes
{
    using System.Reflection;
    using System.Web.Mvc;

    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}
