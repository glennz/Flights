namespace App
{
    using App.Config;
    using App.Controllers;
    using App.Services;

    using Autofac;
    using Autofac.Integration.WebApi;

    using AutoMapper;
    using AutoMapper.Mappers;

    using FlightManager;

    public static class IocConfig
    {
        public static IContainer BuildContainer()
        {
            var b = new ContainerBuilder();
            

            var thisAssembly = typeof(FlightController).Assembly;
            b.RegisterApiControllers(thisAssembly);

            //repo
            b.RegisterType<Repo>().As<IRepo>();

            //service
            b.RegisterType<FlightService>().As<IFlightService>();

            // Automapper
            b.Register(c => new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers)).SingleInstance().OnActivated(a => MappingRelationship.Create(a.Instance));
            b.Register(c => new MappingEngine(c.Resolve<ConfigurationStore>())).As<IMappingEngine>();

            return b.Build();
        }
    }
}