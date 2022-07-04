using Application.Abstractions;
using Application.Library.Queries;
using Application.StarterTasks.Queries;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.AutofacModules;
using Infrastructure.gRPC;
using Infrastructure.Mapper;
using MediatR;
using Presentation.ActionFilters;
using Serilog;
using System.Reflection;

namespace Presentation
{
    public class Startup
    {
        public const string apiVersion = "v1";
        public const string apiName = "Proxy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithCorrelationIdHeader("x-correlation-id")
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen();

            services.AddControllers(
                  c =>
                  {
                      c.Filters.Add(typeof(ApiExceptionFilter));
                  })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetUsersWithMostRentsQuery>());
            //services.AddValidatorsFromAssemblyContaining<GetUsersWithMostRentsQuery>();
            services.AddSingleton<ICoreLibraryGrpcClient, CoreLibraryGrpcClient>();
            services.AddAutofac();
            services.AddMapster();
            services.AddHealthChecks();
            services.AddHttpContextAccessor();
            var builder = new ContainerBuilder();

            ConfigureContainer(builder);
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSerilogRequestLogging();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                   String.Format("/swagger/{0}/swagger.json", apiVersion),
                   String.Format("{0} {1}", apiName, apiVersion));
                c.RoutePrefix = String.Empty;
            });

        }
    }
}
