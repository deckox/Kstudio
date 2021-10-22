window.onload = function insertCheckbox() {

    var statusDaComanda = document.getElementById("StatusComanda").value;

    if (statusDaComanda != "True") {

        document.getElementById("statusaberto").click();

    } else {

        document.getElementById("statusfechado").click();
    }
}

document.onclick = function valorCalculadoDaComanda() {

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

function addFieldsProdutos() {

    var detalhes = document.getElementById("tabelaProdutos").getElementsByTagName('tbody')[0];
    var index = document.getElementById("tabelaProdutos").getElementsByTagName('tr').length - 1;
    var modeloCloneTr = document.getElementById("modeloTr");
    var modeloTr = modeloCloneTr.cloneNode(true);

    modeloTr.setAttribute("id", "tr" + index);


    modeloTr.children[1].children[0].children[0].children.type = "";
    modeloTr.children[1].children[0].children[1].children.Nome.id = "pesquisaProduto_" + index;
    modeloTr.children[1].children[0].children[1].children.Nome.type = "text";
    modeloTr.children[2].children[0].children[1].children.Quantidade.id = "Produto_Quantidade_" + index;
    modeloTr.children[2].children[0].children[1].children.Quantidade.type = "text";
    modeloTr.children[3].children[0].children[1].children.Preco.id = "Produto_Preco_" + index;
    modeloTr.children[3].children[0].children[1].children.Preco.type = "text";



    detalhes.appendChild(modeloTr);


}

function removeFieldsProdutos() {

    var quantidadeDeTr = document.getElementById("tabelaProdutos").getElementsByTagName('tr').length;

    if (quantidadeDeTr < 2) {
        alert("Não é possivel excluir este campo!!");
    } else {

        var detalhe = document.getElementById("modeloTr");
        detalhe.nextElementSibling.remove();

    }

}