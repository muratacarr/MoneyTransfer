using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.DTOs
{
    public class TransferMoneyDto
    {
        public int SenderAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public int Money { get; set; }
    }
}
