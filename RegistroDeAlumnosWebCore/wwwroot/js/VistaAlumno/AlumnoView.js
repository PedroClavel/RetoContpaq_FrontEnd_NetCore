var isNew = true;
var isSaveChanges = false;
var idRowDelete = 0;

$(document).ready(function () {
    console.log("ready!");
    $("#myModalAlertSuccess").on('hide.bs.modal', function () {
        if (isSaveChanges)
            window.location.href = '/Alumno/ListaAlumnos';
    });

    $("#myModalAlertAdvertencia").on('hide.bs.modal', function () {
        if (isSaveChanges)
            window.location.href = '/Alumno/ListaAlumnos';
    });

    $("#myModalAlertSuccess").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true
    });

    $("#myModal").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true
    });


    $("#loadMe").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true
    });

    $("#loadMe").modal({
        backdrop: "static", //remove ability to close modal with click
        keyboard: false, //remove option to close with keyboard
        show: true
    });
});

function SaveView() {
    $('#myModal').modal('show');
    $('#textSuccess').text('Agregar Alumno');
    SetAlumnoValuesDefault();

    isNew = true;
}

function EditView(idRow) {
    $('#myModal').modal('show');
    $('#textSuccess').text('Editar Alumno');
    isNew = false;

    $.ajax({
        url: '/Alumno/GetAlumno', // Url url: '~/Controllers/AlumnoController/GetAlumno', // Url
        data: {
            idAlumno: idRow
        },
        type: "get"  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                var respuesta = JSON.parse(result);

                if (respuesta.isCompleted) {
                    var alumnoDTO = JSON.parse(respuesta.alumno);

                    SetAlumnoValues(alumnoDTO);
                }
                else if (!respuesta.isCompleted && respuesta.message != "") {
                    console.log(respuesta.message);

                    $('#textTitleSuccess').text("Error");
                    $('#textSuccess').text(respuesta.message);
                }
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {
            $("#loadMe").modal("hide");
        });
}

function DeleteView(idRow) {
    console.log("Entro al método DeleteView " + idRow);
    idRowDelete = idRow;
    $('#myModalAlertAdvertencia').modal('show');
    $('#textTitleAdvertencia').text("Advertencia!");
    $('#textAdvertencia').text('Se eliminará el registro seleccionado. ¿Desea Continuar?');
}

function onClose() {
    $('#myModal').modal('hide');
}

function onSave() {
    $('#myModal').modal('hide');
    $("#loadMe").modal('show');
    isSaveChanges = false;

    var jSon = GetJson();
    var urlControl = "";
    var texto = "";
    var titulo = "";

    if (isNew) {
        urlControl = '/Alumno/InsertAlumno';
        texto = "Agregado";
        titulo = "El registro se agregó correctamente";
    }
    else {
        urlControl = '/Alumno/EditAlumno';
        texto = "Modificado";
        titulo = "El registro se modificó correctamente";
    }

    $.ajax({
        url: urlControl, // Url url: '~/Controllers/AlumnoController/GetAlumno', // Url
        data: jSon,
        type: "post"  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                var respuesta = JSON.parse(result);

                if (respuesta.isValid) {
                    myModal
                    $('#myModalAlertSuccess').modal('show');
                    $('#textTitleSuccess').text(texto);
                    $('#textSuccess').text(titulo);

                    isSaveChanges = true;
                }
                else if (!respuesta.isValid && respuesta.message != "") {
                    console.log(respuesta.message);
                    $('#myModal').modal('show');
                    $('#myModalAlertSuccess').modal('show');
                    $('#textTitleSuccess').text("Error");
                    $('#textSuccess').text(respuesta.message);
                    isSaveChanges = false;
                }
            }
            else {
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            isSaveChanges = false;
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {
            $("#loadMe").modal("hide");
        });
}


function Delete() {
    $('#myModalAlertAdvertencia').modal('hide');
    $("#loadMe").modal('show');

    $.ajax({
        url: '/Alumno/DeleteAlumno', // Url url: '~/Controllers/AlumnoController/GetAlumno', // Url
        data: { idAlumno: idRowDelete },
        type: "post"  // Verbo HTTP
    })
        // Se ejecuta si todo fue bien.
        .done(function (result) {
            if (result != null) {
                var respuesta = JSON.parse(result);

                if (respuesta.isValid) {
                    $('#myModalAlertSuccess').modal('show');
                    $('#textTitleSuccess').text("Eliminado");
                    $('#textSuccess').text('El registro se eliminó correctamente.');

                    isSaveChanges = true;
                }
                else if (!respuesta.isValid && respuesta.message != "") {
                    $('#textTitleSuccess').text("Error");
                    $('#textSuccess').text(respuesta.message);
                    isSaveChanges = false;
                }
            }
            else {
            }
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {
            isSaveChanges = false;
        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {
            $("#loadMe").modal("hide");
        });
}
function SetAlumnoValues(alumnoDTO) {
    if (alumnoDTO != null) {
        $('#idAlumno').val(alumnoDTO.IdAlumno);
        $('#nombres').val(alumnoDTO.Nombres);
        $('#apellidoP').val(alumnoDTO.ApellidoPaterno);
        $('#apellidoM').val(alumnoDTO.ApellidoMaterno);
        $('#edad').val(alumnoDTO.Edad);
        $('#grado').val(alumnoDTO.Grado);
        $('#grupo').val(alumnoDTO.Grupo);
        $('#telefono').val(alumnoDTO.Telefono);
    }
}

function SetAlumnoValuesDefault() {
    if (alumnoDTO != null) {
        $('#nombres').val("");
        $('#apellidoP').val("");
        $('#apellidoM').val("");
        $('#edad').val("");
        $('#grado').val("1");
        $('#grupo').val("A");
        $('#telefono').val("");
    }
}

function GetJson() {
    if (isNew) {
        return {
            nombres: $('#nombres').val(),
            apellidoP: $('#apellidoP').val(),
            apellidoM: $('#apellidoM').val(),
            edad: $('#edad').val(),
            grado: $('#grado').val(),
            grupo: $('#grupo').val(),
            telefono: $('#telefono').val()
        };
    }
    else {
        return {
            idAlumno: $('#idAlumno').val(),
            nombres: $('#nombres').val(),
            apellidoP: $('#apellidoP').val(),
            apellidoM: $('#apellidoM').val(),
            edad: $('#edad').val(),
            grado: $('#grado').val(),
            grupo: $('#grupo').val(),
            telefono: $('#telefono').val()
        };
    }
}

function onCloseModalAlertSuccess() {
    $('#myModalAlertSuccess').modal('hide');

}

function onCloseModalAlertAdvertencia() {
    $('#myModalAlertAdvertencia').modal('hide');

}