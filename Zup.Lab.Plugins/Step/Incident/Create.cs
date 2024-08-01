using Microsoft.Xrm.Sdk;
using static Zup.Lab.Plugins.PluginBase;

namespace Zup.Lab.Plugins.Step.Incident {
    public class Create {

        public void ExecuteCrmPlugin(LocalPluginContext localContext)
        {            
            Entity target = localContext.GetTargetEntity();

            if (!target.Contains("zup_cliente"))
                throw new InvalidPluginExecutionException("Não é permitido criar ocorrência sem informar o cliente");

        }
    }
}
