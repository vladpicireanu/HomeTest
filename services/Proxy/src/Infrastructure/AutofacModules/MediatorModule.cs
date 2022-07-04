using Application.Behaviours;
using Application.Library.Queries;
using Application.StarterTasks.Queries;
using Autofac;
using MediatR;
using System.Reflection;
using Module = Autofac.Module;

namespace Infrastructure.AutofacModules
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
            builder
             .RegisterAssemblyTypes(typeof(GetUsersWithMostRentsQuery).Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterGeneric(typeof(ValidatorBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
