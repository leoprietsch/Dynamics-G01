var Zup = Zup || {};
Zup.Lab.Grupo01 = Zup.Lab.Grupo01 || {};
Zup.Lab.Grupo01.LabIncident = Zup.Lab.Grupo01.LabIncident || {
    Onload: function (executionContext) {
        "use strict";
        var formContext = executionContext.getFormContext();

        formContext.getAttribute("zup_cliente").setRequiredLevel("required");

        formContext.getAttribute("zup_categoriaid").addOnChange(Zup.Lab.Grupo01.LabIncident.OnChangeCategory);
        formContext.getAttribute("zup_subcategoriaid").addOnChange(Zup.Lab.Grupo01.LabIncident.OnChangeSub);
    },

    OnChangeCategory: function (executionContext) {
        "use strict";
        var formContext = executionContext.getFormContext();

        var classification = formContext.getAttribute("zup_classificacaoid").getValue();
        var category = formContext.getAttribute("zup_categoriaid").getValue();
        var subcategory = formContext.getAttribute("zup_subcategoriaid").getValue();
        if (subcategory === null && category !== null && classification === null) {
            var catId = category[0].id;
            Xrm.WebApi.retrieveRecord("zup_categoria", catId, "?$select=_zup_classificacaoid_value").then(
                (sucess) => {
                    var classificationId = sucess._zup_classificacaoid_value;
                    var classificationName = sucess["_zup_classificacaoid_value@OData.Community.Display.V1.FormattedValue"];
                    var classificationEntityType = sucess["_zup_classificacaoid_value@Microsoft.Dynamics.CRM.lookuplogicalname"];
                    Zup.Lab.Grupo01.LabIncident.SetLookupValue(formContext, "zup_classificacaoid", classificationId, classificationName, classificationEntityType);
                }
            )
        }
    },

    OnChangeSub: function (executionContext) {
        "use strict";
        var formContext = executionContext.getFormContext();

        var classification = formContext.getAttribute("zup_classificacaoid").getValue();
        var category = formContext.getAttribute("zup_categoriaid").getValue();
        var subcategory = formContext.getAttribute("zup_subcategoriaid").getValue();

        if (subcategory !== null && category === null && classification === null) {
            var subId = subcategory[0].id;
            Xrm.WebApi.retrieveRecord("zup_subcategoria", subId, "?$select=_zup_classificacaoid_value,_zup_categoriaid_value").then(
                (sucess) => {
                    var classificationId = sucess._zup_classificacaoid_value;
                    var classificationName = sucess["_zup_classificacaoid_value@OData.Community.Display.V1.FormattedValue"];
                    var classificationEntityType = sucess["_zup_classificacaoid_value@Microsoft.Dynamics.CRM.lookuplogicalname"];
                    Zup.Lab.Grupo01.LabIncident.SetLookupValue(formContext, "zup_classificacaoid", classificationId, classificationName, classificationEntityType);

                    var categoryId = sucess._zup_categoriaid_value;
                    var categoryName = sucess["_zup_categoriaid_value@OData.Community.Display.V1.FormattedValue"];
                    var categoryEntityType = sucess["_zup_categoriaid_value@Microsoft.Dynamics.CRM.lookuplogicalname"];
                    Zup.Lab.Grupo01.LabIncident.SetLookupValue(formContext, "zup_categoriaid", categoryId, categoryName, categoryEntityType);
                }
            )
        }
    },

    Escalate: function (formContext) {
        "use strict"

        var execute_zup_ActionAumentarPrioridade_Request = {
            Entity: "zup_lab_ocorrencia",
            RegisterId: formContext.data.entity.getId(),

            getMetadata: function () {
                return {
                    boundParameter: null,
                    parameterTypes: {
                        Entity: { typeName: "Edm.String", structuralProperty: 1 },
                        RegisterId: { typeName: "Edm.String", structuralProperty: 1 }
                    },
                    operationType: 0, operationName: "zup_ActionAumentarPrioridade"
                };
            }
        };

        Xrm.WebApi.execute(execute_zup_ActionAumentarPrioridade_Request).then(
            function success(response) {
                if (response.ok) { return response.json(); }
            }
        ).then(function (responseBody) {
            var result = responseBody;
            var newpriority = result["NewPriority"];
            alert("Nova Prioridade: " + newpriority);
            formContext.data.refresh();
        }).catch(function (error) {
            alert("Erro: " + error.message);
        });
    },

    SetLookupValue: function (formContext, fieldName, id, name, entityType) {
        "use strict";
        var lookup = [{ id: id, name: name, entityType: entityType }];
        formContext.getAttribute(fieldName).setValue(lookup);
    }
}