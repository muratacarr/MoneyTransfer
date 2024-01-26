using MoneyTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public int AccountTypeId { get; set; }
        public bool IsActive { get; set; }
        public int AppUserId { get; set; }
    }
}
