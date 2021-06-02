namespace AppGM.Core
{
    /// <summary>
    /// Controlador de un <see cref="ModeloRol"/>
    /// </summary>
    public class ControladorRol : Controlador<ModeloRol>
    {
		#region Campos

		/// <summary>
		/// Datos del rol
		/// </summary>
		public DatosRol datosRol; 

		#endregion

		#region Eventos

        /// <summary>
        /// Representa un metodo que lidie con eventos de avance de horas en el rol
        /// </summary>
        /// <param name="nuevaHora"></param>
        public delegate void dAvanzarHora(ref int nuevaHora);

        /// <summary>
        /// Evento que se dispara al avanzar de hora
        /// </summary>
        public event dAvanzarHora OnAvanzarHora = delegate{};

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
        /// Avanza de hora en el rol
        /// </summary>
        public void AvanzarHora(int _minutos)
        {
            int nuevaHora = modelo.Hora + _minutos;

            OnAvanzarHora(ref nuevaHora);

            modelo.Hora = nuevaHora;
        }

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
