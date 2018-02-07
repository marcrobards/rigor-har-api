namespace Rigor.HAR.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Rigor.HAR.API.Data;
    using Rigor.HAR.API.Services;
    using Microsoft.EntityFrameworkCore;
    using Rigor.HAR.API.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApiDbContext>(opt => opt.UseInMemoryDatabase(ApiConstants.HarDB));

            services.AddMvc();
            services.AddTransient<ApiDbContext>();
            services.AddTransient<IHarFileRepository, HarFileRepository>();
            services.AddTransient<IHarFilesService, HarFilesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var dbContext = serviceProvider.GetService<ApiDbContext>();
            AddTestData(dbContext);

            app.UseMvc();
        }

        private void AddTestData(ApiDbContext dbContext)
        {
            var test = new HarFile
            {
                HarFileId = 1,
                StartedDateTime = DateTime.Now,
                URL = "https://www.microsoft.com/net/",
                JSONContent = ""
            };

            dbContext.HarFiles.Add(test);
            dbContext.SaveChanges();
        }
    }
}
