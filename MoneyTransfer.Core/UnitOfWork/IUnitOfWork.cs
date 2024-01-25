using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
