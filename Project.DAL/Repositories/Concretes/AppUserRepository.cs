using Microsoft.AspNetCore.Identity;
using Project.DAL.Context;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        //Kendimize özel CRUD işlemlerinin yine olması gereki fakat Microsoft.Identity yapısının özel şifrelemeler ve yetkilendirmeleri için hazır async metotları vardır. Bu metotların kullanımı içinde ek olarak AppUserRepository'de ayrı alanlar açmak en dogrusudur. Bu metotlar da Microsoft.Identity içerisinde gömülü olan Manager sınıfları içerisinde bulunur (UserManager ve SingInManager). Bu sınıflarda Dependency Injection ile çalışırlar ve dolayısıyla Constructor based injection yapılabilir.

        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;

        public AppUserRepository(MyContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //özel bir Identity metodu async olarak tanımlanmalıdır. Burada bir API kullanılmaktadır ve bu API requestlerinin bloklanmadan devam edebilmesi ve işlemleri tamamlayarak hatasız bir şekilde akıcı olması için await keywordunun kullanılması gerekir. Metot'da await keywordunun kullanılabilmesi için o metodun async şeklinde tanımlanması ve Task döndürmesi gerekir
        public async Task<bool> AddUser(AppUser item)
        {
            //sadece async marklı metotlar içerisinde await kullanabiliriz.
            IdentityResult result = await _userManager.CreateAsync(item,item.PasswordHash);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(item, isPersistent: false); //isPersistent durumu Cookie'de dursunmu durmasınmı belirler.
                return true;
            }
            return false;
        }
    }
}
