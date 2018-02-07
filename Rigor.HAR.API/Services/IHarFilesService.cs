namespace Rigor.HAR.API.Services
{
    using Rigor.HAR.API.Models;
    using System.Threading.Tasks;

    public interface IHarFilesService
    {
        Task<HarFile> GetHarFileById(long id);
    }
}
