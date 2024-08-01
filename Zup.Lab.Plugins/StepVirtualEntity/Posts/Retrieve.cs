using Microsoft.Xrm.Sdk;
using Zup.Lab.Plugins.Business;
using static Zup.Lab.Plugins.PluginBase;

namespace Zup.Lab.Plugins.StepVirtualEntity.Posts {
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
