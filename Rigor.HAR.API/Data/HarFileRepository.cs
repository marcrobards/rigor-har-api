namespace Rigor.HAR.API.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Rigor.HAR.API.Models;

    public class HarFileRepository : IHarFileRepository
    {
        private readonly ApiDbContext _dbContext;

        public HarFileRepository(ApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<HarFile> GetByIdAsync(long id)
        {
            return await this._dbContext.HarFiles.FindAsync(id);
        }
    }
}
