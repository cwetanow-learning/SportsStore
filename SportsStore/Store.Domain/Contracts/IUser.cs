using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IUser
    {
        int UserId { get; }
        string Username { get; set; }

        string Password { get; set; }

        string ConfirmPassword { get; set; }

        string Email { get; set; }

        string Name { get; set; }
    }
}
