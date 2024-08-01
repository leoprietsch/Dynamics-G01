namespace Zup.Lab.Grupo01.Plugins.Step.Incident {
    public class IncidentStep : PluginBase {

        public IncidentStep() : base(typeof(IncidentStep)) { }

        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            switch (localContext.MessageName())
            {
                case "create":
                    new Create().ExecuteCrmPlugin(localContext);
                    break;
                default:
                    break;
            }
        }
    }
}
