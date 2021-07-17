using System;
using System.Collections.Generic;

namespace AppGM.Core
{
	/// <summary>
	/// Interfaz que implementan los <see cref="ViewModel"/> que contienen <see cref="ViewModelBloqueFuncionBase"/>
	/// </summary>
	public interface IContenedorDeBloques
	{
		#region Propiedades

		/// <summary>
		/// <see cref="ViewModelBloqueFuncionBase"/> contenidos
		/// </summary>
		public ViewModelListaDeElementos<ViewModelBloqueFuncionBase> Bloques { get; set; }

		/// <summary>
		/// <see cref="IContenedorDeBloques"/> que contiene este elemento
		/// </summary>
		public virtual IContenedorDeBloques Padre
		{
			get => null;
			set{}
		}

		#endregion

		#region Metodos

		/// <summary>
		/// Añade un <paramref name="bloque"/> a <see cref="Bloques"/> en <paramref name="indiceBloque"/>
		/// </summary>
		/// <param name="bloque"><see cref="ViewModelBloqueFuncionBase"/> que sera añadido</param>
		/// <param name="indiceBloque">Indice en el que se añadira el bloque</param>
		void AñadirBloque(ViewModelBloqueFuncionBase bloque, int indiceBloque = -1);

		/// <summary>
		/// Quita un <paramref name="bloque"/> de <see cref="Bloques"/>
		/// </summary>
		/// <param name="bloque"><see cref="ViewModelBloqueFuncionBase"/> que quitar</param>
		void QuitarBloque(ViewModelBloqueFuncionBase bloque);

		/// <summary>
		/// Añade un <paramref name="bloqueQueInsertar"/> a <see cref="Bloques"/> en la posicion de <paramref name="bloqueDelQueTomarLaPosicion"/>
		/// desplazando a este ultimo hacia abajo
		/// </summary>
		/// <param name="bloqueQueInsertar"><see cref="ViewModelBloqueFuncionBase"/> que sera añadido</param>
		/// <param name="bloqueDelQueTomarLaPosicion"><see cref="ViewModelBloqueFuncionBase"/> que cuya posicion sera tomada por <paramref name="bloqueQueInsertar"/></param>
		virtual void InsertarBloque(ViewModelBloqueFuncionBase bloqueQueInsertar, ViewModelBloqueFuncionBase bloqueDelQueTomarLaPosicion)
		{
			InsertarBloque(bloqueQueInsertar, bloqueDelQueTomarLaPosicion.IndiceBloque);
		}

		/// <summary>
		/// Añade un <paramref name="bloqueQueInsertar"/> a <see cref="Bloques"/> en <paramref name="indiceEnQueInsertarlo"/>
		/// </summary>
		/// <param name="bloqueQueInsertar"><see cref="ViewModelBloqueFuncionBase"/> que sera añadido</param>
		/// <param name="indiceEnQueInsertarlo">Indice en el que se añadira el bloque</param>
		virtual void InsertarBloque(ViewModelBloqueFuncionBase bloqueQueInsertar, int indiceEnQueInsertarlo)
		{
			//Si el bloque ya existe en la lista entonces lo movemos
			if (Bloques.Contiene(bloqueQueInsertar))
			{
				Bloques.Elementos.Move(bloqueQueInsertar.IndiceBloque, indiceEnQueInsertarlo);
			}
			else
				Bloques.Insert(indiceEnQueInsertarlo, bloqueQueInsertar);

			//Obtenemos el indice anterior del bloque. En caso de que sea -1 tomamos el ultimo indice de la lista
			int indiceAnterior = bloqueQueInsertar.IndiceBloque != -1 ? bloqueQueInsertar.IndiceBloque : Bloques.Count - 1;

			//Actualizamos los indices de los bloques para que reflejen los cambios anteriores
			ActualizarIndicesBloques(Math.Min(indiceAnterior, indiceEnQueInsertarlo));
		}

		/// <summary>
		/// Actualiza los <see cref="ViewModelBloqueFuncionBase.indiceBloque"/> de todos los <see cref="ViewModelBloqueFuncionBase"/>
		/// en <see cref="BloquesColocados"/> a partir de <paramref name="indicePorElQueComenzar"/>
		/// </summary>
		/// <param name="indicePorElQueComenzar">IndiceZ a partir del cual comenzar a actualizar</param>
		virtual void ActualizarIndicesBloques(int indicePorElQueComenzar)
		{
			for (int i = indicePorElQueComenzar; i < Bloques.Count; ++i)
				Bloques[i].IndiceBloque = i;
		}

		/// <summary>
		/// Obtiene una <see cref="List{T}"/> de todos los <see cref="BloqueVariable"/> disponibles para <paramref name="bloqueQueIntentaObtenerLasVariables"/>
		/// </summary>
		/// <param name="bloqueQueIntentaObtenerLasVariables"><see cref="ViewModelBloqueFuncionBase"/> que quiere obtener las variables</param>
		/// <returns> <see cref="List{T}"/> de todos los <see cref="BloqueVariable"/> disponibles</returns>
		List<BloqueVariable> ObtenerVariables(ViewModelBloqueFuncionBase bloqueQueIntentaObtenerLasVariables); 

		#endregion
	}
}