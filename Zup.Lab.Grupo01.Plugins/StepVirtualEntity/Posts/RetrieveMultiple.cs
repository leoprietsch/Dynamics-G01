using Zup.Lab.Grupo01.Plugins.Business;
using static Zup.Lab.Grupo01.Plugins.PluginBase;

namespace Zup.Lab.Grupo01.Plugins.StepVirtualEntity.Posts {
    public class RetrieveMultiple {

        public void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            var outParam = localContext.PluginExecutionContext.OutputParameters;

            PostBusiness postBu = new PostBusiness();
            outParam["BusinessEntityCollection"] = postBu.getPosts();
        }
    }
}
