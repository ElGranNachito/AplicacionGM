using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace AppGM.Core
{
    //Comentario extra: Es una interfaz porque no tenemos herencia multiple y el controlador de tirada ya hereda de ControladorBase
    /// <summary>
    /// Interfaz que define la funcionalidad base de una tirada
    /// </summary>
    public interface IControladorTiradaBase
    {
        #region Propiedades

        /// <summary>
        /// Resultado de la tirada
        /// </summary>
        int Resultado { get; set; }

        /// <summary>
        /// Para una tirada con varios dados esta propiedad lista todos los numeros que salieron
        /// </summary>
        int[] Resultados { get; set; }

        #endregion

        #region Funciones

        /// <summary>
        /// Reliza la tirada
        /// </summary>
        void RealizarTirada();

        /// <summary>
        /// Realiza la tirada
        /// </summary>
        /// <param name="parametro">Parametro extra, no puede ser null. Si no es necesario usar la sobrecarga que no toma parametros</param>
        void RealizarTirada([NotNull]object parametro);

        #endregion
    }

    /// <summary>
    /// Controlador de una tirada
    /// </summary>
    /// <typeparam name="TipoTirada"></typeparam>
    public abstract class ControladorTirada<TipoTirada> : Controlador<TipoTirada>, IControladorTiradaBase
        where TipoTirada : ModeloTiradaBase, new()
    {
	    #region Implementacion Interfaz

        public int Resultado { get; set; }

        public int[] Resultados { get; set; } = null;

        public virtual void RealizarTirada()
        {
            SistemaPrincipal.LoggerGlobal.Log($@"Llamada funcion {nameof(RealizarTirada)} sin parametros pero no esta implementada. 
													   Intentar llamar su sobrecarga con parametros");
        }

        public virtual void RealizarTirada(object parametro)
        {
            RealizarTirada();
        }

        public override string ToString()
        {
	        if (Resultados != null)
	        {
		        StringBuilder bld = new StringBuilder();

		        for (int i = 0; i < Resultados.Length; ++i)
		        {
			        bld.Append(i != Resultados.Length - 1 ? $"{Resultados[i]}," : $"{Resultados[i]}");
		        }

		        bld.Append(Environment.NewLine);
		        bld.Append($"Total: {Resultado}");

		        return bld.ToString();
	        }


	        return $"Resultado: {Resultado}";
        }

        #endregion
    }

    /// <summary>
    /// Tirada con un numero de caras y dados personalizado
    /// </summary>
    public class ControladorTiradaVariable : ControladorTirada<ModeloTiradaVariable>
    {
        #region Constructor

        public ControladorTiradaVariable(ModeloTiradaVariable _modelo)
        {
            modelo = _modelo;

            Resultados = new int[modelo.Dados];
        }

        #endregion

        #region Funciones

        public override void RealizarTirada()
        {
	        if (Resultados == null || Resultados.Length != modelo.Dados)
		        Resultados = new int[modelo.Dados];

	        Resultado = 0;

	        for (int i = 0; i < modelo.Dados; ++i)
	        {
		        Resultados[i] = RNGCryptoServiceProvider.GetInt32(1, modelo.Caras + 1);

		        Resultado += Resultados[i];
	        }
        }

        #endregion
    }

    /// <summary>
    /// Tirada que depende de una stat
    /// </summary>
    public class ControladorTiradaStat : ControladorTirada<ModeloTiradaStat>
    {
        #region Constructor

        public ControladorTiradaStat(ModeloTiradaStat _modeloTiradaStat)
        {
            modelo = _modeloTiradaStat;
        }

        #endregion

        #region Funciones

        public override void RealizarTirada(object p)
        {
            var personaje = (ControladorPersonaje) p;
            
            personaje.SufrirDaño(500, ETipoDeDaño.Explosivo, null);

            //TODO: Tirar 3d6
        }

        #endregion
    }

    /// <summary>
    /// Tirada de daño
    /// </summary>
    public class ControladorTiradaDaño : ControladorTirada<ModeloTiradaDeDaño>
    {
        #region Constructor

        public ControladorTiradaDaño(ModeloTiradaDeDaño _modeloTiradaStat)
        {
            modelo = _modeloTiradaStat;
        }

        #endregion

        #region Funciones

        public override void RealizarTirada(object p)
        {
            var personaje = (ControladorPersonaje)p;

            personaje.SufrirDaño(500, ETipoDeDaño.Explosivo, null);

            //TODO: Tirar 3d6
        }

        #endregion
    }
}