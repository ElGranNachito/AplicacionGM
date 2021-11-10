using System;
using System.Threading.Tasks;
using System.Windows;
using AppGM.Core;

namespace AppGM.Viewmodels
{
    class ViewModelVentanaMensaje : ViewModelVentanaBase, IVentanaMensaje
    {
        #region Propiedades

        public bool EstaAbierta => mVentana.IsVisible;

        public IVentana VentanaPadre { get; set; }

        public override Lazy<IVentanaMensaje> VentanaMensaje { get; set; } = new Lazy<IVentanaMensaje>(() => new ViewModelVentanaMensaje(new Window()));

        public ViewModelConResultadoBase ViewModelContenidoVentana { get; set; }

        #endregion

        #region Constructores

        public ViewModelVentanaMensaje(Window _ventana) : base(_ventana) { }

        #endregion

        #region Implementacion Interfaz Ventana Mensaje

        public override async Task<EResultadoViewModel> MostrarMensaje(ViewModelConResultadoBase vm, string titulo, bool esperarCierre, int alto, int ancho)
        {
	        return await ObtenerVentanaMensaje().Mostrar(vm, titulo, esperarCierre, alto, ancho);
        }

        /// <summary>
        /// Muestra una ventana con contenido que se superpone sobre la ventana principal
        /// </summary>
        /// <param name="vm">View model del contenido que albergara la ventana</param>
        /// <param name="esperarCierre">Si es true no se podra interactuar con la ventana principal hasta que esta se cierre</param>
        /// <param name="alto">Alto de la ventana, si se deja el valor default esta simplemente se iniciara con los valores default</param>
        /// <param name="ancho">Ancho de la ventana, si se deja el valor default esta simplemente se iniciara con los valores default</param>
        /// <returns></returns>
        public async Task<EResultadoViewModel> Mostrar(ViewModelConResultadoBase vm, string titulo, bool esperarCierre, int alto = -1, int ancho = -1)
        {
            ViewModelContenidoVentana = vm;

            TituloVentana = titulo;

            mVentana.Height = alto != -1 ? alto : mVentana.Height;
            mVentana.Width = ancho != -1 ? ancho : mVentana.Width;

            //Cuando se establezca el resultado del vm debemos cerrar la ventana
            vm.OnResultadoEstablecido += vm => mVentana.Hide();

            //Si debemos esperar al cierre de la ventana...
            if (esperarCierre)
            {
                VentanaPadre.DebeEsperarCierreDeMensaje = true;

                //Ejecutamos desde el hilo principal la siguiente funcion y esperamos su conclusion...
                await mVentana.Dispatcher.BeginInvoke( new Action(() =>
                {
                    //Mostramos la ventana
                    mVentana.ShowDialog();
                }));

                VentanaPadre.DebeEsperarCierreDeMensaje = false;

                return vm.Resultado;
            }

            //Si no debemos esperar a que termine entonces sencillamente la mostramos
            mVentana.Show();

            return EResultadoViewModel.NoEstablecido;
        }

        public void EstablecerViewModel(ViewModelConResultadoBase nuevoVM) => ViewModelContenidoVentana = nuevoVM;

        public override void CerrarVentana() => mVentana.Hide();

        #endregion
    }
}