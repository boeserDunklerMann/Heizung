using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Wpf.Controls.VM
{
	public class WohnungTreeViewViewModel
	{
		public Heizung.Wpf.Model.WohnungTreeViewItem Wohnung { get; set; }
		public static WohnungTreeViewViewModel Instance { get; private set; }
		public WohnungTreeViewViewModel()
		{
			Instance = this;
		}
	}
}
