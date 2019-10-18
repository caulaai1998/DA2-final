using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class PublisherCompanyRepository : EFRepository<Publisher, int>, IPublisherCompanyRepository
    {
        AppDbContext _context;
        public PublisherCompanyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
