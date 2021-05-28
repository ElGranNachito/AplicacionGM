namespace AppGM.Core
{
    public class ControladorRol : Controlador<ModeloRol>
    {
        public DatosRol datosRol;

        #region Eventos

        /// <summary>
        /// Representa un metodo que lidie con eventos de avance del dia en el rol
        /// </summary>
        /// <param name="nuevoDia"></param>
        public delegate void dAvanzarDia(ref ushort nuevoDia);

        /// <summary>
        /// Evento que se dispara al avanzar de dia
        /// </summary>
        public event dAvanzarDia OnAvanzarDia = delegate{};

        #endregion

        #region Constructores
        public ControladorRol(ModeloRol _modelo)
			:base(_modelo)
        {
	        datosRol = new DatosRol(_modelo);
        } 

        #endregion

        #region Funciones

        /// <summary>
        /// Avanza de dia en el rol
        /// </summary>
        public void AvanzarDia()
        {
            ushort nuevoDia = ++modelo.Dia;

            OnAvanzarDia(ref nuevoDia);

            modelo.Dia = nuevoDia;
        }

        #endregion
    }
}
