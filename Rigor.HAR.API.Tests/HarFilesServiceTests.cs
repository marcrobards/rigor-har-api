using HarSharp;
using Moq;
using Rigor.HAR.API.Data;
using Rigor.HAR.API.Models;
using Rigor.HAR.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rigor.HAR.API.Tests
{
    public class HarFilesServiceTests
    {
        private IHarFileRepository _harFileRepository;

        private List<HarFile> _harFiles;

        private Har _harFile;

        private IHarFilesService _harFilesService;


        public HarFilesServiceTests()
        {
            this._harFiles = new List<HarFile>();

            this._harFileRepository = SetupHarFileRepository();

            this._harFilesService = new HarFilesService(this._harFileRepository);
        }

        [Fact]
        public async void GetAverageBodySize()
        {
            var harFile = new HarFile
            {
                StartedDateTime = DateTime.Now,
                URL = "https://www.microsoft.com/net",
                JSONString = File.ReadAllText(@"www.microsoft.com.har")
            };

            await this._harFileRepository.SaveAsync(harFile);

            var avgBodySize = await this._harFilesService.GetAverageBodySize(1);

            Assert.Equal(1383.2258064516129, avgBodySize);
        }

        [Fact]
        public async void GetRequestUrlsByFilter()
        {
            var harFile = new HarFile
            {
                StartedDateTime = DateTime.Now,
                URL = "https://www.microsoft.com/net",
                JSONString = File.ReadAllText(@"www.microsoft.com.har")
            };

            await this._harFileRepository.SaveAsync(harFile);

            var filter = "images";

            var foundUrls = await this._harFilesService.GetRequestUrlsByFilter(1, filter);

            Assert.Equal(11, foundUrls.Count());

        }

        private IHarFileRepository SetupHarFileRepository()
        {
            var repo = new Mock<IHarFileRepository>();

            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(this._harFiles);

            repo.Setup(r => r.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Func<long, HarFile>(
                    id => this._harFiles.Find(h => h.HarFileId.Equals(id))));

            repo.Setup(r => r.SaveAsync(It.IsAny<HarFile>()))
                .Callback(new Action<HarFile>(newHarFile =>
                {
                    dynamic nextHarFileId = this._harFiles.Select(h => h.HarFileId).DefaultIfEmpty().Max() + 1;
                    newHarFile.HarFileId = nextHarFileId;
                    this._harFiles.Add(newHarFile);
                }))
                .Returns(Task.FromResult(repo));

            return repo.Object;
        }
    }
}
