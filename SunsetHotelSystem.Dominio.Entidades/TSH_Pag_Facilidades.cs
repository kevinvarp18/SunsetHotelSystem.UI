//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunsetHotelSystem.Dominio.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class TSH_Pag_Facilidades
    {
        public int TN_Identificador_TSH_Pag_Facilidades { get; set; }
        public int TN_IdentificadorNumFac_TSH_Pag_Facilidades { get; set; }
        public System.Guid TN_Id_Imagen_TSH_Pag_Facilidades { get; set; }
        public string TC_Descripcion_TSH_Pag_Facilidades { get; set; }
        public string TC_TituloFacilidad_TSH_Pag_Facilidades { get; set; }
        public byte[] TI_Imagen_TSH_Pag_Facilidades { get; set; }
        public int TN_Borrado_TSH_Pag_Facilidades { get; set; }
    
        public virtual TSH_Pagina TSH_Pagina { get; set; }
    }
}