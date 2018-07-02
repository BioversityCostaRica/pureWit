$(document).ready(function () {

    $('#checkcuando').change(function () {
        if ($(this).is(":checked")) {

            $("#divprogramado").css("display", "none");
            $("#tipoEnvio").val("inmediato");
        }
        else
        {
            $("#divprogramado").css("display", "flex");
            $("#tipoEnvio").val("programado");
        }
    });

});