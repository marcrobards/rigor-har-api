namespace Rigor.HAR.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using Rigor.HAR.API.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HarFileRepository : IHarFileRepository
    {
        private readonly ApiDbContext _dbContext;

        public HarFileRepository(ApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<HarFile>> GetAllAsync()
        {
            return await this._dbContext.HarFiles.ToListAsync<HarFile>();
        }

        public async Task<HarFile> GetByIdAsync(long id)
        {
            return await this._dbContext.HarFiles.FindAsync(id);
        }

        public async Task SaveAsync(HarFile harFile)
        {
            if (harFile == null)
            {
                throw new ArgumentNullException(nameof(harFile));
            }

            this._dbContext.HarFiles.Add(harFile);

            await this._dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(HarFile harFile)
        {
            if (harFile == null)
            {
                throw new ArgumentNullException(nameof(harFile));
            }

            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var existingHarFile = await this._dbContext.HarFiles.FindAsync(id);

            if (existingHarFile != null)
            {
                this._dbContext.HarFiles.Remove(existingHarFile);

                await this._dbContext.SaveChangesAsync();
            }
        }
    }
}
