using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Input;
using CoolLogs;

namespace AppGM.Core
{
	/// <summary>
	/// Representa un metodo que lidia con eventos de modificacion de la lista de bloques contenidos
	/// </summary>
	/// <param name="bloque"><see cref="ViewModelBloqueFuncionBase"/> que fue modificado</param>
	/// <param name="padre"><see cref="IContenedorDeBloques"/> del <paramref name="bloque"/></param>
	public delegate void BloquesContenidosModificados(ViewModelBloqueFuncionBase bloque, IContenedorDeBloques padre);

	/// <summary>
	/// <see cref="ViewModel"/> que representa un control para la creacion de una funcion
	/// </summary>
	public abstract class ViewModelCreacionDeFuncionBase: ViewModelCreacionEdicionDeModelo<ModeloFuncion, ControladorFuncionBase, ViewModelCreacionDeFuncionBase>, IReceptorDeDragUnico, IContenedorDeBloques
	{
		/// <summary>
		/// Evento que se dispara cuando un bloque es removido
		/// </summary>
		public event BloquesContenidosModificados OnBloqueRemovido = delegate {};

		/// <summary>
		/// Evento que se dispara cuando un bloque es añadido
		/// </summary>
		public event BloquesContenidosModificados OnBloqueAñadido = delegate {};

		#region Propiedades & Campos


		//----------------------------------CAMPOS-----------------------------------


		/// <summary>
		/// Indica el proximo ID a otorgar
		/// </summary>
		private int mIDActual = 0;

		/// <summary>
		/// Indica si se deben mostrar las felicitaciones
		/// </summary>
		private bool mMostrarContenedorFelicitaciones = false;

		/// <summary>
		/// Repertorio de frases de felicitacion
		/// </summary>
		private List<string> mFrasesDeFelicitacion = new List<string>
		{
			"10/10", 
			"Que capo", 
			"Una maravilla",
			"Segui asi",
			"UwU"
		};

		/// <summary>
		/// Repertorio de imagenes de felicitacion
		/// </summary>
		private List<string> mPathsImagenesFelicitacion = new List<string> 
		{ 
			$"{Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, $"GuraScratch{Path.DirectorySeparatorChar}GuraScratchLogo-v1.png")}",
			$"{Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, $"GuraScratch{Path.DirectorySeparatorChar}GuraScratchLogo-v4.png")}",
			$"{Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, $"GuraScratch{Path.DirectorySeparatorChar}GuraScratchLogo-v5.png")}",
			$"{Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, $"GuraScratch{Path.DirectorySeparatorChar}GuraScratchLogo-v7.png")}",
			$"{Path.Combine(SistemaPrincipal.ControladorDeArchivos.DirectorioImagenes, $"GuraScratch{Path.DirectorySeparatorChar}GuraScratchLogo-v8.png")}"
		};

		/// <summary>
		/// Almacena los valores de <see cref="MostrarConfiguracion"/> y <see cref="MostrarLogs"/>
		/// </summary>
		private BitVector32 mMenuActivo = new BitVector32();


		//-------------------------------------------------PROPIEDADES-------------------------------------------------


		/// <summary>
		/// Indica el proposito de esta funcion. Es decir si es para una habilidad, un predicado, etc.
		/// </summary>
		public EPropositoFuncion PropositoFuncion { get; set; }

		/// <summary>
		/// El padre de una instancia de este <see cref="ViewModel"/> siempre es null
		/// </summary>
		//Es null porque este vm no puede tener un padre, es El Padre
		public IContenedorDeBloques Padre => null;

		/// <summary>
		/// Bloques disponibles para el uso del usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueMuestra> BloquesDisponibles { get; set; }

		/// <summary>
		/// Bloques colocados por el usuario
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> Bloques { get; set; } = new ViewModelListaDeElementos<ViewModelBloqueFuncionBase>();

		/// <summary>
		/// Variables base, es decir variables por defecto
		/// </summary>
		public List<BloqueVariable> VariablesBase { get; set; }

		/// <summary>
		/// Variables creadas por el usuario
		/// </summary>
		public List<ViewModelBloqueDeclaracionVariable> VariablesCreadas { get; set; } = new List<ViewModelBloqueDeclaracionVariable>();

		/// <summary>
		/// Ventana de autocompletado
		/// </summary>
		public ViewModelVentanaAutocompletado Autocompletado { get; set; } = new ViewModelVentanaAutocompletado();

		/// <summary>
		/// Los que apareceran en la solapa de Logs
		/// </summary>
		public ViewModelListaDeElementos<ViewModelLog> Logs { get; set; } = new ViewModelListaDeElementos<ViewModelLog>();

		/// <summary>
		/// <see cref="Grosor"/> de los bordes del grid que contiene los <see cref="BloquesColocados"/>
		/// </summary>
		public Grosor GrosorBordesGridBloquesColocados { get; set; }

		/// <summary>
		/// Nombre de la funcion
		/// </summary>
		public string NombreFuncion { get; set; }

		/// <summary>
		/// Indica si se puede iniciar una compilacion
		/// </summary>
		public bool PuedeCompilar { get; protected set; } = true;

		/// <summary>
		/// Indica si se puede guardar la funcion actual
		/// </summary>
		public bool PuedeGuardar { get; protected set; } = true;

		/// <summary>
		/// Indica si debe cargar los bloques disponibles en el controlador de funcion pasado
		/// </summary>
		public bool DebeCargarBloquesDesdeControlador { get; protected set; }

		/// <summary>
		/// Obtiene una frase aleatoria de <see cref="mFrasesDeFelicitacion"/>
		/// </summary>
		public string FraseFelicitacion => mFrasesDeFelicitacion[RandomNumberGenerator.GetInt32(mFrasesDeFelicitacion.Count)];

		/// <summary>
		/// Obtiene un path aleatorio de <see cref="mPathsImagenesFelicitacion"/>
		/// </summary>
		public string PathImagenFelicitacion => mPathsImagenesFelicitacion[RandomNumberGenerator.GetInt32(mPathsImagenesFelicitacion.Count)];

		/// <summary>
		/// Indica si mostrar la imagen y frase de exito
		/// </summary>
		public bool MostrarContenedorFelicitaciones
		{
			get => mMostrarContenedorFelicitaciones;
			set
			{
				if (value == mMostrarContenedorFelicitaciones)
					return;

				mMostrarContenedorFelicitaciones = value;

				DispararPropertyChanged(nameof(FraseFelicitacion));
				DispararPropertyChanged(nameof(PathImagenFelicitacion));
			}
		}

		/// <summary>
		/// Indica si se debe mostrar el menu izquierdo
		/// </summary>
		public bool MostrarMenuIzquierdo
		{
			get => MostrarConfiguracion || MostrarLogs;
			set
			{
				//Si el valor es true entonces no hacemos nada porque no sabemos que menu mostrar
				if (value)
					return;

				MostrarConfiguracion = false;
				MostrarLogs = false;
			}
		}

		/// <summary>
		/// Indica si se debe mostrar el menu de configuracion
		/// </summary>
		public bool MostrarConfiguracion
		{
			get => mMenuActivo[1];
			set
			{
				if (value == mMenuActivo[1])
					return;

				//Si este menu ahora es visible, ocultamos el de logs
				if (value)
					MostrarLogs = false;

				mMenuActivo[1] = value;

				//Le avisamos al menu izquierdo que el esta actual de visibilidad cambio
				DispararPropertyChanged(nameof(MostrarMenuIzquierdo));
			}
		}

		/// <summary>
		/// Indica si se debe mostrar el menu de logs
		/// </summary>
		public bool MostrarLogs
		{
			get => mMenuActivo[2];
			set
			{
				if (value == mMenuActivo[2])
					return;

				//Si este menu ahora es visible, ocultamos el de configuracion
				if (value)
					MostrarConfiguracion = false;

				mMenuActivo[2] = value;

				DispararPropertyChanged(nameof(MostrarMenuIzquierdo));
			}
		}

		public int IndiceZ { get; set; }

		public ICommand ComandoCerrarMenuIzquierdo { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor base
		/// </summary>
		public ViewModelCreacionDeFuncionBase(Action<ViewModelCreacionDeFuncionBase> _accionSalir)
			:base(_accionSalir)
		{
			//Añadimos este vm al sistema principal para que se pueda acceder a el globalmente
			SistemaPrincipal.Atar<ViewModelCreacionDeFuncionBase>(this);

			//Obtenemos los bloques disponibles y las variables por defecto
			AsignarListaDeBloques();
			AsignarVariablesBase();

			GrosorBordesGridBloquesColocados = new Grosor(0, 1);

			ComandoCerrarMenuIzquierdo = new Comando(() =>
			{
				MostrarMenuIzquierdo = false;
			});
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Obtiene el ID para asignar a un bloque.
		/// </summary>
		/// <returns>ID</returns>
		public int ObtenerID()
		{
			//Devolvemos el valor actual de la variable y luego incrementamos su valor
			return mIDActual++;
		}

		/// <summary>
		/// Dispara <see cref="OnBloqueAñadido"/>
		/// </summary>
		/// <param name="bloqueAñadido"><see cref="ViewModelBloqueFuncionBase"/> que fue añadido</param>
		/// <param name="padre"><see cref="IContenedorDeBloques"/> del <paramref name="bloqueAñadido"/></param>
		public void DispararBloqueAñadido(ViewModelBloqueFuncionBase bloqueAñadido, IContenedorDeBloques padre) => OnBloqueAñadido(bloqueAñadido, padre);

		/// <summary>
		/// Dispara <see cref="OnBloqueRemovido"/>
		/// </summary>
		/// <param name="bloqueRemovido"><see cref="ViewModelBloqueFuncionBase"/> que fue removido</param>
		/// <param name="padre"><see cref="IContenedorDeBloques"/> del <paramref name="bloqueRemovido"/></param>
		public void DispararBloqueRemovido(ViewModelBloqueFuncionBase bloqueRemovido, IContenedorDeBloques padre) => OnBloqueRemovido(bloqueRemovido, padre);

		/// <summary>
		/// Metodo que cuando sobreescrito en una clase derivada inicializa <see cref="BloquesDisponibles"/> con los bloques
		/// disponibles para un tipo de funcion especifico
		/// </summary>
		protected abstract void AsignarListaDeBloques();

		/// <summary>
		/// Metodo que cuando se sobreescrito en una clase derivada inicializa <see cref="VariablesBase"/> con las variables
		/// disponibles por defecto para un tipo de funcion especifico
		/// </summary>
		protected abstract void AsignarVariablesBase();

		/// <summary>
		/// Cargas los bloques del <see cref="ControladorFuncion{TipoFuncion}"/> y los carga a <see cref="Bloques"/>
		/// </summary>
		public virtual async void CargarBloquesFuncion(){}

		#region Implementacion de IContenedorDeBloques

		public List<BloqueVariable> ObtenerVariables(ViewModelBloqueFuncionBase bloqueQueIntentaObtenerLasVariables)
		{
			var variables = VariablesBase;

			var variablesCreadasValidas = ObtenerVariablesCreadas(bloqueQueIntentaObtenerLasVariables);

			variables = variables.Concat(variablesCreadasValidas.Select(elemento => elemento.GenerarBloque_Impl())).ToList();

			return variables;
		}

		public List<ViewModelBloqueDeclaracionVariable> ObtenerVariablesCreadas(ViewModelBloqueFuncionBase bloqueQueIntentaObtenerLasVariables)
		{
			return VariablesCreadas.FindAll(
				var => var.EsValido && bloqueQueIntentaObtenerLasVariables.IndiceBloque > var.IndiceBloque);
		}

		public void AñadirBloque(ViewModelBloqueFuncionBase bloque, int indice)
		{
			if (SistemaPrincipal.Drag.HayUnDragActivo)
				GrosorBordesGridBloquesColocados = new Grosor(0, 1);

			if (bloque is ViewModelBloqueDeclaracionVariable var)
				VariablesCreadas.Add(var);

			if (indice != -1)
			{
				Base<IContenedorDeBloques>()?.InsertarBloque(bloque, indice);

				DispararBloqueAñadido(bloque, this);

				return;
			}

			bloque.IndiceBloque = Bloques.Count;
			bloque.IndiceZ = 1;

			Bloques.Add(bloque);

			bloque.Inicializar();

			DispararBloqueAñadido(bloque, this);
		}

		public void QuitarBloque(ViewModelBloqueFuncionBase bloque)
		{
			Bloques.Remove(bloque);

			if (bloque is ViewModelBloqueDeclaracionVariable var)
				VariablesCreadas.Remove(var);

			DispararBloqueRemovido(bloque, this);

			Base<IContenedorDeBloques>().ActualizarIndicesBloques(bloque.IndiceBloque);
		}

		#endregion

		#region Implementacion de IReceptorDeDrag

		public void OnDragEntro_Impl(ArgumentosDragAndDropUnico args)
		{
			if (args.contenido is ViewModelBloqueFuncionBase)
				GrosorBordesGridBloquesColocados = new Grosor(5);
		}

		public void OnDragSalio_Impl(ArgumentosDragAndDropUnico args)
		{
			if (args.contenido is ViewModelBloqueFuncionBase)
				GrosorBordesGridBloquesColocados = new Grosor(0, 1);
		}

		public bool OnDrop_Impl(ArgumentosDragAndDropUnico args)
		{
			if (args.contenido is ViewModelBloqueFuncionBase bloque)
			{
				AñadirBloque(bloque.Copiar(Padre), -1);

				return true;
			}

			SistemaPrincipal.LoggerGlobal.Log($"Se intento dropear un {args.contenido.GetType()} pero no esta soportado", ESeveridad.Advertencia);

			return false;
		}

		#endregion

		#endregion
	}
}