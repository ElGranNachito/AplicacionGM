
using System;
using System.Windows.Input;

namespace AppGM.Core
{
    public class Comando : ICommand
    {
        private event EventHandler mCanExecuteChanged;

        private Action mLambda;
        private Predicate<object> mPuedeEjecutarse = obj => true;

        public Comando(Action lambda)
        {
            mLambda = lambda;
        }

        public Comando(Action lambda, Predicate<object> puedeEjecutarse)
        {
            mLambda          = lambda;
            mPuedeEjecutarse = puedeEjecutarse;
        }

        public bool CanExecute(object parametro)
        {
            return mPuedeEjecutarse(parametro);
        }

        public void Execute(object parameter)
        {
            mLambda();
        }

        public event EventHandler CanExecuteChanged
        {
            add    => mCanExecuteChanged += value;
            remove => mCanExecuteChanged -= value;
        }

    }
}
