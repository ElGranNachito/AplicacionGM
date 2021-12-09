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
    /// Convierte una ruta completa o un arreglo de bytes a una imagen. Si la conversion falla se devuelve una imagen por defecto que se
    /// puede especificar a traves del parametro
    /// </summary>
    [ValueConversion(sourceType: typeof(string), targetType: typeof(BitmapImage), ParameterType = typeof(string))]
    public class FullPathToImageConverter : BaseConverter<FullPathToImageConverter>
    {
        //Diccionario con BitmapImages cacheados para no tener que estar creandolo cada vez que tratamos de acceder a una misma imagen
        private Dictionary<string, BitmapImage> mImagenesCacheadas = new Dictionary<string, BitmapImage>(10);
        
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
	        if (value is null)
		        return ObtenerImagenPorDefecto(parameter);

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

			        return ObtenerImagenPorDefecto(parameter);
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

        private BitmapImage ObtenerImagenPorDefecto(object parametroExtra)
        {
	        if (parametroExtra is string nombreImagen)
	        {
		        if (nombreImagen.IsNullOrWhiteSpace())
			        return null;

		        try
		        {
			        return new BitmapImage(new Uri($"pack://application:,,,/Media/Imagenes/{nombreImagen}"));
		        }
		        catch (Exception ex)
		        {
                    SistemaPrincipal.LoggerGlobal.Log($"Ocurrio un error al obtener la imagen por defecto: {ex.Message}", ESeveridad.Error);

                    return null;
		        }
	        }

	        return new BitmapImage(new Uri("pack://application:,,,/Media/Imagenes/CamaritaMarcaJuancha.png"));
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