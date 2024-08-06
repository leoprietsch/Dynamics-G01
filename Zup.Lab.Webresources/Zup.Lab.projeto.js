var Zup = Zup || {};
Zup.Lab = Zup.Lab || {};
Zup.Lab.projeto = Zup.Lab.projeto || {
    Onload: function (executionContext) {
        "use strict";

        var formContext = executionContext.getFormContext();
        
        var campoDuracao = formContext.getAttribute("zup_duracao");
        if (campoDuracao) {
            campoDuracao.addOnChange(Zup.Lab.projeto.OnChangeDuracao);
        }
    },

    OnChangeDuracao: function (executionContext) {
        "use strict";

        var formContext = executionContext.getFormContext();
        var duracao = formContext.getAttribute("zup_duracao").getValue();

        if (duracao == null || duracao <= 0) {
            formContext.ui.setFormNotification("A duração deve ser maior que zero.", "ERROR", "duracaoError");
        } else {
            formContext.ui.clearFormNotification("duracaoError");
        }
    },
    OnSave: function (executionContext) {
        "use strict";

        var formContext = executionContext.getFormContext();
        var duracao = formContext.getAttribute("zup_duracao").getValue();

        if (duracao == null || duracao <= 0) {
            formContext.ui.setFormNotification("Não foi possível salvar o formulário. Atente-se aos erros informados.", "ERROR", "saveFormError");
            executionContext.getEventArgs().preventDefault();
        } else {
            formContext.ui.clearFormNotification("saveFormError");
        }
    }


}