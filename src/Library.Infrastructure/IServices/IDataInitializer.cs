﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}
