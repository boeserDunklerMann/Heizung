﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Wpf.App.VM
{
	public class MainWindowViewModel : DA.lib.MVVM.Framework.ObservableObject
	{
		private UIModel.WohnungTreeViewItem wohnung;
		public UIModel.WohnungTreeViewItem Wohnung
		{
			get => wohnung;
			set
			{
				wohnung = value;
				RaisePropertyChangedEvent(nameof(Wohnung));
			}
		}

		private readonly DBAccess.MySql db;
		public MainWindowViewModel()
		{
			db = DBAccess.MySql.Instance;
			db.SetConnection("heizung", "heizung", "192.168.1.3", "Heizung");   // TODO: hole dies aus der Config
			LoadData();
		}

		public void LoadData()
		{
			var wohnungen = db.LoadAll();
			Wohnung = new UIModel.WohnungTreeViewItem(wohnungen[0]); // TODO: was ist, wenns hier mehrere gibt?
		}
	}
}