namespace Rigor.HAR.API.Services
{
    using Rigor.HAR.API.Models;
    using HarSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHarFilesService
    {
        Task<IEnumerable<HarFile>> GetAllAsync();

        Task<HarFile> GetByIdAsync(long id);

        Task SaveAsync(HarFile harFile);

        Task UpdateAsync(HarFile harFile);

        Task DeleteAsync(long id);

        Task<IEnumerable<Entry>> GetBlockedEntries(long id);

        Task<double> GetAverageBodySize(long id);

        Task<double> GetTotalBodySize(long id);

        Task<IEnumerable<string>> GetRequestUrlsByFilter(long id, string filter);
    }
}
