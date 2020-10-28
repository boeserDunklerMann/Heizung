using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Heizung.Test.Wpf.TreeView
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			WohnungsItem root = new WohnungsItem() { Bezeichnung = "meine Wohnung" };
			WohnungsItem bad = new WohnungsItem() { Bezeichnung = "Bad" };
			bad.Children.Add(new WohnungsItem() { Bezeichnung = "HKV" });
			bad.Children.Add(new WohnungsItem() { Bezeichnung = "KWZ" });
			root.Children.Add(bad);
			root.Children.Add(new WohnungsItem() { Bezeichnung = "Küche" });
			tvWohnung.Items.Add(root);
		}
	}
	public class WohnungsItem
	{
		public string Bezeichnung { get; set; }
		public ObservableCollection<WohnungsItem> Children { get; set; } = new ObservableCollection<WohnungsItem>();
	}
}
