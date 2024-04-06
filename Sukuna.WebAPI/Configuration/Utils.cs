namespace Sukuna.WebAPI.Configuration
{
    public static class Utils // static signie quellle vivra tout le long du programme
    {
        public static string? GetConnectionString(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("ConnectionStrings")[name];
        }
    }
}
