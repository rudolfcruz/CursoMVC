$(function () {
    var id = 0;
    if ($("#AutomovilID").val()) {
        id = $("#AutomovilID").val();
    }
    $.getJSON('/Automovil/ListaTiposAutomovil/' + id, function (data) {

        var items = "<option>Seleccione un tipo</option>";
        $.each(data, function (i, item) {

            if (item.selected) {
                items += "<option value='" + item.Id + "' selected>" + item.Tipo + "</option>";
            }
            else {
                items += "<option value='" + item.Id + "'>" + item.Tipo + "</option>";
            }
        });

        $('#TipoID').html(items);
    });
});