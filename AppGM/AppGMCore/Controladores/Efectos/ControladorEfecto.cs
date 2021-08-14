using System.Collections.Generic;
using System.Linq;
using CoolLogs;

namespace AppGM.Core
{
	public abstract class ControladorEfectoBase<TModelo> : Controlador<TModelo>
		where TModelo : ModeloBase, new()
	{
		/// <summary>
		/// Funcion que nos permite saber si el <see cref="ModeloEfecto"/> puede aplicarse desde determinado <see cref="ControladorPersonaje"/>
		/// a determinados <see cref="ControladorPersonaje"/>
		/// </summary>
		public virtual ControladorFuncion_Predicado FnPuedeAplicarEfecto { get; protected set; }

        /// <summary>
        /// Funcion que aplica el <see cref="ModeloEfecto"/> desde cierto <see cref="ControladorPersonaje"/> a otros <see cref="ControladorPersonaje"/>
        /// </summary>
        public virtual ControladorFuncion_Efecto FnAplicarEfecto { get; protected set; }

        /// <summary>
        /// Funcion que quita el <see cref="ModeloEfecto"/> desde cierto <see cref="ControladorPersonaje"/> a otros <see cref="ControladorPersonaje"/>
        /// </summary>
        public virtual ControladorFuncion_Efecto FnQuitarEfecto { get; protected set; }

		public ControladorEfectoBase(TModelo _modelo)
			:base(_modelo){}

        [AccesibleEnGuraScratch(nameof(ModificarComportamientoAcumulativo))]
		public abstract void ModificarComportamientoAcumulativo(EComportamientoAcumulativo nuevoComportamiento, EModoDeCambioDeComportamiento modoDeCambio);
	}


    /// <summary>
    /// Constrolador de un efecto
    /// </summary>
    //TODO: Añadir una funcion para sacar este efecto a todos sus afectados
    public class ControladorEfecto : ControladorEfectoBase<ModeloEfecto>
    {
        #region Eventos

        /// <summary>
        /// Representa un metodo que lidia con eventos de aplicar efecto
        /// </summary>
        /// <param name="instigador">Quien quiere aplicar el efecto</param>
        /// <param name="objetivo">A quien se le aplicara el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser aplicado</param>
        public delegate void dAplicarEfecto(ControladorPersonaje instigador, ControladorPersonaje objetivo,
            ControladorEfecto efectoAplicado);

        /// <summary>
        /// Representa un metodo que lidia con eventos de reducir efecto
        /// </summary>
        /// <param name="instigador">Quien quiere reducir el efecto</param>
        /// <param name="objetivos">A quienes se les reduce el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser reducido</param>
        public delegate void dReducirTurno(ControladorPersonaje instigador,
            ControladorPersonaje objetivos, ControladorEfecto efectoAplicado);

        /// <summary>
        /// Representa un metodo que lidia con eventos de quitar efecto
        /// </summary>
        /// <param name="instigador">Quien quiere quitar el efecto</param>
        /// <param name="objetivo">A quien se les quita el efecto</param>
        /// <param name="efectoAplicado">Efecto a ser quitado</param>
        public delegate void dQuitarEfecto(ControladorPersonaje instigador, ControladorPersonaje objetivo,
            ControladorEfecto efectoAplicado);

        /// <summary>
        /// Evento que se dispara cuando se aplica el efecto
        /// </summary>
        public event dAplicarEfecto OnAplicarEfecto = delegate { };

        /// <summary>
        /// Evento que se dispara cuando se reduce el efecto
        /// </summary>
        public event dReducirTurno OnReducirEfecto = delegate { };

        /// <summary>
        /// Evento que se dispara cuando se quita el efecto
        /// </summary>
        public event dQuitarEfecto OnQuitarEfecto = delegate { };

        #endregion

        #region Propiedades & Campos

        [AccesibleEnGuraScratch(nameof(AplicacionesEfecto))]
        private List<ControladorEfectoSiendoAplicado> AplicacionesEfecto;

        private Dictionary<ControladorPersonaje, List<ControladorEfectoSiendoAplicado>> mAplicacionesEfectoSolapantes;

        private Dictionary<ControladorPersonaje, Dictionary<EComportamientoAcumulativo, ControladorEfectoSiendoAplicado>> mAplicacionesEfectoNoSolapantes;

        private Dictionary<ControladorPersonaje, List<ControladorEfectoSiendoAplicado>> AplicacionesEfectoSolapantes
        {
	        get
	        {
		        mAplicacionesEfectoSolapantes ??= new Dictionary<ControladorPersonaje, List<ControladorEfectoSiendoAplicado>>();

		        return mAplicacionesEfectoSolapantes;
	        }
        }

        private Dictionary<ControladorPersonaje, Dictionary<EComportamientoAcumulativo, ControladorEfectoSiendoAplicado>> AplicacionesEfectoNoSolapantes
        {
	        get
	        {
		        mAplicacionesEfectoNoSolapantes ??= new Dictionary<ControladorPersonaje, Dictionary<EComportamientoAcumulativo, ControladorEfectoSiendoAplicado>>();

				return mAplicacionesEfectoNoSolapantes;
	        }
        }

        /// <summary>
        /// Obtiene o establece el valor de <see cref="ModeloEfecto.Nombre"/>
        /// </summary>
        [AccesibleEnGuraScratch(nameof(NombreEfecto))]
        public string NombreEfecto
        {
	        get => modelo.Nombre;
	        set => modelo.Nombre = value;
        }

        /// <summary>
        /// Obtiene o establece el valor de <see cref="ModeloEfecto.Tipo"/>
        /// </summary>
        [AccesibleEnGuraScratch(nameof(TipoEfecto))]
        public ETipoEfecto TipoEfecto
        {
	        get => modelo.Tipo;
	        set => modelo.Tipo = value;
        }

        public EComportamientoAcumulativo ComportamientoAcumulativo
        {
	        get => modelo.ComportamientoAcumulativo;
	        set => ModificarComportamientoAcumulativo(value, EModoDeCambioDeComportamiento.SumarTurnosRestantes);
        }

        /// <summary>
        /// Obtiene o establece el valor de <see cref="ModeloEfecto.TurnosDeDuracion"/>
        /// </summary>
        [AccesibleEnGuraScratch(nameof(TurnosDeDuracion))]
        public int TurnosDeDuracion
        {
	        get => modelo.TurnosDeDuracion;
	        set => modelo.TurnosDeDuracion = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_modeloEfecto"><see cref="ModeloEfecto"/> que representa</param>
        public ControladorEfecto(ModeloEfecto _modeloEfecto)
	        : base(_modeloEfecto)
        {
	        foreach (var aplicacion in modelo.Aplicaciones)
	        {
		        var instigador =
			        SistemaPrincipal.DatosRolSeleccionado.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(
				        aplicacion.EfectoAplicandose.Instigador.PersonajeInstigador, true);

		        var objetivo = 
			        SistemaPrincipal.DatosRolSeleccionado.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(
				        aplicacion.EfectoAplicandose.Instigador.PersonajeInstigador, true);

		        AplicarEfecto(instigador, objetivo, aplicacion.EfectoAplicandose.ComportamientoAcumulativo);
	        }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Chequea que el efecto pueda ser aplicado 
        /// </summary>
        /// <param name="usuario">Personaje que intenta aplicar el efecto</param>
        /// <param name="objetivos">Personaje/s a quienes se intenta aplicar el efecto</param>
        /// <returns></returns>
        public bool PuedeAplicarEfecto(ControladorPersonaje usuario, ControladorPersonaje objetivos)
        {
	        //return mPuedeAplicarEfecto(this, usuario, objetivos);
	        return false;
        }

        /// <summary>
        /// Aplica el efecto sobre el personaje
        /// </summary>
        /// <param name="usuario">Personaje que aplicara el efecto</param>
        /// <param name="objetivo">Personaje/s a quienes se les aplicara el efecto</param>
        /// <param name="comportamientoAcumulativoSeleccionado">Comportamiento acumulativo que se utilizara en esta aplicacion particular del efecto.
        /// El valor de este parametro sera ignorado a menos que <see cref="ComportamientoAcumulativo"/> sea <see cref="EComportamientoAcumulativo.SeleccionManual"/></param>
        public void AplicarEfecto(ControladorPersonaje usuario, ControladorPersonaje objetivo, EComportamientoAcumulativo comportamientoAcumulativoSeleccionado = EComportamientoAcumulativo.NINGUNO)
        {
	        ControladorEfectoSiendoAplicado nuevoEfectoSiendoAplicado = null;

	        //Si el comportamiento acumulativo no es de seleccion manual entonces utilizamos ese, si lo es entonces utilizamos el pasado en el parametro
            var comportamientoAcumulativoActual =
		        ComportamientoAcumulativo != EComportamientoAcumulativo.SeleccionManual
			        ? ComportamientoAcumulativo
			        : comportamientoAcumulativoSeleccionado;

            switch (comportamientoAcumulativoActual)
	        {
		        case EComportamientoAcumulativo.Solapar:
		        {
			        nuevoEfectoSiendoAplicado = new ControladorEfectoSiendoAplicado(this, usuario, objetivo);

			        AplicacionesEfecto.Add(nuevoEfectoSiendoAplicado);
                    
                    if(!AplicacionesEfectoSolapantes.ContainsKey(objetivo))
                        AplicacionesEfectoSolapantes.Add(objetivo, new List<ControladorEfectoSiendoAplicado>());

                    AplicacionesEfectoSolapantes[objetivo].Add(nuevoEfectoSiendoAplicado);

                    AplicacionesEfectoSolapantes[objetivo].ForEach(e => e.ContadorAcumulaciones = AplicacionesEfectoSolapantes[objetivo].Count);

                    break;
		        }

		        case EComportamientoAcumulativo.Contar:
		        case EComportamientoAcumulativo.Esperar:
		        case EComportamientoAcumulativo.SumarTurnos:
		        {
			        if (!AplicacionesEfectoNoSolapantes[objetivo].ContainsKey(comportamientoAcumulativoActual))
			        {
				        nuevoEfectoSiendoAplicado = new ControladorEfectoSiendoAplicado(this, usuario, objetivo);

				        AplicacionesEfectoNoSolapantes[objetivo][comportamientoAcumulativoActual] = nuevoEfectoSiendoAplicado;
			        }
			        else
			        {
				        AplicacionesEfectoNoSolapantes[objetivo][comportamientoAcumulativoActual].AñadirAcumulacion();

                    }

			        break;
		        }

		        default:
		        {
                    SistemaPrincipal.LoggerGlobal.Log($"{comportamientoAcumulativoActual} no soportado!", ESeveridad.Error);

                    break;
		        }
	        }

            if (nuevoEfectoSiendoAplicado != null)
            {
	            modelo.Aplicaciones.Add(nuevoEfectoSiendoAplicado.modelo.Efecto);

	            nuevoEfectoSiendoAplicado.OnEfectoQuitado += QuitarEfecto;
            }

            OnAplicarEfecto(usuario, objetivo, this);
        }

        /// <summary>
        /// Aplica el efecto sobre el personaje
        /// </summary>
        /// <param name="usuario">Personaje que origino el efecto</param>
        /// <param name="objetivos">Personaje/s a quienes se les quitara el efecto</param>
        public virtual void QuitarEfecto(ControladorEfectoSiendoAplicado efecto)
        {
	        if (efecto.ComportamientoAcumulativo == EComportamientoAcumulativo.Solapar)
	        {
		        AplicacionesEfectoSolapantes[efecto.objetivo].Remove(efecto);
	        }
	        else
	        {
		        AplicacionesEfectoNoSolapantes[efecto.objetivo].Remove(efecto.ComportamientoAcumulativo);
	        }

	        OnQuitarEfecto(efecto.instigador, efecto.objetivo, this);
        }

        public override void ModificarComportamientoAcumulativo(
	        EComportamientoAcumulativo nuevoComportamiento,
	        EModoDeCambioDeComportamiento modoDeCambio)
        {
	        if (nuevoComportamiento == ComportamientoAcumulativo)
		        return;

	        switch (modoDeCambio)
	        {
		        case var val 
			        when (val & EModoDeCambioDeComportamiento.EliminarAplicacionesConElComportamientoAnterior) != 0:
		        {
			        AplicacionesEfecto.ForEach(a =>
			        {
				        if (a.ComportamientoAcumulativo == ComportamientoAcumulativo)
					        a.QuitarEfecto();
			        });

			        break;
		        }

		        case var val 
			        when (val & EModoDeCambioDeComportamiento.EliminarAplicacionesConComportamientoDistinto) != 0:
		        {
			        AplicacionesEfecto.ForEach(a =>
			        {
				        a.QuitarEfecto();
			        });

			        break;
		        }

		        case var val 
			        when (val & EModoDeCambioDeComportamiento.ModificarAplicacionesActivas) != 0:
		        {
			        AplicacionesEfecto.ForEach(a =>
			        {
				        a.ModificarComportamientoAcumulativo(nuevoComportamiento, modoDeCambio);
			        });

			        break;
		        }

		        case var val 
			        when (val & EModoDeCambioDeComportamiento.ModificarAplicacionesActivasConElComportamientoAnterior) != 0:
		        {
			        AplicacionesEfecto.ForEach(a =>
			        {
                        if(a.ComportamientoAcumulativo == ComportamientoAcumulativo)
							a.ModificarComportamientoAcumulativo(nuevoComportamiento, modoDeCambio);
			        });

			        break;
		        }

		        default:
		        {
                    SistemaPrincipal.LoggerGlobal.Log($"{modoDeCambio.ToString()} no contiene ninguna flag reelevante para este metodo", ESeveridad.Advertencia);

                    break;
		        }
	        }
        }

        public override void ActulizarModelo(ModeloEfecto nuevoModelo)
        {
	        if (nuevoModelo == null)
	        {
                SistemaPrincipal.LoggerGlobal.Log($"{nameof(nuevoModelo)} fue null!", ESeveridad.Error);

                return;
	        }

            //Obtenemos una copia de las aplicaciones del efecto del nuevo modelo
	        var aplicacionesNuevoModelo = new List<ModeloEfectoSiendoAplicado>(nuevoModelo.Aplicaciones.Select(a => a.EfectoAplicandose));

            //Por cada aplicacion en el efecto actual...
	        AplicacionesEfecto.ForEach(a =>
	        {
                //Intentamos obtener el indice de la aplicacion actual en la lista de nuevas aplicaciones
                int indiceAplicacionActual = aplicacionesNuevoModelo.IndexOf(a.AplicacionEfecto);

                //Si no se encontro entonces significa que se quito la aplicacion, por lo que la eliminamos del modleo actual y de la base de datos
                if (indiceAplicacionActual == -1)
                {
	                a.Eliminar();
                }
                //Si se encontro entonces solamente acutalizamos su modelo
                else
		        {
                    a.ActulizarModelo(aplicacionesNuevoModelo[indiceAplicacionActual]);
		        }
	        });

            //Actualizamos las funciones del efecto
	        FnPuedeAplicarEfecto.ActulizarModelo(nuevoModelo.FuncionPuedeAplicar.Funcion);
            FnAplicarEfecto.ActulizarModelo(nuevoModelo.FuncionAplicar.Funcion);
            FnQuitarEfecto.ActulizarModelo(nuevoModelo.FuncionQuitar.Funcion);

            //Actualizamos las funciones de las aplicaciones
	        AplicacionesEfecto.ForEach(ef =>
	        {
                ef.FnPuedeAplicarEfecto.ActulizarModelo(nuevoModelo.FuncionPuedeAplicar?.Funcion);
                ef.FnAplicarEfecto.ActulizarModelo(nuevoModelo.FuncionAplicar?.Funcion);
                ef.FnQuitarEfecto.ActulizarModelo(nuevoModelo.FuncionQuitar?.Funcion);
	        });

	        modelo.Nombre      = nuevoModelo.Nombre;
	        modelo.Tipo        = nuevoModelo.Tipo;
	        modelo.Descripcion = nuevoModelo.Descripcion;
	        modelo.TurnosDeDuracion = nuevoModelo.TurnosDeDuracion;
        }

        #endregion
    }
}