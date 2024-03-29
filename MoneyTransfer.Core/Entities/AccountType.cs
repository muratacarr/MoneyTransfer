﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.Entities
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Account>? Accounts { get; set; }
    }
}
