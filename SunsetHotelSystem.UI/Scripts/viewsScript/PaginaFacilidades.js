function nuevaFacilidad() {

    var formulario = document.getElementsByName("formularioFacilidades");

    var inputFacilidadesTotal = document.getElementsByName('formularioFacilidades').item(0);
    var indexAtributtes = inputFacilidadesTotal.value;
    indexAtributtes++;
    inputFacilidadesTotal.value = indexAtributtes;

    var inputNombre = document.createElement("input");
    inputNombre.id = 'tituloFacilidad' + indexAtributtes;
    inputNombre.name = 'tituloFacilidad' + indexAtributtes;

    var inputDescripcion = document.createElement("textarea");
    inputDescripcion.id = 'descripcion' + indexAtributtes;
    inputDescripcion.name = 'descripcion' + indexAtributtes;

    var spanImagen = document.createElement("span");
    spanImagen.innerHTML = "Subir imagen nueva";

    var inputImagen = document.createElement("input");
    inputImagen.id = 'imagen' + indexAtributtes;
    inputImagen.name = 'imagen' + indexAtributtes;
    inputImagen.setAttribute('type', 'file');

    var divSrc = document.createElement("div");
    divSrc.setAttribute('class', 'col-md-6');
    divSrc.appendChild(spanImagen);
    divSrc.appendChild(inputImagen);

    var divImagen = document.createElement("div");
    divImagen.setAttribute('class', 'formCenter');
    divImagen.appendChild(divSrc);

    var divRow = document.createElement("div");
    divRow.setAttribute('class', 'row');
    divRow.appendChild(inputNombre);
    divRow.appendChild(inputDescripcion);
    divRow.appendChild(divImagen);

    //formulario.appendChild(divRow);

}//Fin del método nuevaFacilidad.