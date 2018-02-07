namespace Rigor.HAR.API.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Rigor.HAR.API.Data;
    using Rigor.HAR.API.Models;

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
