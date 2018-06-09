using SunsetHotelSystem.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunsetHotelSystem.UI.Models {
    public class Tarifa {

        public List<TSH_Caracteristica_habitacion> CaracteristicasHabitaciones { get; set; }
        public List<TSH_Tipo_Habitacion> TipoHabitaciones { get; set; }

        public Tarifa(List<TSH_Caracteristica_habitacion> caracteristicasHabitaciones, List<TSH_Tipo_Habitacion> tipoHabitaciones) {
            CaracteristicasHabitaciones = caracteristicasHabitaciones;
            TipoHabitaciones = tipoHabitaciones;
        }//Fin del constructor sobrecargado.
    }//Fin de la clase Tarifa.
}//Fin del namespace.