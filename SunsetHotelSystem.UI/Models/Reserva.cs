using SunsetHotelSystem.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunsetHotelSystem.UI.Models {
    public class Reserva {
        public List<TSH_Tipo_Habitacion> TipoHabitaciones { get; set; }
        public List<TSH_Reserva> Reservas { get; set; }
        public TSH_Reserva Reserva_ { get; set; }
        public TSH_Tipo_Habitacion TipoHabitacionReserva { get; set; }
        public Reserva(List<TSH_Reserva> reservas, List<TSH_Tipo_Habitacion> tipoHabitaciones) {
            TipoHabitaciones = tipoHabitaciones;
            Reservas = reservas;
        }//Fin del constructor sobrecargado.

        public Reserva(TSH_Reserva reserva, TSH_Tipo_Habitacion tipoHabitacionReserva) {
            Reserva_ = reserva;
            TipoHabitacionReserva = tipoHabitacionReserva;
        }//Fin del constructor sobrecargado.
    }//Fin de la clase Reserva.
}//Fin del namespace.