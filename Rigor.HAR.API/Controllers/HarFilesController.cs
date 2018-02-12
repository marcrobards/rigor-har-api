namespace Rigor.HAR.API.Controllers
{
    using HarSharp;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Schema;
    using Rigor.HAR.API.Models;
    using Rigor.HAR.API.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(item);
            }
        }

        [HttpGet("{id}/content")]
        public async Task<IActionResult> GetContent(long id)
        {
            var item = await this._harFilesService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(item.JSONContent);
            }
        }

        [HttpGet("{id}/blocked")]
        public async Task<IEnumerable<Entry>> GetBlockedEntries(long id)
        {
            return await this._harFilesService.GetBlockedEntries(id);
        }

        [HttpGet("{id}/bodySize")]
        public async Task<object> GetBodySize(long id)
        {
            var averageBodySize = await this._harFilesService.GetAverageBodySize(id);

            var totalBodySize = await this._harFilesService.GetTotalBodySize(id);

            var result = new { AverageBodySize = averageBodySize, TotalBodySize = totalBodySize };

            return result;
        }

        [HttpGet("{id}/responseUrl/{filter}")]
        public async Task<IEnumerable<string>> GetResponseUrlByFilter(long id, string filter)
        {
            var responsesFound = await this._harFilesService.GetRequestUrlsByFilter(id, filter);

            return responsesFound;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]JObject harFileData)
        {
            if (harFileData != null)
            {
                var harData = HarConvert.Deserialize(harFileData.ToString());

                var valid = this.ValidateHarFile(harData);

                if (valid)
                {
                    var firstPage = harData.Log.Pages.First();

                    var harFile = new HarFile();
                    harFile.URL = firstPage.Title;
                    harFile.StartedDateTime = firstPage.StartedDateTime;
                    harFile.JSONString = JsonConvert.SerializeObject(harFileData);

                    await this._harFilesService.SaveAsync(harFile);
                }
                else
                {
                    return BadRequest("The HAR file data is invalid.");
                }
            }

            return CreatedAtAction(nameof(Post), null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]Har harData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingHarFile = await this._harFilesService.GetByIdAsync(id);

            if (existingHarFile == null)
            {
                return NotFound();
            }
            else
            {
                var firstPage = harData.Log.Pages.First();

                var harFile = new HarFile();
                existingHarFile.URL = firstPage.Title;
                existingHarFile.StartedDateTime = firstPage.StartedDateTime;
                existingHarFile.JSONString = JsonConvert.SerializeObject(harData);

                await this._harFilesService.UpdateAsync(existingHarFile);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await this._harFilesService.DeleteAsync(id);

            return new ObjectResult(null);
        }

        private bool ValidateHarFile(Har harData)
        {
            try
            {
                var page = harData.Log.Pages.FirstOrDefault();

                if (page == null)
                {
                    return false;
                }
                else
                {
                    // validate title exists
                    if (string.IsNullOrEmpty(page.Title))
                    {
                        return false;
                    }

                    // validate startedDateTime exists
                    if (page.StartedDateTime == DateTime.MinValue)
                    {
                        return false;
                    }
                }

                // validate entries exist
                if (harData.Log.Entries.Count == 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}