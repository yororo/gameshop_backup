using NWebsec.AspNetCore.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static class NWebsecExtensions
    {
        public static IApplicationBuilder UseNWebsec(this IApplicationBuilder app)
        {
            app.UseCsp(options => options.DefaultSources(directive => directive.Self())
                .ImageSources(directive => directive.Self()
                    .CustomSources("*"))
                .ScriptSources(directive => directive.Self()
                    .UnsafeInline())
                .StyleSources(directive => directive.Self()
                    .UnsafeInline()));

            app.UseXContentTypeOptions();

            app.UseXfo(options => options.Deny());

            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            
            return app;
        }
    }
}