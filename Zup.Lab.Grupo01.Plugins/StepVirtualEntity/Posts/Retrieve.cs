using Microsoft.Xrm.Sdk;
using Zup.Lab.Grupo01.Plugins.Business;
using static Zup.Lab.Grupo01.Plugins.PluginBase;

namespace Zup.Lab.Grupo01.Plugins.StepVirtualEntity.Posts {
    public class Retrieve {

        public void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            var outParam = localContext.PluginExecutionContext.OutputParameters;

            EntityReference target = localContext.GetTargetEntityReference();

            PostBusiness postBu = new PostBusiness();
            outParam["BusinessEntity"] = postBu.getPostById(target.Id);
        }
    }
}
