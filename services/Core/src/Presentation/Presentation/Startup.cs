using System.Reflection;
using Application.Abstractions;
using Infrastructure.Mapper;
using Infrastructure.Persistence;
using Infrastructure.PreparationDb;
using Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presentation.Services;
using Serilog;
using static Application.Library.Queries.GetBookByIdQuery;

namespace Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithCorrelationIdHeader("x-correlation-id")
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<LibraryDbContext>(option =>
                option.UseInMemoryDatabase("InMemory"));

            services.AddGrpc();
            services.AddMapster();
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

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<LibraryService>();
            });

            PrepDb.Population(app);
        }
    }
}
