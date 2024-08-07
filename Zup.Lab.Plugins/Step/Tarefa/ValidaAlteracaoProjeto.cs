using DataverseModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Zup.Lab.Plugins.Step.Tarefa
{
    public class ValidaAlteracaoProjeto : IPlugin
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

                if (entity.Contains("zup_projeto"))
                    throw new InvalidPluginExecutionException("Não é possível mudar a tarefa de projeto.");
            }
            catch (Exception ex)
            {
                tracingService.Trace("Sample plugin: {0}", ex.ToString());
                throw;
            }
        }
    }
}
