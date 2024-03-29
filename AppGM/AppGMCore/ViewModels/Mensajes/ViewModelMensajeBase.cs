﻿namespace AppGM.Core
{
    /// <summary>
    /// VM base para crear un mensaje (popup)
    /// </summary>
    public class ViewModelMensajeBase : ViewModel
    {
        /// <summary>
        /// Color del fondo de la ventana
        /// </summary>
	    public string ColorFondo { get; set; } = "000000";

        /// <summary>
        /// Instancia de la <see cref="IVentana"/>.
        /// Solamente es un atajo a <see cref="ViewModelAplicacion.VentanaMensajePrincipal"/> en <see cref="SistemaPrincipal.Aplicacion"/>
        /// </summary>
        protected IVentana mVentana => SistemaPrincipal.Aplicacion.VentanaMensajePrincipal;
    }
}
