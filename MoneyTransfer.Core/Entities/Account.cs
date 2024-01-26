using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; } = null!;
        public bool IsActive { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
