using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace AppGM.Core
{
    public delegate void dEventoAnimacion(Animacion animacion);
    
    public class Animacion
    {
        public string  pathFotogramas;

        private float  mIntervaloEntreFotogramas;
        private int    mFotogramaActual = 0;
        private ushort mCantidadDeFotogramas;

        private bool mRepetir = false;

        private string mExtensionImagen;

        private SendOrPostCallback actualizarFotogramaActual;
        private Stopwatch          reloj = new Stopwatch();

        private Dictionary<int, string> mPathsCacheados;

        public event dEventoAnimacion OnAnimacionFinalizada = delegate{};
        public Animacion(
            SendOrPostCallback _actualizarFotogramaActual,
            float _iteracionesPorSegundo,
            ushort _cantidadDeFotogramas,
            string _pathFotogramas,
            EFormatoImagen _formatoFotogramas,
            BaseViewModel _elementoContenedor,
            bool _repetir)
        {
            pathFotogramas            = _pathFotogramas;
            mCantidadDeFotogramas     = _cantidadDeFotogramas;
            mRepetir                  = _repetir;
            actualizarFotogramaActual = _actualizarFotogramaActual;

            mIntervaloEntreFotogramas = ((1.0f / mCantidadDeFotogramas) / _iteracionesPorSegundo) * 1000f;

            mExtensionImagen = _formatoFotogramas == EFormatoImagen.Png ? ".png" : ".jpg";

            mPathsCacheados = new Dictionary<int, string>(mCantidadDeFotogramas);

            for (; _cantidadDeFotogramas > 0; --_cantidadDeFotogramas)
            {
                mPathsCacheados.Add(_cantidadDeFotogramas, pathFotogramas + _cantidadDeFotogramas + mExtensionImagen);
            }

            reloj.Start();
        }

        public void Tick()
        {
            if (reloj.ElapsedMilliseconds > mIntervaloEntreFotogramas)
            {
                SistemaPrincipal.ThreadUISyncContext.Post(actualizarFotogramaActual, mPathsCacheados[mFotogramaActual + 1]);

                mFotogramaActual = ++mFotogramaActual % mCantidadDeFotogramas;

                if (mFotogramaActual >= mCantidadDeFotogramas - 1 && !mRepetir)
                    OnAnimacionFinalizada(this);

                reloj.Restart();
            }
        }

        public void Pausar()
        {
            ControladorDeAnimaciones.QuitarAnimacion(this);

            reloj.Stop();
        }

        public void Resumir()
        {
            ControladorDeAnimaciones.AñadirAnimacion(this);

            reloj.Start();
        }

        public bool EstaPausado => !reloj.IsRunning;
    }
}