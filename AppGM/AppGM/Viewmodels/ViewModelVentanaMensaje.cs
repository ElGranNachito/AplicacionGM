using System;
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
        public async void Mostrar(ViewModelMensajeBase vm, bool esperarCierre)
        {
            ViewModelContenidoVentana = vm;

            TituloVentana = vm.Titulo;

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

        public bool EstaAbierta => mVentana.IsVisible;

        public override void CerrarVentana() => mVentana.Hide();

        #endregion
    }
}