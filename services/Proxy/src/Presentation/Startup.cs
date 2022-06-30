using Application.Abstractions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.AutofacModules;
using Infrastructure.gRPC;
using Infrastructure.Mapper;
using MediatR;
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
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule());
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen();

            services.AddControllers();

            services.AddSingleton<ICoreLibraryGrpcClient, CoreLibraryGrpcClient>();
            services.AddAutofac();
            services.AddMapster();
            services.AddHealthChecks();
            services.AddHttpContextAccessor();
            var builder = new ContainerBuilder();

            ConfigureContainer(builder);
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/env", async context =>
                {
                    string isEnv = $"IsDevelopment:{env.IsDevelopment()}, IsStaging:{env.IsStaging()}, IsProduction:{env.IsProduction()}";

                    await context.Response.WriteAsync($"Proxy service is running. EnvironmentName={env.EnvironmentName}.{isEnv}.");
                });
                endpoints.MapHealthChecks("/health");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // "swagger.json" is hardcoded (will not work with any other json filename)
                c.SwaggerEndpoint(
                   String.Format("/swagger/{0}/swagger.json", apiVersion),
                   String.Format("{0} {1}", apiName, apiVersion));
                c.RoutePrefix = String.Empty;
            });

        }
    }
}
