window.onload = function () {

    //Devinindo um evento que é disparado toda vez que o usuario digita alguma letra nova no campo de pesquisa
    document.getElementById("pesquisa").onkeyup = pesquisaHandler;


    function pesquisaHandler() {

        //Obtem o texto digitado no campo de pesquisa
        var value = document.getElementById("pesquisa").value;

        //Faz uma requisição Ajax de GET no controller na action GetClientesForAutocomplete passando o valor digitado no campo como parametro
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
             
                var sugestion = cliente.Banda + ' - ' + cliente.Responsavel;
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
            document.getElementById("Cliente_Id").value = selectedOption;
        }

    }

};

function addFieldsAgendamento() {

    var detalhe = document.createElement("<div>");
    detalhe.setAttribute("name", "detalhe");
    var detalhes = document.getElementById("detalhes");
    var template = document.getElementById("template");
    var index = document.getElementById("detalhes").querySelectorAll('div[name=detalhe]').length;

    var data = template.cloneNode(true);   // clona a div abaixo:
    //<div id="template" class="form-group hide" disa="">
    //    <label class="control-label col-md-2"></label>
    //    <div class="col-md-10">
    //        <input class="form-control text-box single-line" type="text" value="">
    //    </div>
    //</div>
    data.className = "form-group";  //<div id="template" class="form-group hide" >
    data.querySelector("label").innerHTML = "Data";  //<label class="control-label col-md-2">Descrição</label>
    data.querySelector("label").setAttribute("for", "Detalhes_" + index + "__Data"); // <label class="control-label col-md-2" for="Detalhes_[index]__Descricao">Descrição</label>
    data.querySelector("input").setAttribute("name", "Detalhes[" + index + "].Data"); // <input class="form-control text-box single-line" type="text" value="" name="Detalhes[index].Descricao">
    data.querySelector("input").id = "Detalhes_" + index + "__Data"; // <input class="form-control text-box single-line" type="text" value="" name="Detalhes[index].Descricao" id="Detalhes_1__Descricao">

    var horario = template.cloneNode(true);
    horario.className = "form-group";
    horario.querySelector("label").innerHTML = "Horário";
    horario.querySelector("label").setAttribute("for", "Detalhes_" + index + "__Horario");
    horario.querySelector("input").setAttribute("name", "Detalhes[" + index + "].Horario"); 
    horario.querySelector("input").id = "Detalhes_" + index + "__horario";

    var ate = template.cloneNode(true);
    ate.className = "form-group";
    ate.querySelector("label").innerHTML = "Até";
    ate.querySelector("label").setAttribute("for", "Detalhes_" + index + "__Ate");
    ate.querySelector("input").setAttribute("name", "Detalhes[" + index + "].Ate");
    ate.querySelector("input").id = "Detalhes_" + index + "__Ate";

    detalhe.appendChild(data);
    detalhe.appendChild(horario);
    detalhe.appendChild(ate);
    detalhes.appendChild(detalhe);
}