using Microsoft.Extensions.DependencyInjection;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using Project.DAL.Repositories.Abstracts;
using Project.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Project.BLL.ServiceInjections
{
    public static class RepManService
    {
        public static IServiceCollection AddRepManServices(this IServiceCollection services)
        {
            //Scoped, Transient, Singleton

            //Scoped(Kapsamlı): Kapsamlı bir örnek, bir nesnenin belirli bir kapsam süresince oluşturulup sürdürüldüğü anlamına gelir. Bu kapsam genellikle belirli bir işlem veya kullanıcı isteği ile ilgilidir.Kapsam sona erdiğinde, kapsamlı nesne genellikle kaldırılır veya geri alınır.Bu yaklaşım, nesnelerin farklı uygulama bölümleri arasında izole edildiğinden ve paylaşılmadığından emin olmayı sağlar.

            //Transient (Geçici): Geçici bir örnek, her istendiğinde oluşturulur. Bu, belirli bir türün nesnesini her seferinde istediğinizde yeni bir örnek döndürüleceği anlamına gelir.Geçici örnekler kısa ömürlüdür ve uygulamanın farklı bölümleri veya farklı istekler arasında paylaşılmaz.Bir nesnenin her seferinde taze bir örneğine ihtiyaç duyulduğunda veya istenmeyen durum paylaşımını önlemek için kullanışlıdır.

            //Singleton (Tekil Örnek): Bir tekil örnek, tüm uygulama boyunca paylaşılan bir örnektir. Bu, belirli bir türün nesnesini ne kadar sık isterseniz isteyin, her zaman aynı örneği alacağınız anlamına gelir. Singletonlar genellikle paylaşılan kaynakları, yapılandırma ayarlarını veya diğer bileşenleri temsil eden nesneler için kullanılır.Bu şekilde, tek bir erişim ve yönetim noktası oluşturulabilir.

            //Bu Servisimiz için Scoped kullanacağız

            //Repositories
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            //Managers
            services.AddScoped(typeof(IManager<>),typeof(BaseManager<>));

            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IOrderDetailManager, OrderDetailManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IProfileManager, ProfileManager>();
            services.AddScoped<IAppUserManager, AppUserManager>();

            return services;

            //bu extention metodumuzu yarattıktan sonra çağrılması lazım. Bunuda Project.COREUI Startup'da ConfigureServices() metodunun yaşam alanına services.AddRepManServices(); yazarak yaparız.
        }
    }
}
