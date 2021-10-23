using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace AppGM.Core
{
	/// <summary>
	/// Controlador que contiene una funcion de tipo conocido
	/// </summary>
	/// <typeparam name="TFuncion">Tipo de la funcion contenida</typeparam>
	public abstract class ControladorFuncion<TFuncion> : ControladorFuncionBase
	{
		/// <summary>
		/// <para>
		///		Representa una funcion cuya existencia es conocida y como minimo se han cargado sus bloques
		/// </para>
		/// <para>
		///		Esta clase existe porque no se pueden modificar los valores de una tupla desde un diccionario
		/// </para>
		/// </summary>
		/// <typeparam name="TFuncion">Tipo de la funcion</typeparam>
		internal class FuncionCargada<TFuncion>
		{
			/// <summary>
			/// Lambda compilada
			/// </summary>
			public TFuncion funcion;

			/// <summary>
			/// Bloques que conforman la funcion
			/// </summary>
			public List<BloqueBase> bloques;
		}

		#region Propiedades

		/// <summary>
		/// Funcion compilada y lista para utilizar
		/// </summary>
		[MaybeNull]
		public TFuncion Funcion
		{
			get
			{
				if (mFuncionesConocidas.ContainsKey(NombreArchivoFuncion))
					return mFuncionesConocidas[NombreArchivoFuncion].funcion;

				return default;
			}
		}

		/// <summary>
		/// Ultimo resultado de intentar compilar la funcion
		/// </summary>
		[MaybeNull]
		public ResultadoCompilacion<TFuncion> ResultadoCompilacion { get; private set; }

		/// <summary>
		/// Obtiene o establece los bloques de la funcion
		/// </summary>
		[MaybeNull]
		public override List<BloqueBase> Bloques
		{
			get
			{
				if (mFuncionesConocidas.ContainsKey(NombreArchivoFuncion))
					return mFuncionesConocidas[NombreArchivoFuncion].bloques;

				return null;
			}

			protected set => mFuncionesConocidas[NombreArchivoFuncion].bloques = value;
		}

		/// <summary>
		/// <para>
		///		Relacion un path a un archivo xml que representa una funcion con la lambda compilada y lista de bloques que la representa.
		/// </para>
		///
		/// <para>
		///		El principal proposito de esta variable es para que si se crean varios controladores de una misma funcion porque esta tiene variables
		///		estaticas y por lo tanto puede tener varias instancias, no necesitemos cargar el xml y compilar los bloques para cada una.
		/// </para>
		/// </summary>
		internal static Dictionary<string, FuncionCargada<TFuncion>> mFuncionesConocidas = new Dictionary<string, FuncionCargada<TFuncion>>();

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_modelo"></param>
		public ControladorFuncion(ModeloFuncion _modelo)
			: base(_modelo) { }

		#endregion

		#region Metodos

		public override async Task CompilarAsync()
		{
			if (mFuncionesConocidas.ContainsKey(NombreArchivoFuncion))
				return;

			var compilador = new Compilador(Bloques);

			ResultadoCompilacion = await Task.Run(() => compilador.Compilar<TFuncion>());

			mFuncionesConocidas[NombreArchivoFuncion].funcion = ResultadoCompilacion.Funcion;
		}

		#endregion
	}
}