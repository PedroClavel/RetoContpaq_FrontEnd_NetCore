$(function () {
    $("#formLogin").submit(function (e) {
        e.preventDefault();

        $("#loadMe").modal({
            backdrop: "static", //remove ability to close modal with click
            keyboard: false, //remove option to close with keyboard
            show: true
        });

        $("#loadMe").modal('show');

        var usuario = $('#user').val();
        var password = $('#password').val();

        $.ajax({
            url: '/Login/ValidateLogin',//'@Url.Action("ValidateLogin", "Login")', // Url
            data: {
                usuario: usuario,
                pass: password
            },
            type: "get"  // Verbo HTTP
        })
            // Se ejecuta si todo fue bien.
            .done(function (result) {

                if (result != null) {
                    var respuesta = JSON.parse(result);

                    if (respuesta.isUserValid) {
                        window.location.href = '/Alumno/ListaAlumnos'//'@Url.Action("ListaAlumnos", "Alumno")'
                    }
                    else if (!respuesta.isUserValid && respuesta.message != "") {
                        console.log(respuesta.message);

                        $("#myModalAlertSuccess").modal({
                            backdrop: "static", //remove ability to close modal with click
                            keyboard: false, //remove option to close with keyboard
                            show: true
                        });

                        $('#myModalAlertSuccess').modal('show');
                        $('#textTitleSuccess').text("Error");
                        $('#textSuccess').text(respuesta.message);
                    }
                }
                else {
                }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {
            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {
                $("#loadMe").modal("hide");
            });
    });
});


function onCloseModalAlertSuccess() {
    $('#myModalAlertSuccess').modal('hide');

}