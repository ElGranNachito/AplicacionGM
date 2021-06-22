using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppGM
{
	/// <summary>
	/// Lógica de interacción para UserControlBloqueBase.xaml
	/// </summary>
	public partial class UserControlBloqueBase : UserControl
	{
		public UserControlBloqueBase()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ColorBordeBloqueProperty = 
			DependencyProperty.Register("ColorBordeBloque", typeof(SolidColorBrush), typeof(UserControlBloqueBase));

		public SolidColorBrush ColorBordeBloque
		{
			get => (SolidColorBrush)GetValue(ColorBordeBloqueProperty);
			set => SetValue(ColorBordeBloqueProperty, value);
		}

		public static readonly DependencyProperty ColorRellenoBloqueProperty =
			DependencyProperty.Register("ColorRellenoBloque", typeof(SolidColorBrush), typeof(UserControlBloqueBase));

		public SolidColorBrush ColorRellenoBloque
		{
			get => (SolidColorBrush)GetValue(ColorRellenoBloqueProperty);
			set => SetValue(ColorRellenoBloqueProperty, value);
		}
	}
}