using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Heizung.Model;

namespace Heizung.Wpf.App.VM
{
	public class MainWindowViewModel : DA.lib.MVVM.Framework.ObservableObject
	{
		private readonly ReSTWrapper.Wohnung wohnungRest;
		public MainWindowViewModel()
		{
			wohnungRest = ReSTWrapper.Wohnung.Instance;
			LoadData();
		}

		private UIModel.WohnungTreeViewItem wohnung;
		/// <summary>
		/// der Root-Knoten im TV
		/// </summary>
		public UIModel.WohnungTreeViewItem Wohnung
		{
			get => wohnung;
			set
			{
				wohnung = value;
				RaisePropertyChangedEvent(nameof(Wohnung));
			}
		}


		private UIModel.BaseTreeViewItem _selectedTVItem;
		/// <summary>
		/// das im TV asugewählte Item
		/// </summary>
		public UIModel.BaseTreeViewItem SelectedTVItem
		{
			get => _selectedTVItem;
			set
			{
				_selectedTVItem = value;
				if (_selectedTVItem is UIModel.MesspunktTreeViewItem)
					MesspunktSelected();
				RaisePropertyChangedEvent(nameof(SelectedTVItem));
			}
		}

		private Model.MessWert _selectedMW;
		/// <summary>
		/// der im Grid ausgewählte Messwert
		/// </summary>
		public Model.MessWert SelectedMesswert
		{
			get => _selectedMW;
			set
			{
				_selectedMW = value;
				RaisePropertyChangedEvent(nameof(SelectedMesswert));
			}
		}

		private Model.Messpunkt _selectedMP;
		/// <summary>
		/// der im TV ausgewählte Messpunkt (wenns denn einer ist).
		/// </summary>
		public Model.Messpunkt SelectedMesspunkt
		{
			get => _selectedMP;
			set
			{
				_selectedMP = value;
				RaisePropertyChangedEvent(nameof(SelectedMesspunkt));
			}
		}

		public void LoadData()
		{
			var wohnungen = wohnungRest.GetAllWohnungen();
			Wohnung = new UIModel.WohnungTreeViewItem(wohnungen[0]); // TODO: was ist, wenns hier mehrere gibt?
		}

		public void MesspunktSelected()
		{
			SelectedMesspunkt = _selectedTVItem.Data as Model.Messpunkt;
			Messwerte = new ObservableCollection<Model.MessWert>(_selectedMP.Werte);
		}
		
		private ObservableCollection<Model.MessWert> messWerte;
		public ObservableCollection<Model.MessWert> Messwerte
		{
			get => messWerte;
			set
			{
				messWerte = value;
				RaisePropertyChangedEvent(nameof(Messwerte));
			}
		}

		#region Commands
		public DA.lib.MVVM.Framework.DelegateCommand AddNewMW => new DA.lib.MVVM.Framework.DelegateCommand(AddNewMesswert);
		public DA.lib.MVVM.Framework.DelegateCommand SaveAllCmd => new DA.lib.MVVM.Framework.DelegateCommand(SaveAll);
		public DA.lib.MVVM.Framework.DelegateCommand ReloadCmd => new DA.lib.MVVM.Framework.DelegateCommand(LoadData);
		#endregion

		#region Delegates & Events
		public delegate void NoMPSelectedDelegate(object sender);
		public event NoMPSelectedDelegate OnNoMPSelected;
		#endregion

		private void AddNewMesswert()
		{
			if (_selectedMP != null)
			{
				Model.MessWert mw = new Model.MessWert() { MesspunktID = _selectedMP.MesspunktID, Wert = 0, Stamp = DateTime.Now };
				_selectedMP.Werte.Add(mw);
				messWerte.Add(mw);
				RaisePropertyChangedEvent(nameof(SelectedMesspunkt));
				SelectedMesswert = mw;
				RaisePropertyChangedEvent(nameof(SelectedMesswert));
			}
			else
				OnNoMPSelected?.Invoke(this);
		}

		private void SaveAll()
		{
			ReSTWrapper.Messwert messwertrest = ReSTWrapper.Messwert.Instance;
			//ReSTWrapper.Messpunkt messpunktrest = ReSTWrapper.Messpunkt.Instance;
			//ReSTWrapper.Raum raumrest = ReSTWrapper.Raum.Instance;

			Wohnung w = Wohnung.Data as Heizung.Model.Wohnung;
			w.Raeume.ForEach(raum =>
			{
				raum.Messpunkte.ForEach(mp =>
				{
					mp.Werte.ForEach(wert =>
					{
						messwertrest.WriteMesswert(wert);
					});
				});
			});
		}
	}
}