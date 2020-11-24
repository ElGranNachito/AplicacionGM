﻿using System.Windows;
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

            //Creamos el controlador de archivos
            SistemaPrincipal.CrearControladorDeArchivos(new ControladorDeArchivos_Windows());

            //Inicializamos el sistema principal
            SistemaPrincipal.Inicializar();

            MainWindow = new MainWindow();
            MainWindow.Show();

            ControladorDeAnimaciones.Inicializar();
        }
    }
}
