using System.Windows;
using AppGM.Core;

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

            Current.Dispatcher.Thread.Name = "AppGM - Main";

            //Inicializamos el sistema principal
            SistemaPrincipal.Inicializar(new ControladorDeArchivos_Windows());

            MainWindow = new MainWindow();
            MainWindow.Show();

            ControladorDeAnimaciones.Inicializar();
        }

        protected override void OnExit(ExitEventArgs e)
        {
	        SistemaPrincipal.Apagar(e.ApplicationExitCode);
        }
    }
}
