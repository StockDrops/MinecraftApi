﻿namespace MinecraftBlazingHub.Services.Integrations
{
    public interface IPatreonService
    {
        string GetOauth2Url(string requestId);
        void RedirectUserToOauth2(string requestId);
    }
}