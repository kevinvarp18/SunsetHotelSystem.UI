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
    
    public partial class TSH_Habitacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TSH_Habitacion()
        {
            this.TSH_Reserva = new HashSet<TSH_Reserva>();
        }
    
        public int TN_Identificador_TSH_Habitacion { get; set; }
        public int TN_Numero_Habitacion_TSH_Habitacion { get; set; }
        public int TN_Id_TipoH_TSH_Habitacion { get; set; }
        public int TN_Estado_TSH_habitacion { get; set; }
        public int TN_Borrado_TSH_Habitacion { get; set; }
        public string TC_Descripcion_TSH_Habitacion { get; set; }
        public byte[] TI_Imagen_TSH_Habitacion { get; set; }
        public System.Guid TN_Id_Imagen_TSH_Habitacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TSH_Reserva> TSH_Reserva { get; set; }
        public virtual TSH_Tipo_Habitacion TSH_Tipo_Habitacion { get; set; }
    }
}
