using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using OnData.PruebaTecnica.DASS.Models;

namespace OnData.PruebaTecnica.DASS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
  
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Contenido>("Contenidoes");
            builder.EntitySet<Categoria>("Categorias");
            builder.EntitySet<Etiqueta>("Etiquetas");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
