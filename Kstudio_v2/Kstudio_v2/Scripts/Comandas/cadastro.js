window.onload = function () {

    //Devinindo um evento que é disparado toda vez que o usuario digita alguma letra nova no campo de pesquisa
    document.getElementById("pesquisa").onfocus = pesquisaHandler;

    function pesquisaHandler() {



        //Obtem o texto digitado no campo de pesquisa
        var value = document.getElementById("pesquisa").value;

        //Faz uma requisição Ajax de GET no controller na action BuscarClientesAutocomplete passando o valor digitado no campo como parametro
        var request = $.ajax({
            url: '/Agendamentos/BuscarAgendamentosDiariosAutocomplete',
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

function addFieldsProdutos() {

    var detalhes = document.getElementById("tabelaProdutos").getElementsByTagName('tbody')[0];
    var index = document.getElementById("tabelaProdutos").getElementsByTagName('tr').length;
    var modeloCloneTr = document.getElementById("modeloTr");
    var modeloTr = modeloCloneTr.cloneNode(true);

    modeloTr.setAttribute("id", "tr" + index);
    modeloTr.children[0].children[0].children[1].children.pesquisaProduto_0.id = "pesquisaProduto_" + index;
    modeloTr.children[1].children[0].children[1].children.Produto_Quantidade_0.id = "Produto_Quantidade_" + index;
    modeloTr.children[2].children[0].children[1].children.Produto_Preco_0.id = "Produto_Preco_" + index;
    modeloTr.children[0].children[0].children[1].children[0].name = "Produto[" + index + "].Nome";
    modeloTr.children[1].children[0].children[1].children[0].name = "Produto[" + index + "].Quantidade";
    modeloTr.children[2].children[0].children[1].children[0].name = "Produto[" + index + "].Preco";

    detalhes.appendChild(modeloTr);


}

function removeFieldsAgendamento() {

    var quantidadeDeTr = document.getElementById("agendamentosDoCliente").getElementsByTagName('tr').length;

    if (quantidadeDeTr < 3) {
        alert("Não é possivel excluir este campo!!");
    } else {

        var detalhe = document.getElementById("trIdTable");
        detalhe.nextElementSibling.remove();

    }

}

function buscarAgendamentosData() {

    var value = document.getElementById("pesquisadata").value;

    var request = $.ajax({
        url: 'BuscarAgendamentosPorData',
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

function buscarProdutos() {
    var value = document.getElementById("pesquisaProduto").value;

    var request = $.ajax({
        url: 'BuscarProdutoAutocomplete',
        type: 'GET',
        data: { value },
        contentType: 'application/json; charset=utf-8'
    });
}

function preencheCamposDeAgendamento() {

    var value = document.getElementById("pesquisa").value;

    if (value != "") {
        var request = $.ajax({
            url: '/Agendamentos/BuscarAgendamentosAutocomplete',
            type: 'GET',
            data: { value },
            contentType: 'application/json; charset=utf-8'
        });

        request.done(function (data) {

            var jsondata = JSON.parse(data);
            var dateToday = new Date("2021-04-29");
            var dateTodayConvertida = "2021-4-28";//dateToday.getFullYear() + "-" + (dateToday.getMonth() + 1) + "-" + dateToday.getDate();
            var date = dateTodayConvertida;
            var dataConvertida;
            var horasInicio;
            var horaInicioConvertida;
            var horasFinal;
            var horaFinalConvertida;
            var diff;
            var horaTotalDeEnsaio;

            for (var i = 0; i < jsondata[0].Agendamentos.length; i++) {

                var dataAge = new Date(jsondata[0].Agendamentos[i].Data);
                var dataAgeConv = dataAge.getFullYear() + "-" + (dataAge.getMonth() + 1) + "-" + dataAge.getDate();

                if (dateTodayConvertida == dataAgeConv) {
                    date = new Date(jsondata[0].Agendamentos[i].Data);
                    dataConvertida = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();

                    horasInicio = new Date(jsondata[0].Agendamentos[i].HorarioInicio);
                    horaInicioConvertida = horasInicio.toLocaleTimeString();

                    horasFinal = new Date(jsondata[0].Agendamentos[i].HorarioFinal);
                    horaFinalConvertida = horasFinal.toLocaleTimeString();

                    diff = horasFinal.getTime() - horasInicio.getTime();
                    horaTotalDeEnsaio = diff / (1000 * 60 * 60) % 24;
                }
            }

            document.getElementById("Data").value = dataConvertida;
            document.getElementById("HoraDeInicio").value = horaInicioConvertida;
            document.getElementById("HoraFinal").value = horaFinalConvertida;
            document.getElementById("HorasDeEnsaio").value = horaTotalDeEnsaio;

        })
    }
}

function pesquisaProdutoHandler() {

    //Obtem o id do campo clicado
    var e = e || window.event;
    e = e.target || e.srcElement;
    var idElement = e.id;

    //Obtem o texto digitado no campo de pesquisa
    var value = document.getElementById(idElement).value;

    //Faz uma requisição Ajax de GET no controller na action BuscarClientesAutocomplete passando o valor digitado no campo como parametro
    var request = $.ajax({
        url: '/Produtos/BuscarProdutosAutocomplete',
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
        jsondata.forEach(function (produto) {

            var sugestion = produto.Nome;
            options += '<option data-id="' + produto.Id + '"> ' + sugestion + ' </option>';
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

function valorCalculadoDaComanda() {

    var valorHoras = parseFloat(document.getElementById("ValorDeHoras").value);
    var contagemDeProdutos = document.getElementById("tabelaProdutos").rows.length;
    var horasDeEnsaio = parseFloat(document.getElementById("HorasDeEnsaio").value)
    var total = valorHoras * horasDeEnsaio;
    var quantidade;
    var preco;
    var valorTotalId = document.getElementById("valorTotalId");
    //var statusAberto = document.getElementById("statusaberto").checked;
    var statusPago = document.getElementById("statusfechado").checked;

    for (var i = 0; i < contagemDeProdutos; i++) {

        quantidade = document.getElementById("Produto_Quantidade_" + i).value;
        preco = document.getElementById("Produto_Preco_" + i).value;

        total = total + quantidade * preco;

    }

    if (statusPago) {
        document.getElementById("StatusComanda").value = true;
    } else {
        document.getElementById("StatusComanda").value = false;
    }

    document.getElementById("ValorTotalDaComanda").value = total;

    valorTotalId.innerHTML = "Valor Total = R$" + total;
}