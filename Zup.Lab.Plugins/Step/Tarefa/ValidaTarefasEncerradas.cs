using DataverseModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Zup.Lab.Plugins.Step.Tarefa
{
    public class ValidaTarefasEncerradas : IPlugin
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

                if (entity.LogicalName != "zup_tarefa" || !entity.Contains("zup_status")) return;

                if (((OptionSetValue) entity["zup_status"]).Value != 2) return; // Finalizado

                var postImage = context.PostEntityImages["postImageFinalizaTarefa"];

                EntityReference lookupFieldProjeto = (EntityReference)postImage["zup_projeto"];

                List<ZUp_TARefA> listaTarefas;
                using (var contextDataVerse = new DataverseContext(service))
                {
                    listaTarefas = contextDataVerse.ZUp_TARefASet
                        .Where(x => x.ZUp_ProjetO.Id == lookupFieldProjeto.Id).ToList();
                }

                if (listaTarefas.Any(x => ((OptionSetValue) x["zup_status"]).Value != 2)) return; // Finalizado

                var updateProjeto = service.Retrieve(lookupFieldProjeto.LogicalName, lookupFieldProjeto.Id, new ColumnSet("zup_status"));
                ((OptionSetValue)updateProjeto["zup_status"]).Value = 2; // Finalizado
                service.Update(updateProjeto);

            }
            catch (Exception ex)
            {
                tracingService.Trace("Sample plugin: {0}", ex.ToString());
                throw;
            }
        }
    }
}
