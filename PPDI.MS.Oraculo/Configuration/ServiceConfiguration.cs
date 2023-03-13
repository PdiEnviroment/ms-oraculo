using PPDI.MS.Oraculo.Repository;

namespace PPDI.MS.Oraculo.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<INegociacaoRepostory, NegociacaoRepository>();

            return services;
        }
    }
}
