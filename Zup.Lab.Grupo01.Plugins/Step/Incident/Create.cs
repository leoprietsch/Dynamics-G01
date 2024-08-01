using Microsoft.Xrm.Sdk;
using static Zup.Lab.Grupo01.Plugins.PluginBase;

namespace Zup.Lab.Grupo01.Plugins.Step.Incident {
    public class Create {

        public void ExecuteCrmPlugin(LocalPluginContext localContext)
        {            
            Entity target = localContext.GetTargetEntity();

            if (!target.Contains("zup_cliente"))
                throw new InvalidPluginExecutionException("Não é permitido criar ocorrência sem informar o cliente");

        }
    }
}
