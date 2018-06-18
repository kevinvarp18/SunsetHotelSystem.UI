function nuevaPublicidad() {
    var formulario = document.getElementById("formularioPublicidad");
    var inputPublicidadTotal = document.getElementById("publicidadTotal");

    var inputNuevaPublicidad = document.getElementById("nuevaPublicidad");
    inputNuevaPublicidad.value = parseInt(inputNuevaPublicidad.value) + 1;

    var indexAtributtes = parseInt(inputPublicidadTotal.value) + parseInt(inputNuevaPublicidad.value);

    var inputID = document.createElement("input");
    inputID.setAttribute('type', 'number');
    inputID.id = 'id' + indexAtributtes;
    inputID.name = 'id' + indexAtributtes;
    inputID.style.display = "none";
    inputID.value = indexAtributtes;

    var inputBorrado = document.createElement("input");
    inputBorrado.setAttribute('type', 'number');
    inputBorrado.id = 'borrado' + indexAtributtes;
    inputBorrado.name = 'borrado' + indexAtributtes;
    inputBorrado.style.display = "none";
    inputBorrado.value = 1;

    var inputRuta = document.createElement("input");
    inputRuta.setAttribute('type', 'text');
    inputRuta.id = 'rutaPublicidad' + indexAtributtes;
    inputRuta.name = 'rutaPublicidad' + indexAtributtes;
    inputRuta.style.marginLeft = "15%";
    inputRuta.required = true;

    var divRutaPublicidad = document.createElement("div");
    divRutaPublicidad.setAttribute('class', 'formCenter');
    divRutaPublicidad.appendChild(inputRuta);

    var spanImagen = document.createElement("span");
    spanImagen.innerHTML = "Subir imagen nueva";

    var inputImagen = document.createElement("input");
    inputImagen.id = 'imagen' + indexAtributtes;
    inputImagen.name = 'imagen' + indexAtributtes;
    inputImagen.setAttribute('type', 'file');
    inputImagen.required = true;

    var divSrc = document.createElement("div");
    divSrc.setAttribute('class', 'col-md-6');
    divSrc.appendChild(spanImagen);
    divSrc.appendChild(inputImagen);

    var divImagen = document.createElement("div");
    divImagen.setAttribute('class', 'formCenter');
    divImagen.appendChild(divSrc);

    var divRow = document.createElement("div");
    divRow.setAttribute('class', 'row');
    divRow.appendChild(inputID);
    divRow.appendChild(inputBorrado);
    divRow.appendChild(divRutaPublicidad);
    divRow.appendChild(divImagen);

    var acceptButton = document.createElement("input");
    acceptButton.value = 'Aceptar';
    acceptButton.setAttribute('type', 'submit');

    var cancelButton = document.createElement("button");
    cancelButton.setAttribute('data-toggle', 'modal');
    cancelButton.setAttribute('data-target', '#myModal');
    cancelButton.innerHTML = 'Cancelar';

    var centerLabel = document.createElement("center");
    centerLabel.id = "center";
    centerLabel.appendChild(acceptButton);
    centerLabel.appendChild(cancelButton);
    formulario.removeChild(document.getElementById("center"));
    formulario.appendChild(divRow);
    formulario.appendChild(centerLabel);
}//Fin del método nuevaFacilidad.

function eliminarPublicidad(idPublicidad) {
    var divRow = document.getElementById("row" + idPublicidad);
    divRow.style.filter = "alpha(opacity=50)";
    divRow.style.opacity = "0.5";
    var inputBorrado = document.getElementById("borrado" + idPublicidad);
    inputBorrado.value = 0;
    var boton = document.getElementById("eliminar" + idPublicidad);
    boton.id = "recuperar" + idPublicidad;
    boton.value = "Restaurar publicidad";
    boton.setAttribute("onclick", "recuperarPublicidad(" + idPublicidad + ");");
}//Fin del método eliminarPublicidad.

function recuperarPublicidad(idPublicidad) {
    var divRow = document.getElementById("row" + idPublicidad);
    divRow.style.filter = "alpha(opacity=100)";
    divRow.style.opacity = "1";
    var inputBorrado = document.getElementById("borrado" + idPublicidad);
    inputBorrado.value = 1;
    var boton = document.getElementById("recuperar" + idPublicidad);
    boton.id = "eliminar" + idPublicidad;
    boton.value = "Eliminar publicidad";
    boton.setAttribute("onclick", "eliminarPublicidad(" + idPublicidad + ");");
}//Fin de la función restaurarPublicidad.