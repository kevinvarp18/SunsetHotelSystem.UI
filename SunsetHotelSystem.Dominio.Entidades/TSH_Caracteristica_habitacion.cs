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
    
    public partial class TSH_Caracteristica_habitacion
    {
        public string TC_Descripcion_TSH_Caracteristica_habitacion { get; set; }
        public int TN_Id_Tipo_Habitacion_TSH_Caracteristica_Habitacion { get; set; }
        public int TN_Identificador_TSH_Caracteristica_habitacion { get; set; }
    
        public virtual TSH_Tipo_Habitacion TSH_Tipo_Habitacion { get; set; }
    }
}
