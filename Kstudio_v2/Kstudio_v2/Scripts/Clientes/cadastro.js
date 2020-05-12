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