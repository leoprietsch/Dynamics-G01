using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;

namespace Zup.Lab.Grupo01.Plugins {
    public abstract class PluginBase : IPlugin {

        public class LocalPluginContext {
            internal IServiceProvider ServiceProvider { get; private set; }
            internal IOrganizationService service { get; private set; }
            internal IOrganizationService serviceAdmin { get; private set; }
            internal IPluginExecutionContext PluginExecutionContext { get; private set; }
            internal IServiceEndpointNotificationService NotificationService { get; private set; }
            internal IOrganizationServiceFactory Factory { get; private set; }
            internal ITracingService TracingService { get; private set; }

            private LocalPluginContext() { }

            internal LocalPluginContext(IServiceProvider serviceProvider)
            {
                if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");

                ServiceProvider = serviceProvider;
                PluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                NotificationService = (IServiceEndpointNotificationService)serviceProvider.GetService(typeof(IServiceEndpointNotificationService));
                Factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                service = Factory.CreateOrganizationService(PluginExecutionContext.UserId);
                serviceAdmin = Factory.CreateOrganizationService(null);
            }


            internal void Trace(string message)
            {
                if (string.IsNullOrWhiteSpace(message) || TracingService == null) return;

                if (PluginExecutionContext == null)
                {
                    TracingService.Trace(message);
                    return;
                }

                TracingService.Trace("{0}, CorrelationId: {1}, InitiatingUser: {2}",
                       message, PluginExecutionContext.CorrelationId, PluginExecutionContext.InitiatingUserId);
            }

            public Entity GetTargetEntity()
            {
                return (Entity)PluginExecutionContext.InputParameters["Target"];
            }

            public EntityReference GetTargetEntityReference()
            {
                return (EntityReference)PluginExecutionContext.InputParameters["Target"];
            }


            public string MessageName()
            {
                return PluginExecutionContext.MessageName.ToLower();
            }

        }
        protected string ChildClassName { get; private set; }

        internal PluginBase(Type childClassName) { ChildClassName = childClassName.ToString(); }

        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");
            LocalPluginContext localContext = new LocalPluginContext(serviceProvider);

            localContext.Trace($"Entered {this.ChildClassName}.Execute()");
            try
            {
                ExecuteCrmPlugin(localContext);
            }
            catch (FaultException<OrganizationServiceFault> e)
            {
                localContext.Trace($"Exception: {e}");
                throw;
            }
            finally
            {
                localContext.Trace($"Exiting {this.ChildClassName}.Execute()");
            }
        }

        protected virtual void ExecuteCrmPlugin(LocalPluginContext localContext) { }
    }
}
