using System;
using System.Windows.Input;

namespace AppGM.Core
{
    /// <summary>
    /// Representa un metodo a ser ejecutado cuando el usuario interactua de alguna manera con la UI
    /// </summary>
    public class Comando : ICommand
    {
        private event EventHandler mCanExecuteChanged;

        /// <summary>
        /// Funcion a ejecutar
        /// </summary>
        private Action mLambda;

        /// <summary>
        /// Predicado para determinar si el comando puede ejecutarse;
        /// </summary>
        private Predicate<object> mPuedeEjecutarse;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lambda">Funcion a ejecutar</param>
        public Comando(Action lambda)
        {
            mLambda = lambda;

            mPuedeEjecutarse = obj => true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lambda">Funcion a ejecutar</param>
        /// <param name="puedeEjecutarse">Predicado que para determinar si el comando puede ejecutarse</param>
        public Comando(Action lambda, Predicate<object> puedeEjecutarse)
        {
            mLambda          = lambda;
            mPuedeEjecutarse = puedeEjecutarse;
        }

        /// <summary>
        /// Indica si el comando puede ser ejecutado
        /// </summary>
        /// <param name="parametro">Parametro con el que intentar ejecutar</param>
        /// <returns><see cref="bool"/> indicando si el comando puede ser ejecutado</returns>
        public bool CanExecute(object parametro)
        {
            return mPuedeEjecutarse(parametro);
        }

        /// <summary>
        /// Ejecuta el comando
        /// </summary>
        /// <param name="parameter">Parametro del comando</param>
        public void Execute(object parameter)
        {
            mLambda();
        }

        /// <summary>
        /// Subscribe o desubscribe funciones de <see cref="mCanExecuteChanged"/>
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add    => mCanExecuteChanged += value;
            remove => mCanExecuteChanged -= value;
        }

    }
}
