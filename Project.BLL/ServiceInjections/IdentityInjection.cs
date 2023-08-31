using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{
    public static class IdentityInjection
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole<int>>(
                x =>
                {
                    x.Password.RequireDigit = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequiredLength = 8;

                }).AddEntityFrameworkStores<MyContext>();

            return services;

            //bu extention metodumuzu yarattıktan sonra çağrılması lazım. Bunuda Project.COREUI Startup'da ConfigureServices() metodunun yaşam alanına services.AddIdentityService(); yazarak yaparız.
        }
    }
}
