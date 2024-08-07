using DataverseModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Zup.Lab.Plugins.Step.Tarefa
{
    public class Delete : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService) serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = factory.CreateOrganizationService(context.UserId);


            try
            {
                EntityReference entityReference = (EntityReference)context.InputParameters["Target"];

                if (entityReference.LogicalName != "zup_tarefa") return;

                Guid lookupID = entityReference.Id;
                string logicalName = entityReference.LogicalName;

                var Tarefa = service.Retrieve(logicalName, lookupID, new ColumnSet("zup_status"));

                var status = ((OptionSetValue)Tarefa["zup_status"]).Value;

                if (status == 2) //finalizado
                    throw new InvalidPluginExecutionException("Não é possível deletar uma tarefa finalizada.");
            }
            catch (Exception ex)
            {
                tracingService.Trace("Sample plugin: {0}", ex.ToString());
                throw;
            }
        }
    }
}
