using SunsetHotelSystem.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunsetHotelSystem.UI.Models {
    public class Habitacion {
        public List<TSH_Tipo_Habitacion> TipoHabitaciones { get; set; }
        public List<TSH_Habitacion> Habitaciones { get; set; }
        public List<TSH_Habitacion> Reservadas { get; set; }
        public Habitacion(List<TSH_Tipo_Habitacion> tipoHabitaciones, List<TSH_Habitacion> habitaciones) {
            TipoHabitaciones = tipoHabitaciones;
            Habitaciones = habitaciones;
        }//Fin del constructor sobrecargado.
        public Habitacion(List<TSH_Tipo_Habitacion> tipoHabitaciones, List<TSH_Habitacion> habitaciones, List<TSH_Habitacion> reservadas)
        {
            TipoHabitaciones = tipoHabitaciones;
            Habitaciones = habitaciones;
            Reservadas = reservadas;
        }//Fin del constructor sobrecargado.
    }//Fin de la clase Tarifa.
}//Fin del namespace.