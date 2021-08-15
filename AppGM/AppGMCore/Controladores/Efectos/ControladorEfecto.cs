using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
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

        //---------------------------------------------CAMPOS----------------------------------------

        /// <summary>
        /// Contiene todas las aplicaciones de este efecto
        /// </summary>
        [AccesibleEnGuraScratch(nameof(AplicacionesEfectoGlobal))]
        private List<ControladorEfectoSiendoAplicado> AplicacionesEfectoGlobal = new List<ControladorEfectoSiendoAplicado>();

        /// <summary>
        /// Contiene los efectos solapantes aplicados a cierto <see cref="ControladorPersonaje"/>
        /// </summary>
        private Dictionary<ControladorPersonaje, List<ControladorEfectoSiendoAplicado>> mAplicacionesEfectoSolapantes;

        /// <summary>
        /// Contiene todos los efectos que no sean solapantes aplicados a cierto <see cref="ControladorPersonaje"/>
        /// </summary>
        private Dictionary<ControladorPersonaje, Dictionary<EComportamientoAcumulativo, ControladorEfectoSiendoAplicado>> mAplicacionesEfectoNoSolapantes;


        //-------------------------------------------PROPIEDADES-----------------------------------------------

        /// <summary>
        /// Propiedad para incializar <see cref="mAplicacionesEfectoSolapantes"/> perezosamente
        /// </summary>
        private Dictionary<ControladorPersonaje, List<ControladorEfectoSiendoAplicado>> AplicacionesEfectoSolapantes
        {
	        get
	        {
		        mAplicacionesEfectoSolapantes ??= new Dictionary<ControladorPersonaje, List<ControladorEfectoSiendoAplicado>>();

		        return mAplicacionesEfectoSolapantes;
	        }
        }

        /// <summary>
        /// Propiedad para incializar <see cref="mAplicacionesEfectoNoSolapantes"/> perezosamente
        /// </summary>
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

        /// <summary>
        /// Obtiene o establece el valor de <see cref="ModeloEfecto.ComportamientoAcumulativo"/>
        /// </summary>
        public EComportamientoAcumulativo ComportamientoAcumulativo
        {
	        get => modelo.ComportamientoAcumulativo;
	        set => ModificarComportamientoAcumulativo(value, EModoDeCambioDeComportamientoAcumulativo.SumarTurnosRestantes);
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
			        SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(
				        aplicacion.EfectoAplicandose.Instigador.PersonajeInstigador, true);

		        var objetivo = 
			        SistemaPrincipal.ObtenerControlador<ControladorPersonaje, ModeloPersonaje>(
				        aplicacion.EfectoAplicandose.Instigador.PersonajeInstigador, true);

		        AplicarEfecto(instigador, objetivo, aplicacion.EfectoAplicandose.ComportamientoAcumulativo, aplicacion.EfectoAplicandose);
	        }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Compila todas las funciones requeridas para este efecto de manera asincronica
        /// </summary>
        /// <returns></returns>
        public async Task CompilarFuncionesAsync()
        {
	        List<ControladorFuncionBase> controladoresCreados = new List<ControladorFuncionBase>(modelo.Funciones.Count);

	        foreach (var func in modelo.Funciones)
	        {
		        var controladorCreado = InicializarFuncion(func.Funcion, func.TipoFuncion);

		        await controladorCreado.CompilarAsync();
	        }
        }

        /// <summary>
        /// Revisa si este efecto puede ser aplicado a un <paramref name="objetivo"/> por <paramref name="usuario"/>
        /// </summary>
        /// <param name="usuario">Personaje que intenta aplicar el efecto</param>
        /// <param name="objetivo">Personaje a quienes se intenta aplicar el efecto</param>
        /// <returns><see cref="bool"/> indicando si el efecto puede ser aplicado al <paramref name="objetivo"/></returns>
        public bool PuedeAplicarEfecto(ControladorPersonaje usuario, ControladorPersonaje objetivo)
        {
	        return FnPuedeAplicarEfecto.Funcion(this, usuario, objetivo, FnPuedeAplicarEfecto);
        }

        /// <summary>
        /// Aplica el efecto sobre el personaje
        /// </summary>
        /// <param name="usuario">Personaje que aplicara el efecto</param>
        /// <param name="objetivo">Personaje/s a quienes se les aplicara el efecto</param>
        /// <param name="comportamientoAcumulativoSeleccionado">Comportamiento acumulativo que se utilizara en esta aplicacion particular del efecto.
        /// El valor de este parametro sera ignorado a menos que <see cref="ComportamientoAcumulativo"/> sea <see cref="EComportamientoAcumulativo.SeleccionManual"/></param>
        /// <param name="modeloEfectoSiendoAplicado">Modelo a partir del cual crear el nuevo <see cref="ControladorEfectoSiendoAplicado"/></param>
        public void AplicarEfecto(ControladorPersonaje usuario, ControladorPersonaje objetivo, EComportamientoAcumulativo comportamientoAcumulativoSeleccionado = EComportamientoAcumulativo.NINGUNO, ModeloEfectoSiendoAplicado modeloEfectoSiendoAplicado = null)
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
                    //Si el comportamiento es solapar entonces tenemos que crear un nuevo controlador si o si
			        nuevoEfectoSiendoAplicado = modeloEfectoSiendoAplicado == null 
				        ? new ControladorEfectoSiendoAplicado(this, usuario, objetivo)
				        : new ControladorEfectoSiendoAplicado(modeloEfectoSiendoAplicado, this, usuario, objetivo);

			        AplicacionesEfectoGlobal.Add(nuevoEfectoSiendoAplicado);
                    
                    //Si el diccionario de aplicaciones solapantes no contiene una entrada para este objetivo entonces creamos una
                    if(!AplicacionesEfectoSolapantes.ContainsKey(objetivo))
                        AplicacionesEfectoSolapantes.Add(objetivo, new List<ControladorEfectoSiendoAplicado>());

                    AplicacionesEfectoSolapantes[objetivo].Add(nuevoEfectoSiendoAplicado);

                    //Actualizamos las acumulaciones de los efectos solapantes ya aplicados
                    AplicacionesEfectoSolapantes[objetivo].ForEach(e => e.ContadorAcumulaciones = AplicacionesEfectoSolapantes[objetivo].Count);

                    break;
		        }

		        case EComportamientoAcumulativo.Contar:
		        case EComportamientoAcumulativo.Esperar:
		        case EComportamientoAcumulativo.SumarTurnos:
		        {
			        if (!AplicacionesEfectoNoSolapantes[objetivo].ContainsKey(comportamientoAcumulativoActual))
			        {
				        nuevoEfectoSiendoAplicado = modeloEfectoSiendoAplicado == null
					        ? new ControladorEfectoSiendoAplicado(this, usuario, objetivo)
					        : new ControladorEfectoSiendoAplicado(modeloEfectoSiendoAplicado, this, usuario, objetivo);

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
        /// <param name="efecto">Controlador de la aplicacion que quitaremos</param>
        public virtual void QuitarEfecto(ControladorEfectoSiendoAplicado efecto)
        {
	        if (efecto.ComportamientoAcumulativo == EComportamientoAcumulativo.Solapar)
	        {
		        AplicacionesEfectoSolapantes[efecto.objetivo].Remove(efecto);

                //Actualizamos el contador de acumulaciones de los efectos restantes
		        AplicacionesEfectoSolapantes[efecto.objetivo].ForEach(e => --e.ContadorAcumulaciones);

            }
	        else
	        {
                //Quitamos de una la entrada en el diccionario ya que si el comportamiento no es acumulativo solamente hay un controlador
		        AplicacionesEfectoNoSolapantes[efecto.objetivo].Remove(efecto.ComportamientoAcumulativo);
	        }

	        OnQuitarEfecto(efecto.instigador, efecto.objetivo, this);
        }

        /// <summary>
        /// Modifica el <see cref="EComportamientoAcumulativo"/> de los <see cref="ControladorEfectoSiendoAplicado"/>
        /// creados a partir de esta instancia
        /// </summary>
        /// <param name="nuevoComportamiento">Nuevo comportamiento</param>
        /// <param name="modoDeCambio">Manera en la que se realizara el cambio de comportamiento</param>
        public override void ModificarComportamientoAcumulativo(
	        EComportamientoAcumulativo nuevoComportamiento,
	        EModoDeCambioDeComportamientoAcumulativo modoDeCambio)
        {
            //Si el nuevo comportamiento es igual al actual nos pegamos media vuelta
	        if (nuevoComportamiento == ComportamientoAcumulativo)
		        return;

	        switch (modoDeCambio)
	        {
		        case var val 
			        when (val & EModoDeCambioDeComportamientoAcumulativo.EliminarAplicacionesConElComportamientoAnterior) != 0:
		        {
			        AplicacionesEfectoGlobal.ForEach(a =>
			        {
				        if (a.ComportamientoAcumulativo == ComportamientoAcumulativo)
					        a.QuitarEfecto();
			        });

			        break;
		        }

		        case var val 
			        when (val & EModoDeCambioDeComportamientoAcumulativo.EliminarAplicacionesConComportamientoDistinto) != 0:
		        {
			        AplicacionesEfectoGlobal.ForEach(a =>
			        {
				        a.QuitarEfecto();
			        });

			        break;
		        }

		        case var val 
			        when (val & EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivas) != 0:
		        {
			        AplicacionesEfectoGlobal.ForEach(a =>
			        {
				        a.ModificarComportamientoAcumulativo(nuevoComportamiento, modoDeCambio);
			        });

			        break;
		        }

		        case var val 
			        when (val & EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivasConElComportamientoAnterior) != 0:
		        {
			        AplicacionesEfectoGlobal.ForEach(a =>
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

        public override void ActulizarModelo(ModeloEfecto nuevoModelo, bool eliminarSiNuevoModeloEsNull = false)
        {
	        base.ActulizarModelo(nuevoModelo, eliminarSiNuevoModeloEsNull);

	        var nuevaFuncionPuedeAplicar = nuevoModelo.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionPuedeAplicar);
	        var nuevaFuncionAplicar      = nuevoModelo.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionAplicar);
	        var nuevaFuncionQuitar       = nuevoModelo.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionQuitar);

            //Obtenemos una copia de las aplicaciones del efecto del nuevo modelo
	        var aplicacionesNuevoModelo = new List<ModeloEfectoSiendoAplicado>(nuevoModelo.Aplicaciones.Select(a => a.EfectoAplicandose));

            //Por cada aplicacion en el efecto actual...
	        AplicacionesEfectoGlobal.ForEach(a =>
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
	        FnPuedeAplicarEfecto.ActulizarModelo(nuevaFuncionPuedeAplicar?.Funcion);
            FnAplicarEfecto.ActulizarModelo(nuevaFuncionAplicar?.Funcion);
            FnQuitarEfecto.ActulizarModelo(nuevaFuncionQuitar?.Funcion);

            //Actualizamos las funciones de las aplicaciones
	        AplicacionesEfectoGlobal.ForEach(ef =>
	        {
                ef.FnPuedeAplicarEfecto.ActulizarModelo(nuevaFuncionPuedeAplicar?.Funcion, true);
                ef.FnAplicarEfecto.ActulizarModelo(nuevaFuncionAplicar?.Funcion, true);
                ef.FnQuitarEfecto.ActulizarModelo(nuevaFuncionQuitar?.Funcion, true);
	        });

	        modelo.Nombre      = nuevoModelo.Nombre;
	        modelo.Tipo        = nuevoModelo.Tipo;
	        modelo.Descripcion = nuevoModelo.Descripcion;
	        modelo.TurnosDeDuracion = nuevoModelo.TurnosDeDuracion;
        }

        #endregion
    }
}