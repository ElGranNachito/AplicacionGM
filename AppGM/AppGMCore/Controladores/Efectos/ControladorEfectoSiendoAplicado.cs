using System.Collections.Generic;

namespace AppGM.Core.Controladores.Efectos
{
	/// <summary>
	/// Controlador de <see cref="ModeloEfectoSiendoAplicado"/>
	/// </summary>
	public class ControladorEfectoSiendoAplicado : Controlador<ModeloEfectoSiendoAplicado>
	{
		#region Campos & Propiedades

		//---------------------------------------CAMPOS--------------------------------------


		/// <summary>
		/// Personaje que aplico el efecto
		/// </summary>
		public readonly ControladorPersonaje instigador;

		/// <summary>
		/// Personajes a quienes se les esta aplicando el efecto
		/// </summary>
		public readonly List<ControladorPersonaje> objetivos;

		/// <summary>
		/// Controlador del efecto
		/// </summary>
		public readonly ControladorEfecto controladorEfecto;


		//-------------------------------------PROPIEDADES----------------------------------


		/// <summary>
		/// <see cref="ModeloEfecto"/> que representa este <see cref="ModeloEfectoSiendoAplicado"/>
		/// </summary>
		public ModeloEfecto Efecto => controladorEfecto.modelo;

		/// <summary>
		/// <see cref="ModeloEfectoSiendoAplicado"/> que este controlador contiene
		/// </summary>
		public ModeloEfectoSiendoAplicado AplicacionEfecto => modelo; 

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="efecto">Controlador del <see cref="ModeloEfecto"/> que se aplicara</param>
		public ControladorEfectoSiendoAplicado(ControladorEfecto _controladorEfecto, ControladorPersonaje _instigador, List<ControladorPersonaje> _objetivos)
		{
			controladorEfecto = _controladorEfecto;
			instigador        = _instigador;
			objetivos         = _objetivos;
			
			modelo = new ModeloEfectoSiendoAplicado();
			modelo.Inicializar(controladorEfecto, instigador, objetivos);
		}

		/// <summary>
		/// Indica si este efecto puede ser aplicado
		/// </summary>
		/// <param name="objetivo">Objetivo sobre el cual revisar si se puede aplicar el efecto</param>
		/// <returns></returns>
		public bool PuedeAplicarEfecto(ControladorPersonaje objetivo)
		{
			return controladorEfecto.PuedeAplicarEfecto(instigador, objetivo);
		}

		/// <summary>
		/// Aplica el efecto sobre un <paramref name="objetivo"/>
		/// </summary>
		/// <param name="objetivo">Objetivo sobre el cual aplicar el efecto</param>
		public void AplicarEfecto(ControladorPersonaje objetivo)
		{
			controladorEfecto.AplicarEfecto(instigador, objetivo);
		}

		/// <summary>
		/// Quita el efecto de un <paramref name="objetivo"/>
		/// </summary>
		/// <param name="objetivo"><see cref="ControladorPersonaje"/> al que se le quitara el efecto</param>
		public void QuitarEfecto(ControladorPersonaje objetivo)
		{
			controladorEfecto.QuitarEfecto(instigador, objetivo);

			objetivos.Remove(objetivo);

			//Si el efecto ya no esta siendo aplicado a nadie...
			if (objetivos.Count <= 0)
				Eliminar();
		}

		/// <summary>
		/// Quita el efecto de todos los <see cref="objetivos"/>
		/// </summary>
		public void QuitarEfectoATodosLosAfectados()
		{
			foreach (var obj in objetivos)
				controladorEfecto.QuitarEfecto(instigador, obj);
		}

		public override void Eliminar()
		{
			SistemaPrincipal.EliminarModelo(modelo);


		}
	}
}