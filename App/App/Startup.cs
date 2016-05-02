using App;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace App
{
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    using Autofac.Integration.WebApi;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = IocConfig.BuildContainer();
            var httpConf = ConfigureWebApi(new AutofacWebApiDependencyResolver(container));

            ConfigureAuth(app);
            app.UseAutofacWebApi(httpConf);
            app.UseWebApi(httpConf);     
        }

        private HttpConfiguration ConfigureWebApi(IDependencyResolver dependencyResolver)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] { new StringEnumConverter() }
            };

            var conf = new HttpConfiguration();

            conf.DependencyResolver = dependencyResolver;

            // remove xml formatter
            conf.Formatters.Remove(conf.Formatters.XmlFormatter);
            conf.Formatters.JsonFormatter.SerializerSettings = JsonConvert.DefaultSettings();

            conf.MapHttpAttributeRoutes();

            return conf;
        }
    }

    
}
