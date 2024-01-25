using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
    }
}
