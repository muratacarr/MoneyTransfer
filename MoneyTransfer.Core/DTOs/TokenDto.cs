﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransfer.Core.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
