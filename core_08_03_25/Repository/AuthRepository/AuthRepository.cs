﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core_08_03_25.Repository.AuthRepository
{
    public abstract class AuthRepository
    {
        public abstract Task<bool> RegisterAsync(string email, string password);

        public abstract Task<bool> LoginAsync(string email, string password);
    }
}
