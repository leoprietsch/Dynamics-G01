using DataverseModel;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;
using Zup.Lab.Tests.Util;

namespace Zup.Lab.Tests.Tests {
    [TestClass]
    public class Example : CrmConnection {

        [TestMethod]
        public void T001_CheckConnection()
        {
            WhoAmIRequest request = new WhoAmIRequest();
            var me = (WhoAmIResponse)service.Execute(request);

            Assert.IsTrue(me.UserId != null);
        }

        [TestMethod]
        public void T003_QueryExpression()
        {
            QueryExpression qe = new QueryExpression(zup_lab_ocorrencia.EntityLogicalName);
            qe.Criteria = new FilterExpression();
            qe.Criteria.AddCondition("statuscode", ConditionOperator.Equal, (int)zup_lab_ocorrencia_StatusCode.Pendente);
            qe.ColumnSet = new ColumnSet("zup_cliente", "zup_subcategoriaid");
            var ocorrencias = service.RetrieveMultiple(qe);

            Assert.IsTrue(ocorrencias.Entities.Count() > 0);
        }

        [TestMethod]
        public void T004_FetchExpression()
        {
            var fetch = @"<fetch version=""1.0"" mapping=""logical"" no-lock=""false"" distinct=""true"">
                <entity name=""zup_lab_ocorrencia"">
                    <attribute name=""zup_lab_ocorrenciaid""/>
                    <attribute name=""zup_subcategoriaid""/>
                    <attribute name=""zup_cliente""/>
                    <attribute name=""createdon""/>
                    <order attribute=""createdon"" descending=""false""/>
                    <filter type=""and"">
                        <condition attribute=""statuscode"" operator=""eq"" value=""1""/>
                    </filter>
                    </entity>
                </fetch>";
            var ocorrencias = service.RetrieveMultiple(new FetchExpression(fetch));

            Assert.IsTrue(ocorrencias.Entities.Count() > 0);
        }
    }
}
