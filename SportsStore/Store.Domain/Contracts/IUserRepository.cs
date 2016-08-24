using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Users { get; }

        void RegisterUser(User user);
    }
}
