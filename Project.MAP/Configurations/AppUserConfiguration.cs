using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configurations
{
    public class AppUserConfiguration : BaseConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Profile).WithOne(x => x.AppUser).HasForeignKey<AppUserProfile>(x => x.ID);//1 e 1 ilişki ayarı

            //AppUser classının yazılan propertylerin yanı sıra Microfost'un Identity kütüphanesinden gelen propertyleride olacaktır. Identity'den gelen bu property lerin içerisinde primary key olan "Id" ismine sahip olan bir property daha olacaktır. Dolayısıyla bu class tabloya çevirilirken hem kendi "ID" propertysini hemden Microsoft Identity kütüphanesin den gelen "Id"  propertysini Sql deki incasesensitive durumu yüzünden aynı sütun sayarak migration durumunda tabloda aynı isimde iki sütun olamaz hatası verecektir. Bu yüzden kendi ID mizi Sql'e göndermemeliyiz.

            builder.Ignore(x=> x.ID);
        }
    }
}
