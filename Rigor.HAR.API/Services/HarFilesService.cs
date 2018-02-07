namespace Rigor.HAR.API.Services
{
    using Rigor.HAR.API.Data;
    using Rigor.HAR.API.Models;
    using System.Threading.Tasks;

    public class HarFilesService : IHarFilesService
    {
        private readonly IHarFileRepository _harFileRepository;

        public HarFilesService(IHarFileRepository harFileRepository)
        {
            this._harFileRepository = harFileRepository;
        }

        public async Task<HarFile> GetHarFileById(long id)
        {
            return await this._harFileRepository.GetByIdAsync(id);
        }
    }
}
