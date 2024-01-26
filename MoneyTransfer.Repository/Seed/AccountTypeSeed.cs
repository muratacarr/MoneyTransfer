using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTransfer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Repository.Seed
{
    public class AccountTypeSeed : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.HasData(
                new AccountType { Id = 1, Name = "TRY" },
                new AccountType { Id = 2, Name = "USD" },
                new AccountType { Id = 3, Name = "EUR" }
                );
        }
    }
}
