﻿using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Data.IRepositories
{
    public interface ITagRepository : IRepository<Tag,string>
    {
    }
}
