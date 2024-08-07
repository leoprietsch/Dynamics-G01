using DataverseModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Zup.Lab.Plugins.Step.Projeto
{
    public class ValidaFinalizacao : IPlugin
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

                var postImage = context.PostEntityImages["postImageStatus"];
                var novoStatus = ((OptionSetValue)postImage["zup_status"]).Value;

                if (novoStatus != 2)
                    return;

                var idProjeto = entity.Id;
                List<ZUp_TARefA> listaTarefas;

                using (var contextDataVerse = new DataverseContext(service))
                {
                    listaTarefas = contextDataVerse.ZUp_TARefASet
                        .Where(x => x.ZUp_ProjetO.Id == idProjeto).ToList();                    
                }

                if (!listaTarefas.Any()) 
                {
                    throw new InvalidPluginExecutionException("O projeto não pode ser concluido sem tarefas.");
                }
                else if (listaTarefas.Any( x => x.ZUp_Status != ZUp_TARefA_ZUp_Status.Finalizado))
                {
                    throw new InvalidPluginExecutionException("O projeto não pode ser concluido com tarefas não finalizadas.");
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
