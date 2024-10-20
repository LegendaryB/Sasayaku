namespace Sasayaku
{
    public static class WebApplicationFactory
    {
        public static async Task<WebApplication> CreateAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddServices();

            var app = builder.Build();
            await app.ConfigureAsync();

            return app;
        }
    }
}
