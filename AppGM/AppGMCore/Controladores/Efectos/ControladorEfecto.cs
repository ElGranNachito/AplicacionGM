using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
    /// Constrolador de un efecto
    /// </summary>
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
        public event dAplicarEfecto OnEfectoAplicado = delegate { };

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
        private HashSet<ControladorEfectoSiendoAplicado> AplicacionesEfectoGlobal = new HashSet<ControladorEfectoSiendoAplicado>();

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
	        return FnPuedeAplicarEfecto.EjecutarFuncion(null, this, usuario, objetivo).resultadoFuncion;
        }

        /// <summary>
        /// Aplica el efecto sobre el personaje
        /// </summary>
        /// <param name="usuario">Personaje que aplicara el efecto</param>
        /// <param name="objetivo">Personaje/s a quienes se les aplicara el efecto</param>
        /// <param name="comportamientoAcumulativoSeleccionado">Comportamiento acumulativo que se utilizara en esta aplicacion particular del efecto.
        /// El valor de este parametro sera ignorado a menos que <see cref="ComportamientoAcumulativo"/> sea <see cref="EComportamientoAcumulativo.SeleccionManual"/></param>
        /// <param name="modeloEfectoSiendoAplicado">Modelo a partir del cual crear el nuevo <see cref="ControladorEfectoSiendoAplicado"/></param>
        /// <returns>Controlador de la aplicacion del efecto creada o null</returns>
        public ControladorEfectoSiendoAplicado AplicarEfecto(
	        ControladorPersonaje usuario,
	        ControladorPersonaje objetivo,
	        EComportamientoAcumulativo comportamientoAcumulativoSeleccionado = EComportamientoAcumulativo.NINGUNO,
	        ModeloEfectoSiendoAplicado modeloEfectoSiendoAplicado = null)
        {
	        ControladorEfectoSiendoAplicado nuevoEfectoSiendoAplicado = null;

	        //Si el comportamiento acumulativo no es de seleccion manual entonces utilizamos ese, si lo es entonces utilizamos el pasado en el parametro
            var comportamientoAcumulativoActual =
		        ComportamientoAcumulativo == EComportamientoAcumulativo.SeleccionManual || comportamientoAcumulativoSeleccionado != EComportamientoAcumulativo.NINGUNO
			        ? comportamientoAcumulativoSeleccionado
                    : ComportamientoAcumulativo;

			//Si el comportamiento del efecto que sera aplicado es solapar o es de cualquier otro tipo y no hay ninguna aplicacion existente sobre
			//el objetivo con ese comportamiento...
            if (comportamientoAcumulativoActual == EComportamientoAcumulativo.Solapar ||
                !AplicacionesEfectoNoSolapantes[objetivo].ContainsKey(comportamientoAcumulativoActual))
            {
				//Creamos un nuevo controlador para la nueva aplicacion del efecto
	            nuevoEfectoSiendoAplicado = modeloEfectoSiendoAplicado == null
		            ? new ControladorEfectoSiendoAplicado(this, usuario, objetivo)
		            : new ControladorEfectoSiendoAplicado(modeloEfectoSiendoAplicado, this, usuario, objetivo);
            }

            AñadirEfecto(nuevoEfectoSiendoAplicado, objetivo, comportamientoAcumulativoActual);
			
			//Si el controlador de la nueva aplicacion del efecto no es null...
            if (nuevoEfectoSiendoAplicado != null)
            {
				//Añadimos el modelo a la lista de aplicaciones
	            modelo.Aplicaciones.Add(nuevoEfectoSiendoAplicado.modelo.Efecto);

				//Nos subscribimos al eveto de quitar efecto
	            nuevoEfectoSiendoAplicado.OnEfectoQuitado += QuitarEfecto;
            }

			//Disparamos el evento de efecto aplicado
            OnEfectoAplicado(usuario, objetivo, this);

            return nuevoEfectoSiendoAplicado;
        }

		/// <summary>
		/// Añade una nueva aplicacion del efecto y/o aumenta las acumulacion de las aplicaciones existentes
		/// </summary>
		/// <param name="efecto">Controlador de la aplicacion del efecto que sera añadida</param>
		/// <param name="objetivo">Objetivo de la aplicacion</param>
		/// <param name="comportamientoAcumulativo">Comportamiento acumulativo que se le dara al <paramref name="efecto"/></param>
        public void AñadirEfecto(ControladorEfectoSiendoAplicado efecto, ControladorPersonaje objetivo, EComportamientoAcumulativo comportamientoAcumulativo)
        {
	        if (efecto == null)
	        {
		        if(comportamientoAcumulativo == EComportamientoAcumulativo.Solapar)
                {
                    SistemaPrincipal.LoggerGlobal.Log($"{nameof(efecto)} no puede ser null cuando el comportamiento acumulativo es {nameof(EComportamientoAcumulativo.Solapar)}", ESeveridad.Error);

                    return;
                }
                else if (!mAplicacionesEfectoNoSolapantes[objetivo].ContainsKey(comportamientoAcumulativo))
		        {
			        SistemaPrincipal.LoggerGlobal.Log($@"{nameof(efecto)} no puede ser null porque no existe una instancia del efecto con el 
															comportamiento acumulativo especificado ({comportamientoAcumulativo})", ESeveridad.Error);

                    return;
		        }
            }

            switch (comportamientoAcumulativo)
	        {
		        case EComportamientoAcumulativo.Solapar:
		        {
			        if (efecto.ComportamientoAcumulativo != comportamientoAcumulativo)
			        {
				        efecto.ComportamientoAcumulativo = comportamientoAcumulativo;

				        return;
			        }

			        AplicacionesEfectoGlobal.Add(efecto);
                    
                    //Si el diccionario de aplicaciones solapantes no contiene una entrada para este objetivo entonces creamos una
                    if(!AplicacionesEfectoSolapantes.ContainsKey(objetivo))
                        AplicacionesEfectoSolapantes.Add(objetivo, new List<ControladorEfectoSiendoAplicado>());

                    AplicacionesEfectoSolapantes[objetivo].Add(efecto);

                    //Actualizamos las acumulaciones de los efectos solapantes ya aplicados
                    AplicacionesEfectoSolapantes[objetivo].ForEach(e => e.ContadorAcumulaciones = AplicacionesEfectoSolapantes[objetivo].Count);

                    break;
		        }

		        case EComportamientoAcumulativo.Contar:
		        case EComportamientoAcumulativo.Esperar:
		        case EComportamientoAcumulativo.SumarTurnos:
		        {
			        if (!AplicacionesEfectoNoSolapantes[objetivo].ContainsKey(comportamientoAcumulativo))
			        {
				        if (efecto.ComportamientoAcumulativo != comportamientoAcumulativo)
				        {
					        efecto.ComportamientoAcumulativo = comportamientoAcumulativo;

					        return;
				        }

				        AplicacionesEfectoNoSolapantes[objetivo][comportamientoAcumulativo] = efecto;
			        }
			        else
			        {
				        AplicacionesEfectoNoSolapantes[objetivo][comportamientoAcumulativo].AñadirAcumulacion(efecto);

						efecto?.QuitarEfecto();
			        }

			        break;
		        }

		        default:
		        {
                    SistemaPrincipal.LoggerGlobal.Log($"{comportamientoAcumulativo} no soportado!", ESeveridad.Error);

                    break;
		        }
	        }
        }

		/// <summary>
		/// Añade varios efectos a un <paramref name="objetivo"/>
		/// </summary>
		/// <param name="efectos">Controladores las aplicaciones del efecto seran añadidos</param>
		/// <param name="objetivo">Personaje al que se le aplicaran los <paramref name="efectos"/></param>
		/// <param name="comportamientoAcumulativo">Comportamiento acumulativo que se les dara a <paramref name="efectos"/></param>
		public void AñadirEfecto(ControladorPersonaje objetivo, List<ControladorEfectoSiendoAplicado> efectos, EComportamientoAcumulativo comportamientoAcumulativo)
		{
			foreach (var e in efectos)
			{
				AplicarEfecto(e.instigador, objetivo, comportamientoAcumulativo);
			}
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
        /// Devuelve una lista de solo sectura con todas las instancias solapantes de este efecto que se le estan aplicando a <paramref name="personaje"/>
        /// </summary>
        /// <param name="personaje"><see cref="ControladorPersonaje"/> al que se le estan aplicando los efectos</param>
        /// <returns>Lista inmutable que contiene todas las aplicaciones del efecto sobre <paramref name="personaje"/></returns>
        public IReadOnlyList<ControladorEfectoSiendoAplicado> ObtenerAplicacionesSolapantesDelEfecto(ControladorPersonaje personaje)
        {
            //Si no existe una entrada en el diccionario para el personaje pasado entonces devolvemos una lista vacia
	        if (!AplicacionesEfectoSolapantes.ContainsKey(personaje))
		        return new List<ControladorEfectoSiendoAplicado>();

	        return AplicacionesEfectoSolapantes[personaje];
        }

        /// <summary>
        /// Obtiene el <see cref="ControladorEfectoSiendoAplicado"/> para la instancia de este efecto siendo aplicada
        /// en <paramref name="personaje"/> con <paramref name="comportamientoAcumulativo"/>
        /// </summary>
        /// <param name="personaje"><see cref="ControladorPersonaje"/> al que se le estan aplicando los efectos</param>
        /// <param name="comportamientoAcumulativo"><see cref="EComportamientoAcumulativo"/> que tiene la aplicacion del efecto</param>
        /// <returns>Controlador de la aplicacion del efecto</returns>
        public ControladorEfectoSiendoAplicado ObtenerAplicacionNoSolapanteDelEfecto(ControladorPersonaje personaje, EComportamientoAcumulativo comportamientoAcumulativo)
        {
	        if (!AplicacionesEfectoNoSolapantes.ContainsKey(personaje) || !mAplicacionesEfectoNoSolapantes[personaje].ContainsKey(comportamientoAcumulativo))
		        return null;

	        return AplicacionesEfectoNoSolapantes[personaje][comportamientoAcumulativo];
        }

        /// <summary>
        /// Modifica el <see cref="EComportamientoAcumulativo"/> de los <see cref="ControladorEfectoSiendoAplicado"/>
        /// creados a partir de esta instancia
        /// </summary>
        /// <param name="nuevoComportamiento">Nuevo comportamiento</param>
        /// <param name="modoDeCambio">Manera en la que se realizara el cambio de comportamiento</param>
        [AccesibleEnGuraScratch(nameof(ModificarComportamientoAcumulativo))]
        public override void ModificarComportamientoAcumulativo(
	        EComportamientoAcumulativo nuevoComportamiento,
	        EModoDeCambioDeComportamientoAcumulativo modoDeCambio)
        {
            //Si el nuevo comportamiento es igual al actual nos pegamos media vuelta
	        if (nuevoComportamiento == ComportamientoAcumulativo)
		        return;

	        if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.EliminarAplicacionesConElComportamientoAnterior))
	        {
		        foreach (var aplicacion in AplicacionesEfectoGlobal)
		        {
			        if (aplicacion.ComportamientoAcumulativo == ComportamientoAcumulativo)
				        aplicacion.QuitarEfecto();
		        }
	        }
			else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.EliminarAplicaciones))
	        {
		        foreach (var aplicacion in AplicacionesEfectoGlobal)
		        {
			        aplicacion.QuitarEfecto();
		        }
			}
			else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivas))
	        {
		        foreach (var aplicacion in AplicacionesEfectoGlobal)
		        {
			        aplicacion.ModificarComportamientoAcumulativo(nuevoComportamiento, modoDeCambio);
		        }
			}
			else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivasConElComportamientoAnterior))
	        {
		        foreach (var aplicacion in AplicacionesEfectoGlobal)
		        {
			        if (aplicacion.ComportamientoAcumulativo == ComportamientoAcumulativo)
				        aplicacion.ModificarComportamientoAcumulativo(nuevoComportamiento, modoDeCambio);
		        }
	        }
			//Si no contiene ninguna de las flags anteriores entonces no hay nada que hacer en este metodo
	        else
	        {
		        SistemaPrincipal.LoggerGlobal.Log($"{modoDeCambio} no contiene ninguna flag reelevante para este metodo", ESeveridad.Advertencia);
	        }
        }

        public override void ActulizarModelo(ModeloEfecto nuevoModelo, bool eliminarSiNuevoModeloEsNull = false)
        {
	        base.ActulizarModelo(nuevoModelo, eliminarSiNuevoModeloEsNull);

	        var nuevaFuncionPuedeAplicar = nuevoModelo.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionPuedeAplicar);
	        var nuevaFuncionAplicar      = nuevoModelo.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionAplicar);
	        var nuevaFuncionQuitar       = nuevoModelo.Funciones.Find(f => f.TipoFuncion == ETipoFuncionEfecto.FuncionQuitar);

	        //Actualizamos las funciones del efecto
	        FnPuedeAplicarEfecto.ActulizarModelo(nuevaFuncionPuedeAplicar?.Funcion);
	        FnAplicarEfecto.ActulizarModelo(nuevaFuncionAplicar?.Funcion);
	        FnQuitarEfecto.ActulizarModelo(nuevaFuncionQuitar?.Funcion);

			//Obtenemos una copia de las aplicaciones del efecto del nuevo modelo
			var aplicacionesNuevoModelo = new List<ModeloEfectoSiendoAplicado>(nuevoModelo.Aplicaciones.Select(a => a.EfectoAplicandose));

	        //Por cada aplicacion en el efecto actual...
			foreach (var aplicacion in AplicacionesEfectoGlobal)
	        {
				//Intentamos obtener el indice de la aplicacion actual en la lista de nuevas aplicaciones
				int indiceAplicacionActual = aplicacionesNuevoModelo.IndexOf(aplicacion.AplicacionEfecto);

				//Si no se encontro entonces significa que se quito la aplicacion, por lo que la eliminamos del modleo actual y de la base de datos
				if (indiceAplicacionActual == -1)
				{
					aplicacion.Eliminar();
				}
				//Si se encontro entonces solamente acutalizamos su modelo
				else
				{
					aplicacion.ActulizarModelo(aplicacionesNuevoModelo[indiceAplicacionActual]);
				}
			}

            modelo.Nombre      = nuevoModelo.Nombre;
	        modelo.Tipo        = nuevoModelo.Tipo;
	        modelo.Descripcion = nuevoModelo.Descripcion;
	        modelo.TurnosDeDuracion = nuevoModelo.TurnosDeDuracion;
        }

        /// <summary>
        /// Lidia con el cambio de <see cref="ControladorEfectoSiendoAplicado.ComportamientoAcumulativo"/> en un <see cref="ControladorEfectoSiendoAplicado"/>
        /// </summary>
        /// <param name="controladorAplicacionEfecto">Aplicacion del efecto cuyo <see cref="EComportamientoAcumulativo"/> cambio</param>
        /// <param name="comportamientoAnterior">Comportamiento acumulativo anterior</param>
        /// <param name="comportamientoActual">Comportamiento acumulativo actual</param>
        [AccesibleEnGuraScratch(nameof(ModificarComportamientoAcumulativoAplicacion))]
        public void ModificarComportamientoAcumulativoAplicacion(
	        ControladorEfectoSiendoAplicado controladorAplicacionEfecto,
	        EComportamientoAcumulativo comportamientoAnterior,
	        EComportamientoAcumulativo comportamientoActual,
	        EModoDeCambioDeComportamientoAcumulativo modoDeCambio)
        {
			//Nos aseguramos de que el ControladorEfecto que creo la aplicacion que nos pasaron sea este controlador
	        if (controladorAplicacionEfecto.controladorEfecto != this)
	        {
                SistemaPrincipal.LoggerGlobal.Log($@"No se puede modificar el {nameof(EComportamientoAcumulativo)} de {nameof(ControladorEfectoSiendoAplicado)}({controladorAplicacionEfecto}) 
														   porque no fue creado a partir de este {nameof(ControladorEfecto)}({this})", ESeveridad.Error);

		        return;
	        }

			//Este booleano indica si el modo de cambio marca que debemos actualizar de alguna manera el resto de las aplicaciones de este efecto
	        bool debeModificarAplicacionesAnteriores =
		        modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivas) ||
		        modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivasConElComportamientoAnterior);

			//Este booleano indica si el modo de cambio marca que debemos quitar de alguna manera el resto de las aplicaciones de ese efecto
	        bool debeEliminarAplicacionesAnteriores =
		        modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.EliminarAplicaciones) ||
		        modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.EliminarAplicacionesConElComportamientoAnterior) ||
		        modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.EliminarAplicacionesConComportamientoDistinto);

			//Hacemos un switch en el comportamiento acumulativo anterior de la aplicacion que estamos modificando
            switch (comportamientoAnterior)
            {
				#region Comportamiento anterior es solapar
				case EComportamientoAcumulativo.Solapar:
					{
						//En este caso el comportamiento anterior es solapar por lo que el nuevo comportamiento claramente no puede ser solapar
						//teniendo esto en cuenta y que los comportamientos que no son solapar solo pueden contar con una aplicacion a la vez
						//asi que si ya existe una aplicacion del efecto con el nuevo comportamiento, solamente deberemos aumentar su contador de
						//acumulaciones y luego eliminar la aplicacion. Si no existe entonces cambiaremos el tipo de la aplicacion y la moveremos
						//de contenedor

						//Varible que guarda el efecto existente con el nuevo comportamiento
						ControladorEfectoSiendoAplicado aplicacionEfectoExistente = null;

						//Si ya existe un efecto con el nuevo comportamiento...
						if (mAplicacionesEfectoNoSolapantes[controladorAplicacionEfecto.objetivo].ContainsKey(comportamientoActual))
						{
							//Asignamos aplicacionExistente a este efecto
							aplicacionEfectoExistente = mAplicacionesEfectoNoSolapantes[controladorAplicacionEfecto.objetivo][comportamientoActual];
						}
						//Si no lo hace...
						else
						{
							//Guardamos la aplicacion en el diccionario de efectos no solapantes
							mAplicacionesEfectoNoSolapantes[controladorAplicacionEfecto.objetivo][comportamientoActual] = controladorAplicacionEfecto;

							//Quitamos la aplicacion de la lista de aplicaciones solapantes
							mAplicacionesEfectoSolapantes[controladorAplicacionEfecto.objetivo].Remove(controladorAplicacionEfecto);

							//Asignamos la aplicacion existente a la aplicacion del efecto
							aplicacionEfectoExistente = controladorAplicacionEfecto;
						}

						//Si debemos modificar las aplicaciones anteriores de este efecto...
						if (debeModificarAplicacionesAnteriores)
						{
							//Obtenemos todas las aplicaciones solapantes de este efecto sobre el objetivo especificado
							var aplicacionesSolapantesExistentes = mAplicacionesEfectoSolapantes[controladorAplicacionEfecto.objetivo];

							//Si el nuevo comportamiento es sumar turnos...
							if (comportamientoActual == EComportamientoAcumulativo.SumarTurnos)
							{
								//Sumamos los turnos a la nueva aplicacion de la forma correspondiente en base al modo de cambio

								if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.SumarTurnosRestantes))
								{
									//Sumamos todos los turnos restantes
									aplicacionEfectoExistente.TurnosRestantes += aplicacionesSolapantesExistentes.Sum(a => a.TurnosRestantes);
								}
								else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.TomarValorMasAltoDeTurnosRestantes))
								{
									//Tomamos la aplicacion con mas turnos restantes
									aplicacionEfectoExistente.TurnosRestantes += aplicacionesSolapantesExistentes.Max(a => a.TurnosRestantes);
								}
								else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.TomarValorMasBajoDeTurnosRestantes))
								{
									//Tomamos la aplicacion con menos turnos restantes
									aplicacionEfectoExistente.TurnosRestantes += aplicacionesSolapantesExistentes.Min(a => a.TurnosRestantes);
								}
								else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.TomarValorPromedioDeTurnosRestantes))
								{
									//Tomamos el promedio de turnos restantes
									aplicacionEfectoExistente.TurnosRestantes += aplicacionesSolapantesExistentes.Average(a => a.TurnosRestantes).ToInt();
								}
							}

							//Si la aplicacion existente es distinta a la aplicacion que esta cambiando el comportamiento acumulativo entonces le sumamos
							//a las acumulaciones de la primera las acumulaciones de la ultima, esto porque si la aplicacion existente es la que esta cambiando
							//de tipo acumulativo entonces el contador de acumulaciones ya esta actualizado
							if (aplicacionEfectoExistente != controladorAplicacionEfecto)
								aplicacionEfectoExistente.ContadorAcumulaciones += controladorAplicacionEfecto.ContadorAcumulaciones;

							//Quitamos las aplicaciones con el comportamiento anterior, en este caso Solapante
							QuitarAplicacionesConComportamiento(controladorAplicacionEfecto.objetivo, comportamientoAnterior.ObtenerFlag(), null);
						}
						//Si no debemos modificarlas
						else
						{
							//Si la aplicacion existente es distinta a la que esta cambiando el comportamiento...
							if (aplicacionEfectoExistente != controladorAplicacionEfecto)
							{
								//Sumamos a las acumulaciones de la aplicacion existente
								AñadirEfecto(controladorAplicacionEfecto, controladorAplicacionEfecto.objetivo, comportamientoActual);
							}
							//Si no lo es...
							else
							{
								//Reseteamos el contador de acumulaciones
								aplicacionEfectoExistente.ContadorAcumulaciones = 1;
							}
						}

						break;
					}
				#endregion

				#region Comportamiento anterior es sumar turnos
				case EComportamientoAcumulativo.SumarTurnos:
					{
						//Si el nuevo comportamiento es solapar...
						if (comportamientoActual == EComportamientoAcumulativo.Solapar)
						{
							//Si no debe repartir los turnos...
							if (!modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.RepartirTurnos))
							{
								goto case EComportamientoAcumulativo.Contar;
							}

							//Variable que almacena el valor de turnos restantes que asignar a cada nueva aplicacion solapante del efecto que se cree
							int turnosQueDarACadaUno = controladorAplicacionEfecto.TurnosDeDuracion;
							
							//Si debe repartir los turnos restantes avariciosamente...
							if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.RepartirTurnosAvariciosamente))
							{
								turnosQueDarACadaUno = 1;
							}
							//Si debe juntar todos los turnos restantes en una aplicacion...
							else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.JuntarTurnosEnUnaAplicacion))
							{
								turnosQueDarACadaUno = controladorAplicacionEfecto.TurnosRestantes;
							}

							//Mientras nos queden turnos restantes suficientes para crear mas aplicaciones dandoles la cantidad de turnos acordada
							while (controladorAplicacionEfecto.TurnosRestantes > turnosQueDarACadaUno)
							{
								//Creamos una nueva aplicacion solapante del efecto y actualizamos sus turnos restantes al valor acordado
								AplicarEfecto(controladorAplicacionEfecto.instigador, controladorAplicacionEfecto.objetivo, EComportamientoAcumulativo.Solapar)
									.TurnosRestantes = turnosQueDarACadaUno;

								//Restamos a los turnos restantes de la aplicacion
								controladorAplicacionEfecto.TurnosRestantes -= turnosQueDarACadaUno;
							}
							
							//Cambiamos la aplicacion del efecto de contenedor
							QuitarEfecto(controladorAplicacionEfecto);
							AñadirEfecto(controladorAplicacionEfecto, controladorAplicacionEfecto.objetivo, comportamientoActual);
						}
						else
						{
							//Lo mismo que arriba solo que ahora lo hacemos al principio
							QuitarEfecto(controladorAplicacionEfecto);
							AñadirEfecto(controladorAplicacionEfecto, controladorAplicacionEfecto.objetivo, comportamientoActual);

							//Obtenemos la aplicacion del efecto con el nuevo comportamiento
							var controladorEfectoComportamientoActual = mAplicacionesEfectoNoSolapantes[controladorAplicacionEfecto.objetivo][comportamientoActual];

							//Si debemos mantener los turnos actuales
							if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.MantenerTurnosAcumulados))
							{
								//Y el controlador del efecto con el comportamiento actual es distinto al del efecto que cambio su comportamiento acumulativo...
								if(controladorEfectoComportamientoActual != controladorAplicacionEfecto)
									controladorEfectoComportamientoActual.TurnosRestantes += controladorAplicacionEfecto.TurnosRestantes;
							}
							//Si no debemos mantenerlos y el controlador del efecto con el comportamiento actual es igual al del efecto que cambio su comportamiento acumulativo...
							else if (controladorEfectoComportamientoActual == controladorAplicacionEfecto)
							{
								//Nos aseguramos de que los turnos restantes queden dentro de los limites del efecto
								controladorEfectoComportamientoActual.TurnosRestantes = Math.Min(controladorAplicacionEfecto.TurnosRestantes, controladorAplicacionEfecto.TurnosDeDuracion);
							}
						}

						break;
					}
				#endregion

				#region Comportamiento anterior es Esperar o Contar
				case EComportamientoAcumulativo.Esperar:
				case EComportamientoAcumulativo.Contar:
					{
						int turnosQueDar = controladorAplicacionEfecto.TurnosRestantes;

						//Si debemos ignorar los turnos restantes y subirlos al maximo...
						if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.IgnorarTurnosRestantesYMaxear))
						{
							turnosQueDar = controladorAplicacionEfecto.TurnosDeDuracion;
						}
						//Si debemos ignorar los turnos restantes y bajarlos al minimo...
						else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.IgnorarTurnosRestantesYDejarAlMinimo))
						{
							turnosQueDar = 1;
						}
						//Si el nuevo comportamiento es sumar turnos y debemos multiplicar los turnos restantes por las acumulaciones...
						else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.MultiplicarAcumulacionesPorTurnosRestantes) &&
								 comportamientoActual == EComportamientoAcumulativo.SumarTurnos)
						{
							turnosQueDar *= controladorAplicacionEfecto.ContadorAcumulaciones;
						}

						//Si el comportamiento actual es solapar y debemos crear una aplicacion por cada acumulacion...
						if (comportamientoActual == EComportamientoAcumulativo.Solapar &&
							modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.CrearUnaAplicacionPorAcumulacion))
						{
							//Empezamos el bucle en indice 1 porque ya tenemos uno de los efectos creados
							for (int i = 1; i < controladorAplicacionEfecto.ContadorAcumulaciones; ++i)
							{
								//Creamos una aplicacion y actualizamos sus turnos restantes
								AplicarEfecto(controladorAplicacionEfecto.instigador, controladorAplicacionEfecto.objetivo, comportamientoActual)
									.TurnosRestantes = turnosQueDar;
							}
						}

						//Actualizamos los turnos restantes de este efecto
						controladorAplicacionEfecto.TurnosRestantes = turnosQueDar;

						//Lo cambiamos de contendor
						QuitarEfecto(controladorAplicacionEfecto);
						AñadirEfecto(controladorAplicacionEfecto, controladorAplicacionEfecto.objetivo, comportamientoActual);

						break;
					}
				default:
					{
						break;
					} 
	            #endregion
			}

			//Si debemos modificar las aplicaciones anteriores
            if (debeModificarAplicacionesAnteriores)
            {
				//Si debemos modificar las aplicaciones con el comportameinto anterior...
	            if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.ModificarAplicacionesActivasConElComportamientoAnterior))
	            {
		            if (comportamientoAnterior != EComportamientoAcumulativo.Solapar)
		            {
			            ModificarComportamientoAplicaciones(
				            controladorAplicacionEfecto.objetivo,
				            comportamientoActual,
				            comportamientoAnterior.ObtenerFlag(),
				            modoDeCambio);
		            }
	            }
				//Si debemos modificar las aplicaciones con comportamiento distinto
	            else
	            {
		            ModificarComportamientoAplicaciones(
			            controladorAplicacionEfecto.objetivo,
			            comportamientoActual,
			            ~comportamientoActual.ObtenerFlag(),
			            modoDeCambio);
				}
            }
			//Si debemos eliminar las aplicaciones anteiores...
			else if (debeEliminarAplicacionesAnteriores)
            {
				//Si debemos eliminar las aplicaciones con el comportamiento anterior...
	            if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.EliminarAplicacionesConElComportamientoAnterior))
	            {
					QuitarAplicacionesConComportamiento(controladorAplicacionEfecto.objetivo, comportamientoAnterior.ObtenerFlag());
				}
				//Si debemos eliminar todas las aplicaciones anteriores
				else if (modoDeCambio.HasFlag(EModoDeCambioDeComportamientoAcumulativo.EliminarAplicaciones))
	            {
		            QuitarAplicacionesConComportamiento(controladorAplicacionEfecto.objetivo, EComportamientoAcumulativoFlags.TODOS, controladorAplicacionEfecto);
				}
				//Si no se cumplio ninguno de los casos anteriores entonces debemos eliminar las aplicaciones con comportamiento distinto al actual...
	            else
	            {
					QuitarAplicacionesConComportamiento(controladorAplicacionEfecto.objetivo, ~comportamientoActual.ObtenerFlag());
	            }
            }
        }

		/// <summary>
		/// Cambia el <see cref="EComportamientoAcumulativo"/> de los <see cref="ControladorEfectoSiendoAplicado"/> cuyos comportamientos acumulativos
		/// actuales caigan dentro de los <paramref name="objetivos"/> utlizando el <paramref name="modoDeCambio"/>
		/// </summary>
		/// <param name="objetivo">Personaje al que se le estan aplicando los efectos que seran modificados</param>
		/// <param name="nuevoComportamiento">Nuevo comportamiento que tendran los efectos tras el cambio</param>
		/// <param name="objetivos">Tipos de comportamiento actual que tienen que tener los efectos para ser objetivo del cambio de comportamiento</param>
		/// <param name="modoDeCambio">Manera en la que se cambiara el comportamiento</param>
        private void ModificarComportamientoAplicaciones(
	        ControladorPersonaje objetivo,
			EComportamientoAcumulativo nuevoComportamiento,
	        EComportamientoAcumulativoFlags objetivos,
	        EModoDeCambioDeComportamientoAcumulativo modoDeCambio)
        {
	        List<ControladorEfectoSiendoAplicado> efectosQueModificar = new List<ControladorEfectoSiendoAplicado>();

	        //Si estamos buscando efectos con comportamiento solapar y hay algunos aplicados en el objetivo los añadimos a la lista de efectos a modificar
			if (objetivos.HasFlag(EComportamientoAcumulativoFlags.Solapar) && mAplicacionesEfectoSolapantes.ContainsKey(objetivo))
		        efectosQueModificar.AddRange(mAplicacionesEfectoSolapantes[objetivo]);

			//Obtenemos todos los efectos cuyo comportamiento no es solapar
			efectosQueModificar.AddRange( mAplicacionesEfectoNoSolapantes[objetivo].Values.ToList().FindAll(e =>
	        {
		        //Tomamos los efectos cuyo comportamiento caiga dentro de los objetivos
				return objetivos.HasFlag(e.ComportamientoAcumulativo.ObtenerFlag());
	        }));

			modoDeCambio |= EModoDeCambioDeComportamientoAcumulativo.AmbasModificaciones;
			modoDeCambio ^= EModoDeCambioDeComportamientoAcumulativo.AmbasModificaciones;

			//Modificamos los efectos listados para modificar
			efectosQueModificar.ForEach(e =>
			{
				e.ModificarComportamientoAcumulativo(
					nuevoComportamiento,
					modoDeCambio);
			});
        }

		/// <summary>
		/// Quita los <see cref="ControladorEfectoSiendoAplicado"/> cuyo <see cref="EComportamientoAcumulativo"/> caiga dentro de <paramref name="objetivos"/>
		/// </summary>
		/// <param name="objetivo">Personaje al que se le quitaran los efectos</param>
		/// <param name="objetivos">Comportamientos acumulativos que deberan tener los objetos para ser quitados</param>
		/// <param name="efectoAExceptuar">Efecto que sera excento de ser quitado</param>
        private void QuitarAplicacionesConComportamiento(
			ControladorPersonaje objetivo,
	        EComportamientoAcumulativoFlags objetivos,
	        ControladorEfectoSiendoAplicado efectoAExceptuar = null)
        {
	        List<ControladorEfectoSiendoAplicado> efectosQueEliminar = new List<ControladorEfectoSiendoAplicado>();

			//Si estamos buscando efectos con comportamiento solapar y hay algunos aplicados en el objetivo los añadimos a la lista de efectos a quitar
			if(objetivos.HasFlag(EComportamientoAcumulativoFlags.Solapar) && mAplicacionesEfectoSolapantes.ContainsKey(objetivo))
				efectosQueEliminar.AddRange(mAplicacionesEfectoSolapantes[objetivo]);

			//Obtenemos todos los efectos cuyo comportamiento no es solapar
			efectosQueEliminar.AddRange(mAplicacionesEfectoNoSolapantes[objetivo].Values.ToList().FindAll(e =>
			{
				//Tomamos los efectos cuyo comportamiento caiga dentro de los objetivos
				return objetivos.HasFlag(e.ComportamientoAcumulativo.ObtenerFlag());
			}));

			//Si hay algun efecto a exceptuar lo quitamos de la lista de eliminacion
			if (efectoAExceptuar != null)
				efectosQueEliminar.Remove(efectoAExceptuar);

			//Finalmente quitamos todos los efectos encontrados
			efectosQueEliminar.ForEach(e =>
			{
				e.QuitarEfecto();
			});
		}

        #endregion
    }
}