namespace Rigor.HAR.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using Rigor.HAR.API.Models;
    using Rigor.HAR.API.Services;
    using System;
    using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IEnumerable<HarFile>> Get()
        {
            return await this._harFilesService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var item = await this._harFilesService.GetByIdAsync(id);

            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task Post([FromBody]JObject harFileData)
        {
            if (harFileData != null)
            {
                var harFile = new HarFile();

                if (harFileData["log"] != null)
                {
                    var log = harFileData["log"];
                    if (log["pages"] != null)
                    {
                        var pages = log["pages"];
                        var firstPage = pages[0];

                        harFile.URL = (string)firstPage["title"];
                        harFile.StartedDateTime = DateTime.Parse(firstPage["startedDateTime"].ToString());
                        harFile.JSONContent = harFileData.ToString();

                        await this._harFilesService.SaveAsync(harFile);
                    }
                }
            }
        }

        [HttpPut("{id}")]
        public async Task Put(long id, [FromBody]JObject harFileData)
        {
            if (harFileData != null)
            {
                var existingHarFile = await this._harFilesService.GetByIdAsync(id);

                if (existingHarFile != null)
                {
                    if (harFileData["log"] != null)
                    {
                        var log = harFileData["log"];
                        if (log["pages"] != null)
                        {
                            var pages = log["pages"];
                            var firstPage = pages[0];

                            existingHarFile.URL = (string)firstPage["title"];
                            existingHarFile.StartedDateTime = DateTime.Parse(firstPage["startedDateTime"].ToString());
                            existingHarFile.JSONContent = harFileData.ToString();

                            await this._harFilesService.UpdateAsync(existingHarFile);
                        }
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await this._harFilesService.DeleteAsync(id);
        }
    }
}