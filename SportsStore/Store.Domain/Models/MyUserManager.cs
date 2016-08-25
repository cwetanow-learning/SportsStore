using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Store.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Models
{
    public class MyUserManager : UserManager<User>
    {
        public MyUserManager(IUserStore<User> store) : base(store)
        {
        }

        public static MyUserManager Create(
        IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            var manager = new MyUserManager(
                new UserStore<User>(context.Get<EFDBContext>()));

            return manager;
        }
    }
}
