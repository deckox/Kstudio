 $(document).ready(function () {
    $("#campoPesquisa").autocomplete({
        source: '@Url.Action("BuscarIdBanda", "Agendamento")'      
    });
});
 
 