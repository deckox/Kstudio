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
    var whatYearIs = date.getFullYear();
    
    window.alert(whatYearIs);

    resultado.innerHTML = 'aaaaaaaaaaaaaaa';



}

function addFields() {

   // var ul = document.getElementById("campo");
    var form = document.createElement("form");
    form.setAttribute("method", "post");
    form.setAttribute("action", "submit.php"); 

    var FN = document.createElement("input");
    FN.setAttribute("type", "text");
    FN.setAttribute("name", "FullName");
    FN.setAttribute("placeholder", "Full Name"); 

    form.appendChild(FN);
    document.getElementsByClassName("goku")[0]
        .appendChild(form);
    alert("GOKU");

}
 
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