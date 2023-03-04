using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.WebAPI.Attributes;

namespace User.WebAPI
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception != null)//只有action执行成功才自动调用SaveChanges
            {
                return;
            }
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDesc == null)
            {
                return;
            }
            if (actionDesc.MethodInfo.IsDefined(typeof(UnitOfWorkAttribute), true))
            {
                var unitOfWorkAttribute = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
                var dbContextTypes = unitOfWorkAttribute.DbContextTypes;
                foreach ( var dbContextType in dbContextTypes)
                {
                    var dbContext = context.HttpContext.RequestServices.GetService(dbContextType)
                        as DbContext;//DI要DbContext实例
                    if (dbContext != null)
                    {
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
