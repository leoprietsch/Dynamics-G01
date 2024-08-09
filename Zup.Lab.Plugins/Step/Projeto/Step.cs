using Microsoft.Xrm.Sdk;
using System;

namespace Zup.Lab.Plugins.Step.Projeto
{
    public class Step : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService) serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = factory.CreateOrganizationService(context.UserId);


            try
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                if (entity.LogicalName != "zup_projeto") return;
                int duracao;
                DateTime dataInicio;
                tracingService.Trace(context.MessageName);
                if (context.MessageName.ToLower() == "create")
                {
                    duracao = (int)entity["zup_duracao"];
                    dataInicio = (DateTime)entity["zup_datadeinicio"];
                    entity["zup_datadeterminoprevista"] = dataInicio.AddDays(duracao);
                }
                else
                {
                    var postImage = context.PostEntityImages["postImage"];

                    duracao = (int)postImage["zup_duracao"];
                    dataInicio = (DateTime)postImage["zup_datadeinicio"];
                    var update = new Entity(entity.LogicalName, entity.Id);
                    update["zup_datadeterminoprevista"] = dataInicio.AddDays(duracao);
                    service.Update(update);
                }

                
            }
            catch (Exception ex)
            {
                tracingService.Trace("Sample plugin: {0}", ex.ToString());
                throw;
            }
        }
    }
}
