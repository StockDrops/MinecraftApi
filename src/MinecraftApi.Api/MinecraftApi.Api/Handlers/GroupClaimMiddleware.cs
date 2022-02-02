using System.Security.Claims;

namespace MinecraftApi.Api.Handlers
{
    /// <summary>
    /// Middleware to grab the groups of the Azure AD B2C user.
    /// It adds the claim on the go, and adds all its roles.
    /// </summary>
    public class GroupClaimMiddleware
    {

        //private readonly RequestDelegate _next;
        //public GroupClaimMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        //public async Task InvokeAsync(HttpContext httpContext, MicrosoftGraphService graphService)
        //{
        //    if (httpContext.User != null && httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
        //    {
        //        var userId = httpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").First().Value;
        //        var groups = await graphService.GetGroups(userId);
        //        var identity = httpContext.User.Identities.FirstOrDefault();
        //        if (identity != null)
        //        {
        //            foreach (var group in groups)
        //            {
        //                identity.AddClaim(new Claim(identity.RoleClaimType, group));
        //            }
        //        }
        //    }
        //    await _next(httpContext);
        //}
    }
}
