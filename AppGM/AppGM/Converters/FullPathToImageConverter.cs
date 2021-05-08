using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media.Imaging;
using AppGM.Core;

namespace AppGM
{
    public class FullPathToImageConverter : BaseConverter<FullPathToImageConverter>
    {
        //Diccionario con BitmapImages cacheados para no tener que estar creandolo cada vez que tratamos de acceder a una misma imagen
        private Dictionary<string, BitmapImage> imagenesCacheadas = new Dictionary<string, BitmapImage>(10);
        
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tmp = (string) value;

            if (String.IsNullOrEmpty(tmp))
                return null;

            //Si la BitmapImage ya existe entonces la obtenemos
            if (imagenesCacheadas.ContainsKey(tmp))
                return imagenesCacheadas[tmp];

            //Si no existe la creamos y luego la añadimos al diccionario
            BitmapImage bitmapImage = new BitmapImage(new Uri(tmp, UriKind.Absolute));

            imagenesCacheadas.Add(tmp, bitmapImage);

            return bitmapImage;
        }

        public FullPathToImageConverter()
        {
            //Cuando la pagina actual cambie limpiamos el cache ya que las imagenes que utilizaremos seran otras y no nos interesa mantener los Bitmaps anteriores en memoria
            SistemaPrincipal.Aplicacion.OnPaginaActualCambio += (anterior, pagina) =>
            {
                imagenesCacheadas.Clear();
            };
        }
    }
}