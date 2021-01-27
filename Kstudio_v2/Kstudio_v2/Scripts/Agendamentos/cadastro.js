window.onload = function () {

    //Devinindo um evento que é disparado toda vez que o usuario digita alguma letra nova no campo de pesquisa
    document.getElementById("pesquisa").onkeyup = pesquisaHandler;


    function pesquisaHandler() {

        //Obtem o texto digitado no campo de pesquisa
        var value = document.getElementById("pesquisa").value;

        //Faz uma requisição Ajax de GET no controller na action BuscarClientesAutocomplete passando o valor digitado no campo como parametro
        var request = $.ajax({
            url: 'BuscarClientesAutocomplete',
            type: 'GET',
            data: { value },
            contentType: 'application/json; charset=utf-8'
        });

        //Recebe a resposta da requisição Ajax
        request.done(function (data) {

            //Converte o valor recebido pelo controller em um objeto Json
            var jsondata = JSON.parse(data);

            //Pega cada um dos clientes e cria as options para gerar o autocomplete
            var options = '';
            jsondata.forEach(function (cliente) {

                var sugestion = cliente.Id + '-' + cliente.Banda + '-' + cliente.Responsavel;
                options += '<option data-id="' + cliente.Id + '"> ' + sugestion + ' </option>';
            });

            //adiciona as opções na tela
            document.getElementById("autocompleteCliente").innerHTML = options;
        });


        //Verifica se alguma opção está selecionada
        var selectedOption = '';
        var options = document.querySelectorAll("option");
        if (options.length > 0) {

            var selecionado = $('#pesquisa').val();

            options.forEach(function (opt) {

                if (opt.value === selecionado) {
                    var idCliente = opt.getAttribute("data-id");
                    selectedOption = idCliente;
                }
            });
        }

        //Se alguma opção estiver selecionada, jopa o id do clienteescolhido no campo hidden que sera enviado comos dados ao salvar
        if (selectedOption != '') {
            document.getElementById("Id").value = selectedOption;
        }

    }

};

function addFieldsAgendamento() {

   
    var detalhes = document.getElementById("agendamentosDoCliente").getElementsByTagName('tbody')[0];
    var index = document.getElementById("agendamentosDoCliente").getElementsByTagName('tr').length - 1;
    var detalhe = document.createElement("tr");
    detalhe.setAttribute("id", "detalhe_" + index);
    var templateData = document.getElementById("modeloData");
    var templateHorario = document.getElementById("modeloHorarioInicio");
    var templateAte = document.getElementById("modeloHorarioFim");
    //var index = document.getElementById("modeloData").querySelectorAll('div[name=detalhe]').length + 1;
   

    var data = templateData.cloneNode(true);   // clona a div abaixo:
    
    data.className = "form-group";  //<div id="template" class="form-group hide" >
    //data.querySelector("label").innerHTML = "Data";  //<label class="control-label col-md-2">Descrição</label>
    //data.querySelector("label").setAttribute("for", "AgendamentosViewModel_" + index + "__Data"); // <label class="control-label col-md-2" for="Detalhes_[index]__Descricao">Descrição</label>
    data.querySelector("input").setAttribute("name", "AgendamentosViewModel[" + index + "].Data"); // <input class="form-control text-box single-line" type="text" value="" name="Detalhes[index].Descricao">
    data.querySelector("input").id = "AgendamentosViewModel_" + index + "__Data"; // <input class="form-control text-box single-line" type="text" value="" name="Detalhes[index].Descricao" id="Detalhes_1__Descricao">

    var horario = templateHorario.cloneNode(true);
    horario.className = "form-group";
    //horario.querySelector("label").innerHTML = "Horário";
    //horario.querySelector("label").setAttribute("for", "AgendamentosViewModel_" + index + "__HorarioInicio");
    horario.querySelector("input").setAttribute("name", "AgendamentosViewModel[" + index + "].HorarioInicio");
    horario.querySelector("input").id = "AgendamentosViewModel_" + index + "__HorarioInicio";

    var ate = templateAte.cloneNode(true);
    ate.className = "form-group";
    //ate.querySelector("label").innerHTML = "Até";
    //ate.querySelector("label").setAttribute("for", "AgendamentosViewModel_" + index + "__HorarioFinal");
    ate.querySelector("input").setAttribute("name", "AgendamentosViewModel[" + index + "].HorarioFinal");
    ate.querySelector("input").id = "AgendamentosViewModel_" + index + "__HorarioFinal";

    detalhe.appendChild(data);
    detalhe.appendChild(horario);
    detalhe.appendChild(ate);
    detalhes.appendChild(detalhe);
}

function removeFieldsAgendamento() {

    var quantidadeDeTr = document.getElementById("agendamentosDoCliente").getElementsByTagName('tr').length;

    if (quantidadeDeTr < 3) {
        alert("Não é possivel excluir este campo!!");
    }

    else if (quantidadeDeTr > 3) {
        var detalhe = document.getElementById("detalhe_1");
        detalhe.nextElementSibling.remove();
    }

    else if (quantidadeDeTr == 3)
    {
        var detalhe = document.getElementById("detalhe_1");
        detalhe.remove();
    }
    
     

}

function buscarAgendamentos() {

    var value = document.getElementById("pesquisa").value;

    var request = $.ajax({
        url: 'BuscarAgendamentosAutocomplete',
        type: 'GET',
        data: { value },
        contentType: 'application/json; charset=utf-8'
    });

    request.done(function (data) {

        //Converte o valor recebido pelo controller em um objeto Json
        var jsondata = JSON.parse(data);
        var agenda = '';
        var id = '';
        var agendamentoId = "";
        var banda = '';
        var responsavel = '';
        var dataAgendamento = '';
        var horarioInicio = '';
        var horarioFinal = '';
        var tableId = document.getElementById("agendamentosDoCliente");
        var trId = '';
        var tdId = '';
        var tdCreate = '';
        var tableLabelId = document.getElementById("tablelabel");
        var copy = tableLabelId.cloneNode(true);
        tableId.innerHTML = copy.outerHTML;


        jsondata.forEach(function (cliente) {

            for (var i = 0; i < cliente.Agendamentos.length; i++) {

                id = cliente.Id;
                agendamentoId = cliente.Agendamentos[i].Id;
                banda = cliente.Banda;
                responsavel = cliente.Responsavel;
                dataAgendamento = cliente.Agendamentos[i].Data;
                horarioInicio = cliente.Agendamentos[i].HorarioInicio;
                horarioFinal = cliente.Agendamentos[i].HorarioFinal;

                var trCreate = document.createElement("tr");
                trCreate.setAttribute("id", "tr" + i);
                tableId.appendChild(trCreate);

                tdCreate = document.createElement("td");


                trId = document.getElementById(trCreate.id); // getElementById

                trId.appendChild(tdCreate);
                agenda = id;
                tdCreate.appendChild(document.createTextNode(agenda));

                tdCreate = document.createElement("td");

                trId.appendChild(tdCreate);
                agenda = banda;
                tdCreate.appendChild(document.createTextNode(agenda));

                tdCreate = document.createElement("td");

                trId.appendChild(tdCreate);
                agenda = responsavel;
                tdCreate.appendChild(document.createTextNode(agenda));

                tdCreate = document.createElement("td");

                trId.appendChild(tdCreate);
                agenda = dataAgendamento;
                tdCreate.appendChild(document.createTextNode(agenda));

                tdCreate = document.createElement("td");

                trId.appendChild(tdCreate);
                agenda = horarioInicio;
                tdCreate.appendChild(document.createTextNode(agenda));

                tdCreate = document.createElement("td");

                trId.appendChild(tdCreate);
                agenda = horarioFinal;
                tdCreate.appendChild(document.createTextNode(agenda));

                tdCreate = document.createElement("td");
                tdCreate.setAttribute("id", "td" + i);
                trId.appendChild(tdCreate);

                aCreate = document.createElement("a");
                aCreate.setAttribute("href", "/Agendamentos/Editar/" + agendamentoId);
                tdId = document.getElementById(tdCreate.id);
                tdId.appendChild(aCreate);
                agenda = "Editar";
                aCreate.appendChild(document.createTextNode(agenda));

                aCreate = document.createElement("a");
                aCreate.setAttribute("href", "/Agendamentos/Detalhes/" + agendamentoId);
                tdId = document.getElementById(tdCreate.id);
                tdId.appendChild(aCreate);
                agenda = " Detalhes";
                aCreate.appendChild(document.createTextNode(agenda));

                aCreate = document.createElement("a");
                aCreate.setAttribute("href", "/Agendamentos/Deletar/" + agendamentoId);
                tdId = document.getElementById(tdCreate.id);
                tdId.appendChild(aCreate);
                agenda = " Deletar";
                aCreate.appendChild(document.createTextNode(agenda));

            }
           
        });
 
    });
}

function teste() {

    var id = document.getElementById("agendamentosDoCliente");
    var tableLabelId = document.getElementById("tablelabel");
    var tbody = document.getElementsByTagName("tbody")[0];
    var copy = tableLabelId.cloneNode(true);
    var divcreate = document.createElement("div");
    id.innerHTML = copy.outerHTML;
    id.appendChild(divcreate);
   
    tbody.appendChild(divcreate);


}
