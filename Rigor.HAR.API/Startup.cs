namespace Rigor.HAR.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Linq;
    using Rigor.HAR.API.Data;
    using Rigor.HAR.API.Models;
    using Rigor.HAR.API.Services;
    using System;

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
            services.AddDbContext<ApiDbContext>(opt => opt
                .UseInMemoryDatabase(ApiConstants.HarDB));
                //.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IHarFileRepository, HarFileRepository>();
            services.AddScoped<IHarFilesService, HarFilesService>();

            services.AddResponseCompression();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //var dbContext = serviceProvider.GetService<ApiDbContext>();
            //AddTestData(dbContext);

            app.UseResponseCompression();

            app.UseMvc();
        }

        private void AddTestData(ApiDbContext dbContext)
        {
            var test = new HarFile
            {
                HarFileId = 1,
                StartedDateTime = DateTime.Now,
                URL = "https://www.microsoft.com/net/",
                JSONString = null
            };

            dbContext.HarFiles.Add(test);
            dbContext.SaveChanges();
        }
    }
}
