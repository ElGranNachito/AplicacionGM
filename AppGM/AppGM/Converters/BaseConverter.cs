using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AppGM
{
    /// <summary>
    /// Convertidor base para que hereden todas las clases que deseen implementar la funcionalidad de convertidor
    /// </summary>
    /// <typeparam name="TipoConverter">Tipo del convertidor, debe ser la clase que heredo de <see cref="BaseConverter{TipoConverter}"/></typeparam>
    public abstract class BaseConverter<TipoConverter> : MarkupExtension, IValueConverter
	    where TipoConverter : BaseConverter<TipoConverter>, new()
    {
        #region Campos

        /// <summary>
        /// Instancia del <see cref="TipoConverter"/>
        /// </summary>
        private static Lazy<TipoConverter> mInstanciaConverter = new Lazy<TipoConverter>(() => new TipoConverter());

        #endregion

        #region Funciones

        public override object ProvideValue(IServiceProvider serviceProvider) => mInstanciaConverter.Value;

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) => null;
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null; 

        #endregion
    }
}
