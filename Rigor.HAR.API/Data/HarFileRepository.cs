namespace Rigor.HAR.API.Data
{
    using Rigor.HAR.API.Models;
    using System.Threading.Tasks;

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
