﻿function nuevaFacilidad() {
    var formulario = document.getElementById("formularioFacilidades");
    var inputFacilidadesTotal = document.getElementById("facilidadesTotal");
    var indexAtributtes = inputFacilidadesTotal.value;
    indexAtributtes++;
    inputFacilidadesTotal.value = indexAtributtes;

    var inputNombre = document.createElement("input");
    inputNombre.setAttribute('type', 'text');
    inputNombre.id = 'tituloFacilidad' + indexAtributtes;
    inputNombre.name = 'tituloFacilidad' + indexAtributtes;
    inputNombre.style.marginLeft = "15%";
    inputNombre.required = true;

    var divNombreFacilidad = document.createElement("div");
    divNombreFacilidad.setAttribute('class', 'formCenter');
    divNombreFacilidad.appendChild(inputNombre);

    var inputDescripcion = document.createElement("textarea");
    inputDescripcion.id = 'descripcion' + indexAtributtes;
    inputDescripcion.name = 'descripcion' + indexAtributtes;
    inputDescripcion.required = true;

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
    divRow.appendChild(divNombreFacilidad);
    divRow.appendChild(inputDescripcion);
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

function eliminarFacilidad(idFacilidad) {
    var divRow = document.getElementById("row" + idFacilidad);
    divRow.style.display = "none";
    var inputBorrado = document.getElementById("borrado" + idFacilidad);
    inputBorrado.value = 0;
}//Fin del método eliminarFacilidad.