using DataverseModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Zup.Lab.Plugins.Step.Tarefa
{
    public class Validacao : IPlugin
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

                if (entity.LogicalName != "zup_tarefa") return;

                var postImage = context.PostEntityImages["postImageTarefa"];
                var novoStatus = ((OptionSetValue)postImage["zup_status"]).Value;

                if (novoStatus != 1) //em andamento 
                    return;

                EntityReference lookupFieldProjeto = (EntityReference)postImage["zup_projeto"];
                Guid lookupID = lookupFieldProjeto.Id;
                string logicalName = lookupFieldProjeto.LogicalName;

                var Projeto = service.Retrieve(logicalName, lookupID, new ColumnSet("zup_status"));
                var statusProjeto = ((OptionSetValue)Projeto["zup_status"]).Value;

                if (statusProjeto == 0) //aguardando inicio
                {
                    throw new InvalidPluginExecutionException("A tarefa não pode ser colocada em andamento, se o projeto não estiver iniciado.");
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
