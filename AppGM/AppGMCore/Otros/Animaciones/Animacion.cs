using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;

namespace AppGM.Core
{
    public class Animacion
    {
        public string pathFotogramas;

        private float intervaloEntreFotogramas;
        private int fotogramaActual = 0;
        private ushort cantidadDeFotogramas;

        private string extensionImagen;

        private BaseViewModel elementoContenedor;
        private PropertyInfo  propiedad;
        private Stopwatch     reloj = new Stopwatch();
        public Animacion(
            Expression<Func<string>> _propiedad,
            float _iteracionesPorSegundo,
            ushort _cantidadDeFotogramas,
            string _pathFotogramas,
            EFormatoImagen _formatoFotogramas,
            BaseViewModel _elementoContenedor)
        {
            pathFotogramas           = _pathFotogramas;
            cantidadDeFotogramas     = _cantidadDeFotogramas;
            intervaloEntreFotogramas = ((1.0f / cantidadDeFotogramas) / _iteracionesPorSegundo) * 1000f;

            extensionImagen = _formatoFotogramas == EFormatoImagen.Png ? ".png" : ".jpg";

            MemberExpression mex = _propiedad.Body as MemberExpression;

            propiedad = mex.Member as PropertyInfo;

            elementoContenedor = _elementoContenedor;

            reloj.Start();
        }

        public void Tick()
        {
            if (reloj.ElapsedMilliseconds > intervaloEntreFotogramas)
            {
                fotogramaActual = ++fotogramaActual % cantidadDeFotogramas;

                propiedad.SetValue(elementoContenedor, pathFotogramas + fotogramaActual + extensionImagen);

                reloj.Restart();
            }
        }
    }
}