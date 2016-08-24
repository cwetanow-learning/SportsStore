using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Models;

namespace Store.Domain.Concrete
{
    public class IRepository : EFProductRepository, IUserRepository
    {
        public IEnumerable<IUser> Users
        {
            get
            {
                return this.context.Users;
            }
        }

        public void RegisterUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
