using System;
using System.Threading.Tasks;
using System.Windows;
using AppGM.Core;

namespace AppGM.Viewmodels
{
    class ViewModelVentanaMensaje : ViewModelVentanaBase, IVentanaMensaje
    {
        #region Miembros
        public bool DebeEsperarCierre { get; set; } = false;
        public ViewModelMensajeBase ViewModelContenidoVentana { get; set; }

        #endregion

        #region Constructores

        public ViewModelVentanaMensaje(Window _ventana) : base(_ventana) { }

        #endregion

        #region Implementacion Interfaz Ventana Mensaje

        /// <summary>
        /// Muestra una ventana con contenido que se superpone sobre la ventana principal
        /// </summary>
        /// <param name="vm">View model del contenido que albergara la ventana</param>
        /// <param name="esperarCierre">Si es true no se podra interactuar con la ventana principal hasta que esta se cierre</param>
        /// <param name="alto">Alto de la ventana, si se deja el valor default esta simplemente se iniciara con los valores default</param>
        /// <param name="ancho">Ancho de la ventana, si se deja el valor default esta simplemente se iniciara con los valores default</param>
        /// <returns></returns>
        public async Task Mostrar(ViewModelMensajeBase vm, bool esperarCierre, int alto = -1, int ancho = -1)
        {
            ViewModelContenidoVentana = vm;

            TituloVentana = vm.Titulo;

            mVentana.Height = alto != -1 ? alto : mVentana.Height;
            mVentana.Width = ancho != -1 ? ancho : mVentana.Width;

            if (esperarCierre)
            {
                DebeEsperarCierre = true;

                await mVentana.Dispatcher.BeginInvoke(new Action(() =>
                {
                    mVentana.ShowDialog();
                }));

                DebeEsperarCierre = false;

                return;
            }

            mVentana.Show();
        }

        public void EstablecerViewModel(ViewModelMensajeBase nuevoVM) => ViewModelContenidoVentana = nuevoVM;

        public bool EstaAbierta => mVentana.IsVisible;

        public override void CerrarVentana() => mVentana.Hide();

        #endregion
    }
}