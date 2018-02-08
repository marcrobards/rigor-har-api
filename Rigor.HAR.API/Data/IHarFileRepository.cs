namespace Rigor.HAR.API.Data
{
    using Rigor.HAR.API.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHarFileRepository
    {
        Task<IEnumerable<HarFile>> GetAllAsync();

        Task<HarFile> GetByIdAsync(long id);

        Task SaveAsync(HarFile harFile);

        Task UpdateAsync(HarFile harFile);

        Task DeleteAsync(long id);
    }
}
