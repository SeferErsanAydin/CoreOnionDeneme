using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceInjections
{ 
    //DbContextPool'umuz böylece StartUp'da DAL'de bir sınıfı kullanmak yerine BLL'deki bu ifadelerle IServiceCollection tipine bir metot entegre edecek
    public static class DbContextService
    {
        public static IServiceCollection AddDbContextService(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();//provider belirlenmezse GetService'e ulaşılamaz

            //IConfiguration kullanırken kütüphane önerilerinde ilk Castle kütüphanesi gelmektedir fakat kullanacağımız kütüphane Microsoft.Extentions.Configuration olmak zorundadır DIKKAT!

            IConfiguration configuration = provider.GetService<IConfiguration>(); //appsettings.jsona connection stringimiz için ulaştık

            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());//DB mizi singleton patterne göre poola attık, Sql Server kullanıcağımızı ve LazyLoading kullanmak istediğimizi burada bu şekilde belirtiyoruz.

            return services;

            //bu extention metodumuzu yarattıktan sonra çağrılması lazım. Bunuda Project.COREUI Startup'da ConfigureServices() metodunun yaşam alanına services.AddDbContextService(); yazarak yaparız.
        }
    }
}
