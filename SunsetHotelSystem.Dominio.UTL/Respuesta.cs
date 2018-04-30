using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunsetHotelSystem.Dominio.UTL {
    public partial class Respuesta<T> {
        public Boolean bnlIndicadorTransaccion = true;
        public EMessageType enuTipoMensaje;
        public String strMensajeRespuesta = "¡Transacción realizada exitosamente!";
        public String strOrigen = String.Empty;
        public T valorRetorno;
    }//Fin de la clase Response.

    #region "Enums"
    public enum EMessageType {
        eNone = 0,
        eValidation = 1,
        eInformative = 2,
        eError = 3,
        eRequest = 4
    }//Fin del EMessageType.
    #endregion
}
