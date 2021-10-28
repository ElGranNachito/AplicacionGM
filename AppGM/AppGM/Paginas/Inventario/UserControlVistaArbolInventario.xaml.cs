using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppGM
{
	/// <summary>
	/// Lógica de interacción para UserControlVistaArbolInventario.xaml
	/// </summary>
	public partial class UserControlVistaArbolInventario : UserControl
	{
		public UserControlVistaArbolInventario()
		{
			InitializeComponent();

			DataContextChanged += (sender, args) =>
			{

			};

			TreeView.SelectedItemChanged += (sender, args) =>
			{

			};
		}
	}
}
