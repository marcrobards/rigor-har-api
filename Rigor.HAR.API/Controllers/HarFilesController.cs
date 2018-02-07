namespace Rigor.HAR.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Rigor.HAR.API.Services;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/harfiles")]
    public class HarFilesController : Controller
    {
        private readonly IHarFilesService _harFilesService;

        public HarFilesController(IHarFilesService harFilesService)
        {
            this._harFilesService = harFilesService;
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await this._harFilesService.GetHarFileById(id);

            return new ObjectResult(item);
        }
    }
}