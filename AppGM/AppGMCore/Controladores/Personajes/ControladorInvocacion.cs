using System;

namespace AppGM.Core
{
    public class ControladorInvocacion : ControladorPersonaje
    {
        #region Controladores

        public ControladorPersonaje ControladorInvocador { get; set; }

        public ControladorEfecto ControladorEfecto { get; set; }

        #endregion

        #region Constructores

        public ControladorInvocacion(ModeloPersonaje _modeloInvocacion) : base(_modeloInvocacion) { } 
        #endregion

        #region Funciones

        /// <summary>
        /// Metodo que lidia con las operacion necesarias a realizar cada vez que avanza un turno
        /// TODO: Aplicar los efectos
        /// TODO: Si funciona de manera automatica chequear cada turno si puede actuar
        /// </summary>
        public override void AvanzarTurno()
        {
            for (int i = 0; i < Efectos.Count; ++i)
                Efectos[i].AplicarEfecto(this);
        }

        #endregion
    }

    public class ControladorInvocacionTemporal : ControladorInvocacion
    {
        #region Constructores
        public ControladorInvocacionTemporal(ModeloInvocacionTemporal _modeloInvocacionTemporal) : base(_modeloInvocacionTemporal) { } 

        #endregion

        #region Funciones

        /// <summary>
        /// Metodo que lidia con las operacion necesarias a realizar cada vez que avanza un turno
        /// TODO: Aplicar los efectos
        /// TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Restar los turnos de vida que le quedan
        /// </summary>
        public override void AvanzarTurno()
        {
            for (int i = 0; i < Efectos.Count; ++i)
            {
                Efectos[i].AplicarEfecto(this);
            }
        }

        #endregion
    }

    public class ControladorInvocacionCondicionada : ControladorInvocacion
    {
        /// <summary>
        /// Funcion que nos retorna un booleano dictando si la invocacion debe desaparecer o no
        /// </summary>
        public Func<ControladorPersonaje, bool> DebeDesaparecer;

        public ControladorInvocacionCondicionada(ModeloInvocacionCondicionada _modeloInvocacionCondicionada) : base(_modeloInvocacionCondicionada){}

        #region Funciones

        /// <summary>
        /// Metodo que lidia con las operacion necesarias a realizar cada vez que avanza un turno
        /// TODO: Si funciona de manera automatica chequear cada turno si puede actuar. Revisar si se cumplen las condiciones para que desaparezca
        /// </summary>
        public override void AvanzarTurno()
        {
            for (int i = 0; i < Efectos.Count; ++i)
            {
                Efectos[i].AplicarEfecto(this);
            }
        }

        #endregion
    }
}