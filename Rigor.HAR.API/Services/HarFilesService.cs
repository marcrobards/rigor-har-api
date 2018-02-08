namespace Rigor.HAR.API.Services
{
    using Rigor.HAR.API.Data;
    using Rigor.HAR.API.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HarFilesService : IHarFilesService
    {
        private readonly IHarFileRepository _harFileRepository;

        public HarFilesService(IHarFileRepository harFileRepository)
        {
            this._harFileRepository = harFileRepository;
        }

        public async Task<IEnumerable<HarFile>> GetAllAsync()
        {
            return await this._harFileRepository.GetAllAsync();
        }

        public async Task<HarFile> GetByIdAsync(long id)
        {
            return await this._harFileRepository.GetByIdAsync(id);
        }

        public async Task SaveAsync(HarFile harFile)
        {
            try
            {
                await this._harFileRepository.SaveAsync(harFile);
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled Service Exception", ex);
            }
        }

        public async Task UpdateAsync(HarFile harFile)
        {
            try
            {
                await this._harFileRepository.UpdateAsync(harFile);
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled Service Exception", ex);
            }
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                await this._harFileRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled Service Exception", ex);
            }
        }
    }
}
