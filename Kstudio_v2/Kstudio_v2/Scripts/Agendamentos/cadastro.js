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
          debugger
            //Converte o valor recebido pelo controller em um objeto Json
            var jsondata = JSON.parse(data);

            //Pega cada um dos clientes e cria as options para gerar o autocomplete
            var options = '';
            jsondata.forEach(function (cliente) {
              debugger
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
            debugger
            var selecionado = $('#pesquisa').val();
          debugger
            options.forEach(function (opt) {
                debugger
                if (opt.value === selecionado) {
                    var idCliente = opt.getAttribute("data-id");
                    selectedOption = idCliente;
                }
            });
        }

        //Se alguma opção estiver selecionada, jopa o id do clienteescolhido no campo hidden que sera enviado comos dados ao salvar
        if (selectedOption != '') {
            document.getElementById("IdCliente").value = selectedOption;
        }

    }

};