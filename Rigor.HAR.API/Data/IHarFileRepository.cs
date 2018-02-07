namespace Rigor.HAR.API.Data
{
    using Rigor.HAR.API.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IHarFileRepository
    {
        Task<HarFile> GetByIdAsync(long id);
    }
}
