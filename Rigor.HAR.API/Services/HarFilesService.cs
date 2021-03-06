﻿namespace Rigor.HAR.API.Services
{
    using HarSharp;
    using Rigor.HAR.API.Data;
    using Rigor.HAR.API.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<IEnumerable<Entry>> GetBlockedEntries(long id)
        {
            try
            {
                var harFile = await this._harFileRepository.GetByIdAsync(id);
                if (harFile == null)
                {
                    throw new Exception($"HarFile {id} not found.");
                }

                var harModel = HarConvert.Deserialize(harFile.HarContentString);

                var blockedEntires = harModel.Log.Entries.OrderByDescending(e => e.Timings.Blocked);

                return blockedEntires;
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled Service Exception", ex);
            }
        }

        public async Task<double> GetAverageBodySize(long id)
        {
            try
            {
                var harFile = await this._harFileRepository.GetByIdAsync(id);
                if (harFile == null)
                {
                    throw new Exception($"HarFile {id} not found.");
                }

                var harModel = HarConvert.Deserialize(harFile.HarContentString);

                var avgBodySize = harModel.Log.Entries.Average(e => e.Response.BodySize);

                return avgBodySize;
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled Service Exception", ex);
            }
        }

        public async Task<double> GetTotalBodySize(long id)
        {
            try
            {
                var harFile = await this._harFileRepository.GetByIdAsync(id);
                if (harFile == null)
                {
                    throw new Exception($"HarFile {id} not found.");
                }

                var harModel = HarConvert.Deserialize(harFile.HarContentString);

                var totalBodySize = harModel.Log.Entries.Sum(e => e.Response.BodySize);

                return totalBodySize;
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled Service Exception", ex);
            }
        }

        public async Task<IEnumerable<string>> GetRequestUrlsByFilter(long id, string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                throw new ArgumentNullException(nameof(filter));
            }

            try
            {
                var harFile = await this._harFileRepository.GetByIdAsync(id);
                if (harFile == null)
                {
                    throw new Exception($"HarFile {id} not found.");
                }

                var harModel = HarConvert.Deserialize(harFile.HarContentString);

                var urlsFound = harModel.Log.Entries.Select(e => e.Request.Url.AbsoluteUri).Where(u => u.Contains(filter));

                return urlsFound;
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled Service Exception", ex);
            }
        }
    }
}
