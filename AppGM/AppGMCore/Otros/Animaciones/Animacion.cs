using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;

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

        private BaseViewModel elementoContenedor;
        private PropertyInfo  propiedad;
        private Stopwatch     reloj = new Stopwatch();

        public event dEventoAnimacion OnAnimacionFinalizada = delegate{};
        public Animacion(
            Expression<Func<string>> _propiedad,
            float _iteracionesPorSegundo,
            ushort _cantidadDeFotogramas,
            string _pathFotogramas,
            EFormatoImagen _formatoFotogramas,
            BaseViewModel _elementoContenedor,
            bool _repetir)
        {
            pathFotogramas            = _pathFotogramas;
            mCantidadDeFotogramas     = _cantidadDeFotogramas;
            mIntervaloEntreFotogramas = ((1.0f / mCantidadDeFotogramas) / _iteracionesPorSegundo) * 1000f;
            mRepetir                  = _repetir;

            mExtensionImagen = _formatoFotogramas == EFormatoImagen.Png ? ".png" : ".jpg";

            
            MemberExpression mex = _propiedad.Body as MemberExpression;

            propiedad = mex.Member as PropertyInfo;

            elementoContenedor = _elementoContenedor;

            reloj.Start();
        }

        public void Tick()
        {
            if (reloj.ElapsedMilliseconds > mIntervaloEntreFotogramas)
            {
                mFotogramaActual = ++mFotogramaActual % mCantidadDeFotogramas;

                propiedad.SetValue(elementoContenedor, pathFotogramas + mFotogramaActual + mExtensionImagen);

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