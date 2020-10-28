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

namespace Heizung.Wpf.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			VM.MainWindowViewModel vm = DataContext as VM.MainWindowViewModel;
			tvWohnung.Items.Add(vm.Wohnung);
		}

		private void tvWohnung_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			(DataContext as VM.MainWindowViewModel).SelectedTVItem = (UIModel.BaseTreeViewItem)e.NewValue;
		}
	}
}
