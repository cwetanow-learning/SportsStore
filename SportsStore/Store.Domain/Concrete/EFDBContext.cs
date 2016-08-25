using Microsoft.AspNet.Identity.EntityFramework;
using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Concrete
{
    public class EFDBContext : IdentityDbContext<User>
    {
        public EFDBContext()
            : base("EFDbContext", throwIfV1Schema: false)
        {
        }

        public DbSet<Product> Products { get; set; }

        
    }
}
