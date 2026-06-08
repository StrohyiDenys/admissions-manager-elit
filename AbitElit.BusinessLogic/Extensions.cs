using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AbitElit.BusinessLogic
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IApplicantService, ApplicantService>(); 
            serviceCollection.AddScoped<IAuthService, AuthService>();

            return serviceCollection;
        }
    }
}