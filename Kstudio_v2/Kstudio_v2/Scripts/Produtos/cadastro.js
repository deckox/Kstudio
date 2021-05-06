window.onload = function () {

    //Devinindo um evento que é disparado toda vez que o usuario digita alguma letra nova no campo de pesquisa
    document.getElementById("pesquisaProdutos").onkeyup = pesquisaHandler;


    function pesquisaHandler() {

        //Obtem o texto digitado no campo de pesquisa
        var value = document.getElementById("pesquisaProdutos").value;

        //Faz uma requisição Ajax de GET no controller na action BuscarClientesAutocomplete passando o valor digitado no campo como parametro
        var request = $.ajax({
            url: 'BuscarProdutosAutocomplete',
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

                var sugestion = produto.Id + '-' + produto.Nome;
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
                    var idProduto = opt.getAttribute("data-id");
                    selectedOption = idProduto;
                }
            });
        }

        //Se alguma opção estiver selecionada, jopa o id do clienteescolhido no campo hidden que sera enviado comos dados ao salvar
        if (selectedOption != '') {
            document.getElementById("Id").value = selectedOption;
        }

    }

};

function buscarProdutos() {

    var value = document.getElementById("pesquisaProdutos").value;

    var request = $.ajax({
        url: 'BuscarProdutos',
        type: 'GET',
        data: { value },
        contentType: 'application/json; charset=utf-8'
    });

    request.done(function (data) {

        //Converte o valor recebido pelo controller em um objeto Json
        var jsondata = JSON.parse(data);
        var id = 0;
        var nome = "";
        var precoDeCusto = '';
        var precoDeVenda = '';
        var preco = '';
        var tableId = document.getElementById("tabelaInformacaoDoProduto");
        var tableLabelId = document.getElementById("tableLabel");
        var copy = tableLabelId.cloneNode(true);
        tableId.innerHTML = copy.outerHTML;
        var tbody = document.getElementsByTagName("tbody")[0];

        id = jsondata.Id;
        nome = jsondata.Nome;
        precoDeCusto = jsondata.PrecoDeCusto;
        precoDeVenda = jsondata.PrecoDeVenda;
        preco = jsondata.Preco;

        var trCreate = document.createElement("tr");
        trCreate.setAttribute("id", "retornoProdutoPesquisado");
        tbody.appendChild(trCreate);

        trId = document.getElementById("retornoProdutoPesquisado");
        var tdCreateId = document.createElement("td");
        trId.appendChild(tdCreateId);
        tdCreateId.appendChild(document.createTextNode(id));

        var tdCreateNome = document.createElement("td");
        trId.appendChild(tdCreateNome);
        tdCreateNome.appendChild(document.createTextNode(nome));

        var tdCreatePreco = document.createElement("td");
        trId.appendChild(tdCreatePreco);
        tdCreatePreco.appendChild(document.createTextNode(preco));

        var tdCreatePrecoDeCusto = document.createElement("td");
        trId.appendChild(tdCreatePrecoDeCusto);
        tdCreatePrecoDeCusto.appendChild(document.createTextNode(precoDeCusto));

        var tdCreatePrecoDeVenda = document.createElement("td");
        trId.appendChild(tdCreatePrecoDeVenda);
        tdCreatePrecoDeVenda.appendChild(document.createTextNode(precoDeVenda));


        var tdLinks = document.createElement("td");
        trId.appendChild(tdLinks);


        var aCreateEditar = document.createElement("a");
        aCreateEditar.setAttribute("href", "/Produtos/Editar/" + id);
        tdLinks.appendChild(aCreateEditar);
        aCreateEditar.appendChild(document.createTextNode("Editar "));

        var aCreateDetalhes = document.createElement("a");
        aCreateDetalhes.setAttribute("href", "/Produtos/Detalhes/" + id);
        tdLinks.appendChild(aCreateDetalhes);
        aCreateDetalhes.appendChild(document.createTextNode("Detalhes "));

        var aCreateDeletar = document.createElement("a");
        aCreateDeletar.setAttribute("href", "/Produtos/Deletar/" + id);
        tdLinks.appendChild(aCreateDeletar);
        aCreateDeletar.appendChild(document.createTextNode("Deletar"));

    });
}

