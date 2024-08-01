using Microsoft.Xrm.Sdk;

namespace Zup.Lab.Grupo01.Plugins.StepVirtualEntity.Posts {
    public class PostStep : PluginBase {

        public PostStep() : base(typeof(PostStep)) { }

        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            switch (localContext.MessageName())
            {
                case "retrieve":
                    new Retrieve().ExecuteCrmPlugin(localContext);
                    break;
                case "retrievemultiple":
                    new RetrieveMultiple().ExecuteCrmPlugin(localContext);
                    break;
                default:
                    throw new InvalidPluginExecutionException("Step Não Implementado");
            }
        }
    }
}
