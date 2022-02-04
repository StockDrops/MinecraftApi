namespace MinecraftBlazingHub.Services.Integrations
{
    public interface IBlazorPatreonService : MinecraftApi.Integrations.Contracts.Patreon.IPatreonService
    {
        string GetOauth2Url(string requestId);
        void RedirectUserToOauth2(string requestId);
    }
}