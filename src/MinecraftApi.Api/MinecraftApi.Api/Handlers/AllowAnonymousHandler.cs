using Microsoft.AspNetCore.Authorization;

namespace MinecraftApi.Api.Handlers
{
    /// <summary>
    /// This authorisation handler will bypass all requirements
    /// </summary>
    public class AllowAnonymousHandler : IAuthorizationHandler
    {
        /// <inheritdoc/>
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (IAuthorizationRequirement requirement in context.PendingRequirements.ToList())
            {
                context.Succeed(requirement); //Simply pass all requirements
            }
            return Task.CompletedTask;
        }
    }
}
