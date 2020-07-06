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

    function mostrardate() {

            var date = new Date()
            var whatYearIs = date.getFullYear()
            var resultado = document.getElementById('eu')


            resultado.innerHTML = 'aaaaaaaaaaaaaaa'
            window.alert(whatYearIs)


    }

    function validarCampo() {

        var banda = document.getElementById('banda')
        var responsavel = document.getElementById('responsavel')
        var email = document.getElementById('email')
        var telefone = document.getElementById('telefone')

        if (banda.value.length == 0 || responsavel.value.length == 0 || email.value.length == 0 || telefone.value.length == 0) {

            alert("OI")
        }
    }
  

});