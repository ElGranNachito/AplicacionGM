using System.Windows;
using AppGM.Core;
using AppGMCore;

namespace AppGM
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Inicializamos el sistema principal
            SistemaPrincipal.Inicializar();

            MainWindow = new MainWindow();
        }
    }
}
