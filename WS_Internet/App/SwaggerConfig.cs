using System;
using System.Web.Http;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Collections.Generic;
using System.Configuration;
using WS_Internet.v0.Controllers.FilterAttributes;

namespace WS_Internet.App
{
    public class SwaggerConfig
    {
        private const string ASSEMBLY_NAME = "WS_Internet";

        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.PrettyPrint();
                    c.MultipleApiVersions(
                        (apiDesc, targetApiVersion) => apiDesc.RelativePath.Contains(targetApiVersion),
                        vc =>
                        {
                            vc.Version("v1", ASSEMBLY_NAME + " " + ConfigurationManager.AppSettings["APP_NAME"] + " - v1");
                        }
                    );
                    c.OperationFilter<AddRequiredHeaderParameter>();
                    c.IncludeXmlComments(String.Format(@"{0}\bin\\Resources\\Documentacion.XML", System.AppDomain.CurrentDomain.BaseDirectory));
                })
                .EnableSwaggerUi(c =>
                {
                    var thisAssembly = typeof(SwaggerConfig).Assembly;

                    c.DocumentTitle(ASSEMBLY_NAME + " " + ConfigurationManager.AppSettings["APP_NAME"]);
                    c.InjectStylesheet(thisAssembly, ASSEMBLY_NAME + ".Resources.Swagger.css");
                    c.InjectJavaScript(thisAssembly, ASSEMBLY_NAME + ".Resources.Swagger.js");
                    c.EnableDiscoveryUrlSelector();
                });
        }

        public class AddRequiredHeaderParameter : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                if (operation.parameters == null)
                {
                    operation.parameters = new List<Parameter>();
                }

                // Token y ControlAcceso
                var atributoToken = apiDescription.GetControllerAndActionAttributes<ConToken>().GetEnumerator();
                if (atributoToken != null && atributoToken.MoveNext() == true)
                {
                    AddHeaders(operation.parameters);
                }
            }

            private void AddHeaders(IList<Parameter> parametros)
            {
                parametros.Add(new Parameter
                {
                    name = "--Token",
                    @in = "header",
                    type = "string",
                    description = "Token",
                    required = true
                });
            }
        }
    }
}