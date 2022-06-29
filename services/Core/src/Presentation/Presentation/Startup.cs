using System.Reflection;
using Application.Abstractions;
using Infrastructure.Persistence;
using Infrastructure.PreparationDb;
using Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presentation.Services;
using static Application.Library.Queries.GetBookByIdQuery;

namespace Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<LibraryDbContext>(option =>
                option.UseInMemoryDatabase("InMemory"));

            services.AddGrpc();
            services.AddMediatR(typeof(GetBookByIdQueryHandler).GetTypeInfo().Assembly);
            services.AddScoped<ILibraryRepository, LibraryRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<LibraryService>();
            });

            PrepDb.Population(app);
        }
    }
}
