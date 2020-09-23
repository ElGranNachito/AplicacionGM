using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AppGM
{
    public abstract class BaseConverter<TipoConverter> : MarkupExtension, IValueConverter
        where TipoConverter : BaseConverter<TipoConverter>, new()
    {
        #region Miembros

        private static TipoConverter mInstanciaConverter = new TipoConverter();

        #endregion

        #region Funciones

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mInstanciaConverter;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) => null;
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null; 

        #endregion
    }
}
