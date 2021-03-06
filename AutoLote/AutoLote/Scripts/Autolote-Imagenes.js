﻿$("#addItem").click(function () {
    $.ajax({
        url: this.href,
        cache: false,
        success: function (html) { $("#editorRows").append(html); }
    });
    return false;
});

$(document).on("click", ".deleteRow", function () {
    var id = $(this)[0].id;
    $(this).parents("div.editorRow:first").remove();

    var gui = GetRandomGUI();
    var htm = '<div class="editorRow">';
    htm += '<input type="hidden" autoComplete = "off" name = "AutomovilImagenes.index" value = "' + gui + '"/>';
    htm += '<input type="hidden" autoComplete = "off" name = "AutomovilImagenes[' + gui + '].AutoimagenesID" value="' + id + '"/>';
    htm += '<input type="hidden" autoComplete = "off" name = "AutomovilImagenes[' + gui + '].ImagenEliminada" value="true"/></div>';
    $('#Automovilimagenes').append(htm);

    return false;
});



$("#add").on('click', function () {
    var gui = GetRandomGUI();
    var htm = '<div class="editorRow">';
    htm += '<input type="hidden" autoComplete = "off" name = "AutomovilImagenes.index" value = "' + gui + '"/>';
    htm += 'Imagen: <input type = "file" name = "Automovilimagenes[' + gui + '].ImagenSubida" id = "xyzy" />';
    htm += '<a href="#" class="deleteRow">Eliminar</a></div>';
    $('#Automovilimagenes').append(htm);
    return false;
});

function GetRandomGUI() {
    var val = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
    return val;
}