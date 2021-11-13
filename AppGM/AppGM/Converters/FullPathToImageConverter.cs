using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using AppGM.Core;
using CoolLogs;

namespace AppGM
{
    /// <summary>
    /// Convierte una ruta completa a una imagen. No creo que haga falta aclarar que esa ruta completa de be ser a una imagen
    /// </summary>
    [ValueConversion(sourceType: typeof(string), targetType: typeof(BitmapImage))]
    public class FullPathToImageConverter : BaseConverter<FullPathToImageConverter>
    {
        //Diccionario con BitmapImages cacheados para no tener que estar creandolo cada vez que tratamos de acceder a una misma imagen
        private Dictionary<string, BitmapImage> mImagenesCacheadas = new Dictionary<string, BitmapImage>(10);
        
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
	        if (value is null)
		        return null;

	        BitmapImage nuevaImagen = null;

            //Si la fuente de la imagen es un arreglo de bytes
	        if (value is byte[] bytesImagen)
	        {
		        nuevaImagen = new BitmapImage();

		        using MemoryStream streamImagen = new MemoryStream(bytesImagen);

                nuevaImagen.BeginInit();

                nuevaImagen.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                nuevaImagen.CacheOption   = BitmapCacheOption.OnLoad;
                nuevaImagen.UriSource     = null;
                nuevaImagen.StreamSource  = streamImagen;

                nuevaImagen.EndInit();
	        }
            //Si es un path
	        else
	        {
		        string tmp = (string)value;

		        if (!File.Exists(tmp))
		        {
                    SistemaPrincipal.LoggerGlobal.Log($"Se intento cargar una imagen que no existe ({tmp})", ESeveridad.Error);

			        return null;
		        }

		        if (String.IsNullOrEmpty(tmp))
			        return null;

		        //Si la BitmapImage ya existe entonces la obtenemos
		        if (mImagenesCacheadas.ContainsKey(tmp))
			        return mImagenesCacheadas[tmp];

		        //Si no existe la creamos y luego la añadimos al diccionario
		        nuevaImagen = new BitmapImage(new Uri(tmp, UriKind.Absolute));

		        mImagenesCacheadas.Add(tmp, nuevaImagen);
            }
            

            if (nuevaImagen.CanFreeze)
            {
	            nuevaImagen.Freeze();
            }
            else
			{
                SistemaPrincipal.LoggerGlobal.Log($"No se pudo congelar imagen {nuevaImagen}", ESeveridad.Advertencia);
			}
            
            return nuevaImagen;
        }

        public FullPathToImageConverter()
        {
            //Cuando la pagina actual cambie limpiamos el cache ya que las imagenes que utilizaremos seran otras y no nos interesa mantener los Bitmaps anteriores en memoria
            SistemaPrincipal.Aplicacion.OnPaginaActualCambio += (anterior, pagina) =>
            {
                mImagenesCacheadas.Clear();
            };
        }
    }
}