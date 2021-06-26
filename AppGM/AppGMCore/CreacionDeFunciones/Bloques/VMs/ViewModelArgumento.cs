using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using Ninject.Infrastructure.Language;

using AppGM.Core.Delegados;

namespace AppGM.Core
{
	//TODO: Deteccion automatica del tipo

	/// <summary>
	/// Representa un argumento para una funcion u operacion
	/// </summary>
	public class ViewModelArgumento: ViewModelBloqueFuncion<BloqueArgumento>, IAutocompletable
	{
		#region Eventos

		/// <summary>
		/// Evento que dispara cuando <see cref="TextoActual"/> es modificado por el programa
		/// </summary>
		public event DVariableCambio<string> OnTextoActualModificado = delegate { };

		public event DVariableCambio<Type> OnTipoArgumentoModificado = delegate { };

		#endregion

		#region Campos & Propiedades


		//-----------------------------CAMPOS--------------------------------

		/// <summary>
		/// <see cref="ViewModelBloqueFuncionBase"/> que contiene este campo
		/// </summary>
		private ViewModelBloqueFuncionBase mBloqueContendor;

		/// <summary>
		/// Variable de la que partimos
		/// </summary>
		private object mBase;
		/// <summary>
		/// Tipo que representa <see cref="mBase"/>
		/// </summary>
		private Type mTipoBase;

		/// <summary>
		/// Tipo del argumento
		/// </summary>
		private Type mTipoArgumento;

		/// <summary>
		/// Lista de acceso de miembros despues de <see cref="mBase"/>
		/// </summary>
		private List<MemberInfo> mMiembrosConsecuentes = new List<MemberInfo>();

		/// <summary>
		/// Contiene los <see cref="ViewModelBloqueArgumentosFuncion"/> que se necesitan para guardar los
		/// datos de llamada a las funciones que necesiten parametros.
		/// La llave de este diccionario es el indice de la funcion en la lista de miembros.
		/// </summary>
		private Dictionary<int, ViewModelBloqueArgumentosFuncion> mFuncionesConParametros = new Dictionary<int, ViewModelBloqueArgumentosFuncion>();

		/// <summary>
		/// Valor anterior del campo de texto
		/// </summary>
		private string mTextoAnterior = "";

		/// <summary>
		/// Indica el numero de secciones actuales
		/// </summary>
		private int mNumeroDeSecciones = 1;

		/// <summary>
		/// Posicion del signo de intercalacion
		/// </summary>
		private int mPosSignoIntercalacion;


		//--------------------------PROPIEDADES------------------------------


		public ViewModelVentanaAutocompletado Autocompletado => mVMCreacionDeFuncion.Autocompletado;

		/// <summary>
		/// Valor del argumento
		/// </summary>
		public object Valor { get; set; }

		/// <summary>
		/// <see cref="bool"/> que indica si debemos detectar el <see cref="TipoArgumento"/> automaticamente
		/// en base a lo que ingrese el usuario.
		/// </summary>
		public bool DeteccionAutomaticaDeTipo { get; set; }
			
		/// <summary>
		/// Valor que el usuario ingreso
		/// </summary>
		public string TextoActual { get; set; } = string.Empty;

		/// <summary>
		/// Texto que se esta mostrando actualmente
		/// </summary>
		public string TextoTextBox { get; set; } = string.Empty;

		/// <summary>
		/// Nombre que se muestra en el campo de texto
		/// </summary>
		public string Nombre { get; set; } = string.Empty;

		/// <summary>
		/// Posicion del signo de intercalacion
		/// </summary>
		public int PosSignoIntercalacion
		{
			get => mPosSignoIntercalacion;
			set
			{
				if (value == mPosSignoIntercalacion)
					return;

				mPosSignoIntercalacion = value;

				DispararPropertyChanged(new PropertyChangedEventArgs(nameof(BloqueArgumentosFuncionActual)));
			}
		}

		/// <summary>
		/// Tipo del argumento
		/// </summary>
		public Type TipoArgumento
		{
			get => mTipoArgumento;
			set
			{
				if (value == mTipoArgumento)
					return;

				Type valorAnterior = mTipoArgumento;

				mTipoArgumento = value;

				ActualizarValidez();

				OnTipoArgumentoModificado(valorAnterior, mTipoArgumento);
			}
		}

		/// <summary>
		/// Obtiene el <see cref="ViewModelBloqueLlamarFuncion"/> que corresponde a la funcion
		/// sobre la que se encuentra el <see cref="PosSignoIntercalacion"/>
		/// </summary>
		public ViewModelBloqueArgumentosFuncion BloqueArgumentosFuncionActual
		{
			get
			{
				int indiceSeccionActual = ObtenerIndiceSeccionActual() - 1;

				if (mFuncionesConParametros.ContainsKey(indiceSeccionActual))
					return mFuncionesConParametros[indiceSeccionActual];

				return null;
			}
		}

		/// <summary>
		/// Tipos base disponibles
		/// </summary>
		public List<Type> TiposDisponibles { get; set; } = new List<Type>
		{
			typeof(int), typeof(double), typeof(EArquetipo)
		};

		/// <summary>
		/// Expresion regular para detectar caracteres que no permitimos
		/// </summary>
		public Regex ExpresionRegularDetectarCaracteresNoPermitidos { get; set; } = new Regex("[^a-zñ0-9.]+", RegexOptions.IgnoreCase);

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_vmCreacionDeFuncion">VM del control de creacion de funcion</param>
		/// <param name="bloqueContenedor"></param>
		/// <param name="_tipoArgumento">Tipo de este argumento</param>
		/// <param name="_nombre">Nombre de este argumento</param>
		public ViewModelArgumento(ViewModelCreacionDeFuncionBase _vmCreacionDeFuncion, ViewModelBloqueFuncionBase _bloqueContenedor, Type _tipoArgumento, string _nombre = "")
			:base(_vmCreacionDeFuncion)
		{
			mVMCreacionDeFuncion = _vmCreacionDeFuncion;
			mBloqueContendor     = _bloqueContenedor;
			TipoArgumento        = _tipoArgumento;
			Nombre               = _nombre;

			IndiceBloque = mBloqueContendor.IndiceBloque;

			mBloqueContendor.OnIndiceBloqueModificado += (anterior, actual) =>
			{
				IndiceBloque = mBloqueContendor.IndiceBloque;
			};
		}

		#endregion

		#region Metodos

		public override List<BloqueVariable> ObtenerVariables() => mBloqueContendor.ObtenerVariables();

		public override BloqueArgumento GenerarBloque_Impl()
		{
			if (!EsValido)
				return null;

			List<SeccionArgumentoBase> seccionesArgumento = new List<SeccionArgumentoBase>();

			//Solo añadimos una seccion si la base es una variable
			if(mBase is BloqueVariable var)
				seccionesArgumento.Add(new SeccionArgumentoVariable(var.nombre));

			for (int i = 0; i < mMiembrosConsecuentes.Count; ++i)
			{
				//Si este miembro es una funcion con parametros...
				if (mMiembrosConsecuentes[i].EsFuncionConParametros())
				{
					//Generamos los argumentos de la funcion
					IEnumerable<BloqueArgumento> argumentosFuncion =
						from param in mFuncionesConParametros[i].ArgumentosFuncion 
						select param.GenerarBloque_Impl();

					seccionesArgumento.Add(new SeccionArgumentoMetodoConParametros((MethodInfo)mMiembrosConsecuentes[i], argumentosFuncion.ToList()));

					//Nos saltamos el resto del bucle
					continue;
				}

				seccionesArgumento.Add(new SeccionArgumentoMiembro(mMiembrosConsecuentes[i]));
			}

			return new BloqueArgumento(seccionesArgumento, TipoArgumento);
		}

		public void ActualizarPosibilidadesAutocompletado(string nuevoTexto, int nuevaPosSignoIntercalacion)
		{
			if(nuevoTexto.Length > 0 && nuevoTexto.Last() == '.')
				EncontrarMiembrosFaltantes();

			TextoActual           = nuevoTexto;
			PosSignoIntercalacion = nuevaPosSignoIntercalacion;

			ActualizarCantidadDeSecciones();

			//Si el usuario borro texto
			if (TextoActual.Length < mTextoAnterior.Length)
			{
				//Obtenemos en indice de la seccion donde se encuentra el cursor
				int indiceSeccionAcutal = ObtenerIndiceSeccionActual();

				//Si la seccion es 0, es decir la de la variable...
				if (indiceSeccionAcutal == 0)
				{
					mBase = null;

					mMiembrosConsecuentes.Clear();
				}
				//Si la seccion actual no es mayor a la cantidad de miembros...
				else if (indiceSeccionAcutal <= mMiembrosConsecuentes.Count)
				{
					//Disminuimos en uno el indice porque este tiene en cuenta la variable base pero la lista
					//mMiembrosConsecuentes no lo hace asi que el 0 de esta lista es el 1 del indiceSeccionActual
					--indiceSeccionAcutal;

					//Quitamos los miembros desde el indice seleccionado en adelante
					for (int i = mMiembrosConsecuentes.Count - 1; i >= indiceSeccionAcutal; --i)
					{
						if (mMiembrosConsecuentes[i].EsFuncionConParametros())
							mFuncionesConParametros.Remove(i);

						mMiembrosConsecuentes.RemoveAt(i);

						DispararPropertyChanged(new PropertyChangedEventArgs(nameof(BloqueArgumentosFuncionActual)));
					}

					//Limpiamos la lista de valores existentes del autocompletado
					Autocompletado.ValoresExistentes.Clear();
				}
			}

			//Si debemos actualizar el autocompletado con variables...
			if (DeberiaActualizarAutocompletadoConVariables())
			{
				//Actualizamos los valores
				Autocompletado.ActualizarValoresExistentes(ObtenerVariablesCompatibles());
			}
			else if (DeberiaActualizarAutocompletadoConMiembros())
			{
				Autocompletado.ActualizarValoresExistentes(ObtenerMiembrosCompatibles());
			}

			//Le pasamos al autocompletado el nuevo texto
			Autocompletado.ActualizarTextoActual(ObtenerSeccionDelTexto());

			mTextoAnterior = TextoActual;
		}

		public void ActualizarPosicionSignoDeIntercalacion(int nuevaPosicion)
		{
			if (nuevaPosicion == PosSignoIntercalacion)
				return;

			PosSignoIntercalacion = nuevaPosicion;
		}

		public void HandlerValorSeleccionado(ViewModelItemAutocompletadoBase valorSeleccionado)
		{
			//Si es un item que contiene un miembro...
			if (valorSeleccionado is ViewModelItemAutocompletadoMiembro itemMiembro)
			{
				mMiembrosConsecuentes.Add(itemMiembro.valorItem);

				//Obtenemos los indices de la seccion actual
				var indices = ObtenerIndices();

				//Si los indices no son -1...
				if(indices.indiceMenor != -1 && indices.indiceMayor != -1)
					//Quitamos la parte del nombre del miembro que ya estaba escrita
					TextoActual = TextoActual.Remove(indices.indiceMenor);

				if (itemMiembro.valorItem.EsFuncionConParametros())
				{
					mFuncionesConParametros.Add(ObtenerIndiceSeccionActual() - 1,
						new ViewModelBloqueArgumentosFuncion(mVMCreacionDeFuncion, (MethodInfo) itemMiembro.valorItem));

					DispararPropertyChanged(new PropertyChangedEventArgs(nameof(BloqueArgumentosFuncionActual)));
				}

				//Anexamos al texto actual el nombre del miembro
				ModificarTextoActual(TextoActual + itemMiembro.RepresentacionTextual);
			}
			//Si contiene una variable...
			else if (valorSeleccionado is ViewModelItemAutocompletadoVariable itemVariable)
			{
				//Establecemos la variable base
				ActualizarBase(itemVariable.bloque);

				//Actualizamos el texto actual
				ModificarTextoActual(itemVariable.bloque.nombre);
			}
			else if (valorSeleccionado is ViewModelItemAutocompletadoTipo itemTipo)
			{
				ActualizarBase(itemTipo.tipo);

				//Actualizamos el texto actual
				ModificarTextoActual(itemTipo.tipo.Name);
			}
		}

		public void FocusObtenido()
		{
			Autocompletado.OnValorSeleccionado += HandlerValorSeleccionado;
		}

		public void FocusPerdido()
		{
			Autocompletado.OnValorSeleccionado -= HandlerValorSeleccionado;

			ActualizarValidez();

			//Colocamos la posicion del signo de intercalacion en 0 para que desaparezca al lista
			//de parametros de la funcion en caso de estar desplegada
			PosSignoIntercalacion = 0;
		}

		/// <summary>
		/// Obtiene todas las <see cref="BloqueVariable"/> que se pueden utilizar para asignar a este <see cref="ViewModelArgumento{TipoArgumento}"/>
		/// </summary>
		/// <returns><see cref="List{T}"/> de todas las variables posibles</returns>
		private List<ViewModelItemAutocompletadoBase> ObtenerVariablesCompatibles()
		{
			List<ViewModelItemAutocompletadoBase> nombresVariables = new List<ViewModelItemAutocompletadoBase>();

			//Obtenemos todas las variables
			var variablesDisponibles = ObtenerVariables();

			if (!DeteccionAutomaticaDeTipo)
			{
				//Quitamos las variables que no se puedan usar para asignar al tipo de este argumento
				variablesDisponibles.RemoveAll(bloque =>
				{
					return !TipoArgumento.IsAssignableFrom(bloque.tipo);
				});
			}

			nombresVariables.AddRange(
				from bloque in variablesDisponibles
				select new ViewModelItemAutocompletadoVariable(bloque.nombre, bloque));

			nombresVariables.AddRange(
				from tipo in TiposDisponibles
				select new ViewModelItemAutocompletadoTipo(tipo));
			
			return nombresVariables.ToList();
		}

		/// <summary>
		/// Obtiene los <see cref="ViewModelItemAutocompletadoMiembro"/> que representan a los
		/// <see cref="MemberInfo"/> que se pueden utilizar para asignar a <see cref="TipoArgumento"/>
		/// </summary>
		/// <returns><see cref="List{T}"/> de <see cref="ViewModelItemAutocompletadoMiembro"/> compatibles</returns>
		private List<ViewModelItemAutocompletadoBase> ObtenerMiembrosCompatibles()
		{
			//Nos aseguramos que la variable base exista
			if (!ConfirmarExistenciaBase())
				return null;

			if (mBase is Type t)
			{
				return (from miembro in t.GetMembers(BindingFlags.Static | BindingFlags.Public)
					where DeteccionAutomaticaDeTipo || TipoArgumento.IsAssignableFrom(t)
					select new ViewModelItemAutocompletadoMiembro(miembro)).Cast<ViewModelItemAutocompletadoBase>().ToList();
			}

			IEnumerable<ViewModelItemAutocompletadoBase> miembrosDisponibles;

			//Tipo del valor que devuelve el miembro
			Type tipo = null;

			//Si la lista de miembros es null o esta vacia asignamos 'tipo' al tipo de la variable base
			if (mMiembrosConsecuentes.Count == 0)
				tipo = mTipoBase;
			//Si no...
			else if (mMiembrosConsecuentes.Count != 0)
			{
				//Obtenemos el miembro de la seccion anterior a la que actualmente se esta escribiendo
				tipo = mMiembrosConsecuentes[ObtenerIndiceSeccionActual() - 2].ObtenerTipoRetorno();
			}
			
			//De la lista de miembros encontrados solamente seleccionamos los que tengan
			//el atributo 'AccesibleEnGuraScratch' y que puedan ser utilizados para 
			//asignar a 'TipoArgumento'
			miembrosDisponibles = from miembro in tipo.GetMembers()
				where miembro.EsAccesibleEnGuraScratch()
				select new ViewModelItemAutocompletadoMiembro(miembro);

			//Devolvemos los miembros encontrados
			return miembrosDisponibles.ToList();
		}

		/// <summary>
		/// Obtiene el par de indices que delimitan la seccion en la que actualmente
		/// se encuentra el <see paramref="posicionDelTexto"/>
		/// </summary>
		/// <returns>Par de indices, en caso de que la seccion consista solo de un punto
		/// o el <see cref="TextoActual"/> este vacio los indices devueltos seran -1</returns>
		private (int indiceMenor, int indiceMayor) ObtenerIndices(int posicionDelTexto = -1)
		{
			if (posicionDelTexto == -1)
				posicionDelTexto = PosSignoIntercalacion;

			//Si el texto actual esta vacio devolvemos -1 para ambos indices
			if (TextoActual.Length == 0)
				return (-1, -1);

			//Si solo hay una seccion...
			if (mNumeroDeSecciones == 1)
				//Devolvemos indices que abarcan toda la cadena
				return (0, TextoActual.Length - 1);

			//Obtenemos el indice del primer punto
			int indicePrimerPunto = TextoActual.IndexOf('.');

			//Si no es -1 pero el signo de intercalacion esta antes del primer punto...
			if (posicionDelTexto < indicePrimerPunto)
				//Devolvemos indices a la primera seccion
				return (0, indicePrimerPunto - 1);

			//Si el ultimo caracter es un punto...
			if (TextoActual.Last() == '.')
				//Devolvemos -1 para ambos indices
				return (-1, -1);

			//Si tras el primer punto no hay mas signos de puntuacion...
			if (mNumeroDeSecciones == 2)
				//Devolvemos un indice que empieza desde despues del primer punto hasta el final de la cadena
				return (indicePrimerPunto + 1, TextoActual.Length - 1);

			//Igualamos primer y ultimo indice a la posicion del signo de intercalacion
			int primerIndice = posicionDelTexto - 1;
			int ultimoIndice = posicionDelTexto;

			//Si el primer indice es igual a la longitud del texto...
			if (ultimoIndice == TextoActual.Length)
			{
				--ultimoIndice;
			}

			//Disminuimos el primer indice hasta que se encuentre en la posicion de un punto
			while (TextoActual[primerIndice] != '.')
				--primerIndice;

			//Aumentamos el ultimo indice hasta que llegue al final de la cadena o llegue a un punto
			while (ultimoIndice < TextoActual.Length - 1 && TextoActual[ultimoIndice] != '.')
				++ultimoIndice;

			//Devolvemos los indices abarcando la seccion sin contar los puntos
			return (primerIndice + 1, ultimoIndice - (TextoActual[ultimoIndice] == '.' ? 1 : 0));
		}

		/// <summary>
		/// Obtiene la seccion actual del texto que esta modificando el usuario
		/// </summary>
		/// <returns><see cref="string"/> que contiene la cadena</returns>
		private string ObtenerSeccionDelTexto(int posicionDelTexto = -1)
		{
			//Obtenemos los indices actuales
			var indices = ObtenerIndices(posicionDelTexto);

			//Si alguno de los dos indices es -1 entonces simplemente devolvemos una cadena vacia
			if (indices.indiceMenor == -1 || indices.indiceMayor == -1)
				return string.Empty;

			//Devolvemos la sub-cadena
			return TextoActual.Substring(indices.indiceMenor, indices.indiceMayor - indices.indiceMenor + 1);
		}

		/// <summary>
		/// Obtiene el indice de la seccion que el usuario actualmente esta modificando
		/// </summary>
		/// <returns><see cref="int"/> que es el indice de la seccion actual</returns>
		private int ObtenerIndiceSeccionActual()
		{
			//Obtenemos el primer indice del caracter '.'
			int indicePuntoActual = TextoActual.IndexOf('.');

			//Si no hay ningun punto o la posicion de intercalacion es menor al indice de este punto...
			if (indicePuntoActual == -1 || PosSignoIntercalacion < indicePuntoActual)
				//Devolvemos 0
				return 0;

			//IndiceZ de la seccion que actualmente estamos evaluando
			int indiceActual = 0;

			//Mientras el signo de intercalacion sea menor al indice del punto actual y el punto
			//actual tenga un indice valido, es decir que exista
			while (PosSignoIntercalacion > indicePuntoActual && indicePuntoActual != -1)
			{
				//Aumentamos el indice actual
				++indiceActual;

				//Actualizamos el indice del punto actual
				indicePuntoActual = TextoActual.IndexOf('.', indicePuntoActual + 1);
			}

			return indiceActual;
		}

		/// <summary>
		/// Obtiene el texto de una determinada seccion
		/// </summary>
		/// <param name="indiceSeccion"></param>
		/// <returns></returns>
		private string ObtenerTextoDeSeccion(int indiceSeccion)
		{
			if (indiceSeccion >= mNumeroDeSecciones)
				return string.Empty;

			//Indice del punto actual
			int indiceActual = 0;

			//Iteramos mientras que el indice de seccion no sea cero
			while (indiceSeccion != 0)
			{
				--indiceSeccion;

				//Si la posicion siguiente al indice actual cae fuera de limites...
				if (indiceActual + 1 >= TextoActual.Length)
					return string.Empty;

				//Obtenemos el indice del proximo punto
				indiceActual = TextoActual.IndexOf('.', indiceActual + 1);
			}

			//Si el indice actual es valido...
			if (indiceActual != -1)
				return ObtenerSeccionDelTexto(indiceActual + 1);
			//Si no...
			else
				return string.Empty;
		}

		/// <summary>
		/// Booleano indicando si debemos actualizar el <see cref="Autocompletado"/> con una lista de
		/// las <see cref="ViewModelItemAutocompletadoVariable"/> disponibles
		/// </summary>
		/// <returns><see cref="bool"/> indicando si debemos actualizar el <see cref="Autocompletado"/></returns>
		private bool DeberiaActualizarAutocompletadoConVariables()
		{
			//Si ya hay valores en al lista y son de tipo ViewModelAutocompletadoString
			//entonces no hace falta actualizar
			if (Autocompletado.ValoresExistentes.Count != 0 &&
			    Autocompletado.ValoresExistentes[0] is ViewModelItemAutocompletadoString)
				return false;

			//Si estamos en la primera seccion entonces actualizamos
			return !TextoActual.Contains('.') || PosSignoIntercalacion < TextoActual.IndexOf('.');
		}

		/// <summary>
		/// Booleano indicando si debemos actualizar el <see cref="Autocompletado"/> con una lista de
		/// los <see cref="ViewModelItemAutocompletadoMiembro"/> disponibles
		/// </summary>
		/// <returns><see cref="bool"/> indicando si debemos actualizar el <see cref="Autocompletado"/></returns>
		private bool DeberiaActualizarAutocompletadoConMiembros()
		{
			if (mNumeroDeSecciones == 1)
				return false;

			if (Autocompletado.ValoresExistentes.Count != 0
			    && Autocompletado.ValoresExistentes[0] is ViewModelItemAutocompletadoMiembro)
				return false;

			int indicePrimerPunto = TextoActual.IndexOf('.');

			return indicePrimerPunto < PosSignoIntercalacion;
		}

		/// <summary>
		/// Si no se selecciona una variable directamente desde el <see cref="Autocompletado"/>
		/// esta funcion intenta seleccionar la variable apropiada en base al nombre
		/// ingresado y guardarla en <see cref="mBase"/>
		/// </summary>
		private bool ConfirmarExistenciaBase()
		{
			if (mBase != null)
				return true;

			//Guardamos el texto actual en una cadena tempora
			string nombreVariable = TextoActual;

			//Si tiene al menos dos secciones...
			if (mNumeroDeSecciones > 1)
			{
				//Obtenemos el indice del primer punto
				int indicePunto = nombreVariable.IndexOf('.');

				//Obtenemos toda la cadena antes del primer punto
				nombreVariable = nombreVariable.Substring(0, indicePunto);
			}

			//Obtenemos las variables compatibles para este parametro
			var variablesDisponibles = ObtenerVariablesCompatibles();

			//Por cada variable...
			foreach (var variable in variablesDisponibles)
			{
				//Confirmamos que su vm sea del tipo correcto y luego que el nombre sea exactamente igual al de la variable
				if (variable is ViewModelItemAutocompletadoVariable vm
				    && variable.Comparar(nombreVariable, true))
				{
					ActualizarBase(vm.bloque);

					return true;
				}
			}

			//Si no era una variable entonces nos fijamos si es un tipo...
			foreach (var tipo in TiposDisponibles)
			{
				//Si el nombre es igual al del tipo...
				if (tipo.Name == nombreVariable)
				{
					ActualizarBase(tipo);

					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Intenta encontrar los <see cref="MemberInfo"/> a los que corresponden los nombres
		/// ingresados por el usuario y los añade a <see cref="mMiembrosConsecuentes"/>
		/// </summary>
		private void EncontrarMiembrosFaltantes()
		{
			//Si no falta ningun miembro entonces simplemente retornamos
			if (mMiembrosConsecuentes.Count == mNumeroDeSecciones - 1)
				return;

			//Ultimo tipo valido en el argumento
			Type ultimoTipoValido = mMiembrosConsecuentes.Count > 0 
				? mMiembrosConsecuentes.Last().ObtenerTipoRetorno() 
				: mTipoBase;

			//Condicion del bucle de miembros. Se encarga de terminar si un miembro es adecuado
			Func<MemberInfo, bool> condicionBucle;

			//Iteramos una vez por cada miembro faltante
			for (int i = mMiembrosConsecuentes.Count + 1; i < mNumeroDeSecciones; i++)
			{
				//Obtenemos el texto correspondiente a ese miembro
				string seccionDeTextoConsecuente = ObtenerTextoDeSeccion(i);

				//Si el tipo actual es un enum...
				if (typeof(Enum).IsAssignableFrom(ultimoTipoValido))
				{
					condicionBucle = miembro =>
						miembro.ObtenerTipoRetorno().EsAccesibleEnGuraScratch() &&
						miembro.Name.Equals(seccionDeTextoConsecuente) &&
						(DeteccionAutomaticaDeTipo || TipoArgumento.IsAssignableFrom(miembro.ObtenerTipoRetorno()));
				}
				//Si es cualquier otra cosa...
				else
				{
					condicionBucle = miembro =>
						miembro.EsAccesibleEnGuraScratch() &&
						miembro.Name.Equals(seccionDeTextoConsecuente) &&
						(DeteccionAutomaticaDeTipo || TipoArgumento.IsAssignableFrom(miembro.ObtenerTipoRetorno()));
				}

				foreach (var miembro in ultimoTipoValido.GetMembers())
				{
					//Si este miembro cumple con las condiciones...
					if (condicionBucle(miembro))
					{
						//Lo añadimos a la lista
						mMiembrosConsecuentes.Add(miembro);

						//Actualizamos el ultimo tipo para las iteraciones consecuentes
						ultimoTipoValido = miembro.ObtenerTipoRetorno();

						break;
					}
				}

			}
		}

		/// <summary>
		/// Indica si el valor ingresado por el usuario es valido
		/// </summary>
		/// <returns><see cref="bool"/> indicando si el valor es valido</returns>
		public bool VerificarValidez()
		{
			//Si el texto esta vacio entonces la variable no se inicializa a ningun valor
			if (TextoActual.Length == 0)
				return true;

			//Si la variable base no existe
			if (!ConfirmarExistenciaBase())
				//Revisamos que se pueda convertir del string ingresado al tipo del argumento
				return TypeDescriptor.GetConverter(TipoArgumento).IsValid(TextoActual);

			EncontrarMiembrosFaltantes();

			//Si hay mas secciones de las que deberia...
			while (mNumeroDeSecciones - 1 > mMiembrosConsecuentes.Count)
			{
				--mNumeroDeSecciones;

				TextoActual = TextoActual.Remove(TextoActual.LastIndexOf('.'));
			}

			//Cambiamos el texto anterior aqui para evitar que tome como que el usuario borro caracteres
			mTextoAnterior = TextoActual;
			ModificarTextoActual(TextoActual, true);

			Type tipoDeRetorno = mNumeroDeSecciones == 1 ? mTipoBase : mMiembrosConsecuentes.Last().ObtenerTipoRetorno();

			if (DeteccionAutomaticaDeTipo && tipoDeRetorno != TipoArgumento)
				TipoArgumento = tipoDeRetorno;
			else if (!TipoArgumento.IsAssignableFrom(tipoDeRetorno))
				return false;

			//Revisamos los miembros consecuentes
			for (int i = 0; i < mMiembrosConsecuentes.Count; ++i)
			{
				Type tipoRetorno = mMiembrosConsecuentes[i].ObtenerTipoRetorno();

				//Nos aseguramos de que sean accesibles
				if (!tipoRetorno.EsAccesibleEnGuraScratch())
					return false;

				//Si el miembro es un metodo y tiene una lista de parametros...
				if (mMiembrosConsecuentes[i] is MethodInfo && mFuncionesConParametros.ContainsKey(i))
				{
					//Revisamos que los argumentos sean validos
					if (!mFuncionesConParametros[i].VerificarValidez())
						return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Actualiza el valor de la variable <see cref="EsValido"/>.
		/// </summary>
		//Esto es una funcion para no tener que reescribir VerificarValidez de manera que sea un void 
		public void ActualizarValidez() => EsValido = VerificarValidez();

		/// <summary>
		/// Actualiza <see cref="mBase"/> y <see cref="mTipoBase"/>
		/// </summary>
		/// <param name="nuevoValor">Valor que se le asignara a <see cref="mBase"/></param>
		private void ActualizarBase(object nuevoValor)
		{
			//Si es una variable...
			if (nuevoValor is BloqueVariable var)
			{
				mBase     = var;
				mTipoBase = var.tipo;
			}
			//Si no es una variable entonces tiene que ser un tipo
			else if (nuevoValor is Type t)
			{
				mBase     = nuevoValor;
				mTipoBase = t;
			}
		}

		/// <summary>
		/// Modifica el valor de <see cref="TextoActual"/> y dispara <see cref="OnTextoActualModificado"/>
		/// </summary>
		/// <param name="nuevoTexto">texto que se asignara a <see cref="TextoActual"/></param>
		/// <param name="forzar"><see cref="bool"/> que indica si realizar el cambio aun si
		/// <paramref name="nuevoTexto"/> y <see cref="TextoActual"/> son iguales</param>
		private void ModificarTextoActual(string nuevoTexto, bool forzar = false)
		{
			if (nuevoTexto.Equals(TextoActual) && !forzar)
				return;

			TextoActual  = nuevoTexto;
			TextoTextBox = nuevoTexto;

			ActualizarCantidadDeSecciones();

			OnTextoActualModificado(mTextoAnterior, TextoActual);

			mTextoAnterior = TextoActual;
		}

		/// <summary>
		/// Actualiza el <see cref="mNumeroDeSecciones"/> para que refleje correctamente el texto actual
		/// </summary>
		private void ActualizarCantidadDeSecciones()
		{
			mNumeroDeSecciones = TextoActual.Count(c => c == '.') + 1;
		}

		#endregion
	}
}