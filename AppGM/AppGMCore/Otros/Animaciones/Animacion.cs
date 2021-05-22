using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace AppGM.Core
{
	/// <summary>
    /// Clase que representa una animacion
    /// </summary>
    public class Animacion
    {
		#region Campos, Propiedades & Eventos


		//-----------------------CAMPOS---------------------------


		//Intervalo entre cada fotograma
		private float mIntervaloEntreFotogramas;

		//Indice del fotograma actual
		private int mFotogramaActual = 0;

		//Cantidad total de fotogramas
		private ushort mCantidadDeFotogramas;

		//Repetir la animacion una vez termina de reproducirse
		private bool mRepetir = false;

		//Extension de los fotogramas
		private string mExtensionImagen;

		//Delegado que lleva a cabo la logica para actualizar el fotograma
		private SendOrPostCallback actualizarFotogramaActual;

		//Reloj encargado de medir el paso del tiempo
		private Stopwatch reloj = new Stopwatch();

		//Diccionario que relaciones los paths con su indice
		private Dictionary<int, string> mPathsCacheados;


		//-----------------------PROPIEDADES------------------------------


		/// <summary>
		/// Propiedad que indica si la animacion esta pausada
		/// </summary>
		public bool EstaPausado => !reloj.IsRunning;


		//-------------------------EVENTOS-----------------------------


		public event EventHandler<Animacion> OnAnimacionFinalizada = delegate { };

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor de <see cref="Animacion"/>
		/// </summary> 
		/// <param name="_actualizarFotogramaActual">
		/// Delegado encargado de llevar a cabo la logica para actualizar el fotograma.
		/// Toma como parametro un object que es una referencia a un string que contiene el path completo del fotograma a mostrar</param>
		/// <param name="_iteracionesPorSegundo">Cantidad de veces que la animacion se repetira en un segundo</param>
		/// <param name="_cantidadDeFotogramas">Cantidad de fotogramas que tiene la animacion</param>
		/// <param name="_pathFotogramas">Ruta absoluta de los fotogramas, estos deben estar diferenciados el uno del otro por un numero</param>
		/// <param name="_formatoFotogramas">Formato de imagen de los fotogramas</param>
		/// <param name="_repetir">Repetir la animacion desde cero cuando termine</param>
		public Animacion(
			SendOrPostCallback _actualizarFotogramaActual,
			float _iteracionesPorSegundo,
			ushort _cantidadDeFotogramas,
			string _pathFotogramas,
			EFormatoImagen _formatoFotogramas,
			bool _repetir)
		{
			mCantidadDeFotogramas = _cantidadDeFotogramas;
			mRepetir = _repetir;
			actualizarFotogramaActual = _actualizarFotogramaActual;

			mIntervaloEntreFotogramas = ((1.0f / mCantidadDeFotogramas) / _iteracionesPorSegundo) * 1000f;

			mExtensionImagen = _formatoFotogramas == EFormatoImagen.Png ? ".png" : ".jpg";

			mPathsCacheados = new Dictionary<int, string>(mCantidadDeFotogramas);

			for (; _cantidadDeFotogramas > 0; --_cantidadDeFotogramas)
			{
				mPathsCacheados.Add(_cantidadDeFotogramas, _pathFotogramas + _cantidadDeFotogramas + mExtensionImagen);
			}

			reloj.Start();
		} 

		#endregion

		#region Metodos

		/// <summary>
		/// Funcion que se encargada de actualizar los fotogramas
		/// </summary>
		public void Tick()
		{
			//Si el tiempo transcurrido ya es mayor o igual al intervalo entre los fotogramas...
			if (reloj.ElapsedMilliseconds >= mIntervaloEntreFotogramas)
			{
				//Si no repetiremos la animacion y el fotograma ya es el ultimo...
				if (!mRepetir && mFotogramaActual >= mCantidadDeFotogramas - 1)
				{
					//Disparamos el evento de animacion finalizada
					OnAnimacionFinalizada(this, this);

					//Salimos
					return;
				}

				//Llamamos desde el hilo principal el delegado de actualizar fotograma
				SistemaPrincipal.ThreadUISyncContext.Post(actualizarFotogramaActual, mPathsCacheados[mFotogramaActual + 1]);

				//Actualizamos el indice del fotograma actual
				mFotogramaActual = ++mFotogramaActual % mCantidadDeFotogramas;

				//Reiniciamos el reloj
				reloj.Restart();
			}
		}

		/// <summary>
		/// Pausa la reproduccion de la animacion
		/// </summary>
		public void Pausar()
		{
			//Pausamos el reloj
			reloj.Stop();
		}

		/// <summary>
		/// Resume la reproduccion de la animacion
		/// </summary>
		public void Resumir()
		{
			//Reanudamos el reloj
			reloj.Start();
		} 

		#endregion
    }
}