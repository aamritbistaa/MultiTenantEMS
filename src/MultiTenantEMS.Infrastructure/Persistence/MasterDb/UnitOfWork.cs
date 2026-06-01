using MultiTenantEMS.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEMS.Infrastructure.Persistence.MasterDb
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly MasterDbContext _dbContext;

        public UnitOfWork(MasterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
