function validarCampo() {


    var banda = document.getElementById('banda').value;
    var responsavel = document.getElementById('responsavel').value;
    var email = document.getElementById('email').value;
    var telefone = document.getElementById('telefoneId').value;
  

    if (banda == "" && responsavel == "" && email == "" && telefone == "") {

        alert("Favor preencher os campos: banda,responsavel,email e telefone");
        return false;
    }

    else {
        return true;
    }
}

function apenasletras(input) {

}


function mostrardate() {

    var date = new Date();
    alert("dateok")
    var whatYearIs = date.getFullYear();
    alert("whatYearIsok")
    
    window.alert(whatYearIs);

    resultado.innerHTML = 'aaaaaaaaaaaaaaa';



}

function addFields() {

    
    var detalhe = document.createElement("div"); //cria <div></div>
    detalhe.setAttribute("name", "detalhe"); //cria name <div name="detalhe"></div>
    var detalhes = document.getElementById("detalhes");
    var template = document.getElementById("template"); 
    var index = document.getElementById("detalhes").querySelectorAll('div[name=detalhe]').length;

    var elDescricao = template.cloneNode(true);   // clona a div abaixo:
    //<div id="template" class="form-group hide" disa="">
    //    <label class="control-label col-md-2"></label>
    //    <div class="col-md-10">
    //        <input class="form-control text-box single-line" type="text" value="">
	//    </div>
    //</div>
    elDescricao.className = "form-group";  //<div id="template" class="form-group hide" >
    elDescricao.querySelector("label").innerHTML = "Descrição";  //<label class="control-label col-md-2">Descrição</label>
    elDescricao.querySelector("label").setAttribute("for", "Detalhes_" + index + "__Descricao"); // <label class="control-label col-md-2" for="Detalhes_[index]__Descricao">Descrição</label>
    elDescricao.querySelector("input").setAttribute("name", "Detalhes[" + index + "].Descricao"); // <input class="form-control text-box single-line" type="text" value="" name="Detalhes[index].Descricao">
    elDescricao.querySelector("input").id = "Detalhes_" + index + "__Descricao"; // <input class="form-control text-box single-line" type="text" value="" name="Detalhes[index].Descricao" id="Detalhes_1__Descricao">

    var elHora = template.cloneNode(true);
    elHora.className = "form-group";
    elHora.querySelector("label").innerHTML = "Quantidade";
    elHora.querySelector("label").setAttribute("for", "Detalhes_" + index + "__Quantidade");
    elHora.querySelector("input").setAttribute("name", "Detalhes[" + index + "].Quantidade");
    elHora.querySelector("input").id = "Detalhes_" + index + "__Quantidade";

    var elPreco = template.cloneNode(true);
    elPreco.className = "form-group";
    elPreco.querySelector("label").innerHTML = "Preço";
    elPreco.querySelector("label").setAttribute("for", "Detalhes_" + index + "__Preco");
    elPreco.querySelector("input").setAttribute("name", "Detalhes[" + index + "].Preco");
    elPreco.querySelector("input").id = "Detalhes_" + index + "__Preco";


    detalhe.appendChild(elDescricao);
    detalhe.appendChild(elHora);
    detalhe.appendChild(elPreco);
    detalhe.appendChild(elPreco);
    detalhes.appendChild(detalhe);

}

function clearform() {

    alert("before");
    document.getElementById("cadastroform").reset();
    alert("after");
}

$(document).ready(function () {
    $("#campoPesquisa").autocomplete({
        source: '@Url.Action("BuscarIdBanda", "Agendamento")'      
    });
});
 
$(document).ready(function () {

    $("#salvar").click(function () {
        var banda = $("#banda").val();
        var responsavel = $("#responsavel").val();
        var estilomusical = $("#estilomusical").val();
        var email = $("#email").val();
        var telefone = $("#telefone").val();

        var produto = {
            Banda: banda,
            Responsavel: responsavel,
            EstiloMusical: estilomusical,
            Email: email,
            Telefone: telefone
        };

        $.post("/Clientes/Cadastro", { produto }, function (data) {
            var jsonResult = JSON.parse(data);
            showMessage(jsonResult.Message, jsonResult.Error);
        });

    });


    function showMessage(msg, isError) {
        if (isError == true) {
            $("#msgOk").hide();
            $("#msgErro").html(msg);
            $("#msgErro").show();
        } else {
            $("#msgErro").hide();
            $("#msgOk").html(msg);
            $("#msgOk").show();
        }
    }

});