using System.Collections.Generic;
using System.Linq;

namespace AppGM.Core
{
    /// <summary>
    /// Clase encargada de crear <see cref="ModeloBase"/>
    /// </summary>
    public static class Creador
    {
        //TODO: Expandir
        public static ModeloPersonaje CrearPersonaje(ETipoPersonaje tipo)
        {
            switch (tipo)
            {
                case ETipoPersonaje.Master:
                    ModeloMaster nuevoMaster = new ModeloMaster
                    {
                        TipoPersonaje = ETipoPersonaje.Master
                    };

                    return nuevoMaster;

                case ETipoPersonaje.Servant:
                    ModeloServant nuevoServant = new ModeloServant
                    {
                        TipoPersonaje = ETipoPersonaje.Servant
                    };

                    return nuevoServant;

                case ETipoPersonaje.Invocacion:
                    ModeloInvocacion nuevaInvocacion = new ModeloInvocacion
                    {
                        TipoPersonaje = ETipoPersonaje.Invocacion
                    };

                    return  nuevaInvocacion;

                case ETipoPersonaje.NPC:
                    ModeloPersonaje nuevoPersonaje = new ModeloPersonaje
                    {
                        TipoPersonaje = ETipoPersonaje.NPC
                    };

                    return nuevoPersonaje;

                default:
                    return null;

            }
        }

        public static TIPersonajeEfectoSiendoAplicado CrearTIPersonaje(
	        this ControladorEfectoSiendoAplicado efecto,
	        ControladorPersonaje personaje)
        {
	        return new TIPersonajeEfectoSiendoAplicado
	        {
		        EfectoSiendoAplicado = efecto.modelo,
		        Personaje            = personaje.modelo
	        };
        }

        /// <summary>
        /// Inicializa una instancia de <see cref="ModeloEfectoSiendoAplicado"/>
        /// </summary>
        /// <param name="efectoSiendoAplicado">Modelo a inicializar</param>
        /// <param name="efecto">Efecto que representa</param>
        /// <param name="instigador"><see cref="ControladorPersonaje"/> que causo el efecto</param>
        /// <param name="objetivos"><see cref="ControladorPersonaje"/> a los que se les aplica el efecto</param>
        /// <param name="añadirABaseDeDatos">Indica si añadir este modelo a la base de datos</param>
        public static void Inicializar(this ModeloEfectoSiendoAplicado efectoSiendoAplicado, ControladorEfecto efecto,
	        ControladorPersonaje instigador, List<ControladorPersonaje> objetivos, bool añadirABaseDeDatos = true)
        {
	        //efectoSiendoAplicado.Efecto = new TIEfectoSiendoAplicadoEfecto
	        //{
		       // Efecto = efecto.modelo,
		       // EfectoAplicandose = efectoSiendoAplicado
	        //};

	        //efectoSiendoAplicado.Instigador =  new TIEfectoSiendoAplicadoPersonajeInstigador
	        //{
		       // EfectoAplicandose   = efectoSiendoAplicado,
		       // PersonajeInstigador = instigador.modelo
	        //};

	        //efectoSiendoAplicado.Objetivos = new List<TIEfectoSiendoAplicadoPersonajeObjetivo>(
		       // from personaje in objetivos
		       // select new TIEfectoSiendoAplicadoPersonajeObjetivo
		       // {
			      //  EfectoAplicandose = efectoSiendoAplicado,
			      //  PersonajeObjetivo = personaje.modelo
		       // });

	        //efectoSiendoAplicado.TurnosRestantes = efecto.modelo.TurnosDeDuracion;
	        //efectoSiendoAplicado.EstaSiendoAplicado = false;

         //   //Si debemos guardar el modelo...
	        //if (añadirABaseDeDatos)
	        //{
		       // SistemaPrincipal.GuardarModelo(efectoSiendoAplicado);

         //       SistemaPrincipal.GuardarModelo(efectoSiendoAplicado.Efecto);
         //       SistemaPrincipal.GuardarModelo(efectoSiendoAplicado.Instigador);

         //       foreach (var obj in efectoSiendoAplicado.Objetivos)
	        //        SistemaPrincipal.GuardarModelo(obj);

         //       SistemaPrincipal.GuardarDatosRol();
	        //}
        }
    }
}