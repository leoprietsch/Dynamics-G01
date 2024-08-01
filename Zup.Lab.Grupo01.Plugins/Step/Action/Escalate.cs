using DataverseModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Zup.Lab.Grupo01.Plugins.Step.Incident {
    public class Escalate : PluginBase {

        public Escalate() : base(typeof(Escalate)) { }

        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            var outParam = localContext.PluginExecutionContext.OutputParameters;

            var entity = localContext.PluginExecutionContext.InputParameters["Entity"] as string;
            var registerId = localContext.PluginExecutionContext.InputParameters["RegisterId"] as string;

            var register = localContext.serviceAdmin.Retrieve(entity, new Guid(registerId), new ColumnSet("zup_prioridade"));

            if (register == null)
                throw new InvalidPluginExecutionException("Registro não encontrado");

            if (!register.Contains("zup_prioridade"))
                throw new InvalidPluginExecutionException("Registro não possui prioridade definida");


            var actualPriority = register.GetAttributeValue<OptionSetValue>("zup_prioridade");

            var updateEntity = new Entity(entity, new Guid(registerId));

            if (actualPriority.Value == 0)
            {
                updateEntity["zup_prioridade"] = new OptionSetValue((int)zup_Prioridade.Medio);
                outParam["NewPriority"] = new OptionSetValue((int)zup_Prioridade.Medio);
            }

            if (actualPriority.Value == 1)
            {
                updateEntity["zup_prioridade"] = new OptionSetValue((int)zup_Prioridade.Urgente);
                outParam["NewPriority"] = new OptionSetValue((int)zup_Prioridade.Urgente);
            }

            if (actualPriority.Value == 2)
                throw new InvalidPluginExecutionException("Registro já é urgente");

            localContext.serviceAdmin.Update(updateEntity);
        }
    }
}
