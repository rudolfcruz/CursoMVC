$(function () {

    $("#MarcaID").change(function () {
        if ($("#ModelosID").val()) {
            $.getJSON('/Modelos/ListaModelosMarcas/' + $('#MarcaID').val(), function (data) {
                var items = $('#ModelosID').children()[0].outerHTML; //'<option>Seleccione un modelo</option>';
                $.each(data, function (i, modelo) {
                    items += "<option value='" + modelo.Id + "'>" + modelo.Modelo + "</option>";
                });
                $("#ModelosID").html(items);
            });
        }
    });

    var id = 0;
    if ($("#Id").val()) {
        id = $("#Id").val();
    }

    $.getJSON('/Marcas/ListaMarcas/' + id, function (data) {
        var items = $("#MarcaID")[0].innerHTML; //"<option>Seleccione una marca</option>";

        $.each(data, function (i, marca) {
            if (marca.selected) {
                items += "<option value='" + marca.Id + "'selected>" + marca.Marca + "</option>";
                MostrarModelosPorMarca();
            } else {
                items += "<option value='" + marca.Id + "'>" + marca.Marca + "</option>";
            }
        });
        $('#MarcaID').html(items);
    });

    function MostrarModelosPorMarca() {
        $.getJSON('/Automovil/ListaModelosPorAutomovil/' + $("#Id").val(), function (data) {
            var items = $("#ModelosID")[0].innerHTML;
            $.each(data, function (i, modelo) {
                if (modelo.Selected) {
                    items += "<option value='" + modelo.Id + "'selected>" + modelo.Modelo + "</option>";
                } else {
                    items += "<option value='" + modelo.Id + "'>" + modelo.Modelo + "</option>";
                }
            });
            $('#ModelosId').html(items);
        });
    }
});