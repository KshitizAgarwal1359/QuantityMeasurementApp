namespace Presentation.API
{
    // UC21: API Gateway — YARP Reverse Proxy — Port 5000
    // Single entry point for the Angular frontend.
    // Routes requests to BusinessLogic.API and DataRepository.API.
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // UC21: Load YARP reverse proxy configuration from appsettings.json
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            // UC21: CORS — only the Gateway needs this, frontend talks to Gateway only
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseCors();
            app.MapReverseProxy();
            app.Run();
        }
    }
}
