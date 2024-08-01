using Zup.Lab.Plugins.Business;
using static Zup.Lab.Plugins.PluginBase;

namespace Zup.Lab.Plugins.StepVirtualEntity.Posts {
    public class RetrieveMultiple {

        public void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            var outParam = localContext.PluginExecutionContext.OutputParameters;

            PostBusiness postBu = new PostBusiness();
            outParam["BusinessEntityCollection"] = postBu.getPosts();
        }
    }
}
