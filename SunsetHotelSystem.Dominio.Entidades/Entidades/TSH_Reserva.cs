//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunsetHotelSystem.Dominio.Entidades.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class TSH_Reserva
    {
        public int TN_Identificador_TSH_Reserva { get; set; }
        public System.Guid TN_Numero_Reserva_TSH_Reserva { get; set; }
        public int TN_Num_Habitacion_TSH_Reserva { get; set; }
        public string TC_Id_Cliente_TSH_Reserva { get; set; }
        public System.DateTime TD_Fecha_Ingreso_TSH_Reserva { get; set; }
        public System.DateTime TD_Fecha_Salida_TSH_Reserva { get; set; }
        public int TN_Borrado_TSH_Reserva { get; set; }
    
        public virtual TSH_Cliente TSH_Cliente { get; set; }
        public virtual TSH_Habitacion TSH_Habitacion { get; set; }
    }
}
