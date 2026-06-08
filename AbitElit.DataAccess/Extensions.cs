using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AbitElit.DataAccess
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IApplicantRepository, ApplicantRepository>();
            serviceCollection.AddDbContext<AbitElitDbContext>(x =>
            {
              x.UseNpgsql("host=localhost;Database=abit_elit;Username=postgres;Password=0000");  
            }
            );
            return serviceCollection;
        }
    }
}