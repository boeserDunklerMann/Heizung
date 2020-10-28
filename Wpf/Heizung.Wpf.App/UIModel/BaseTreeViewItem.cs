using Heizung.Model;
using System.Collections.ObjectModel;

namespace Heizung.Wpf.App.UIModel
{
	public class BaseTreeViewItem
	{
		private readonly BaseModel _baseModel;

		public string Bezeichnung
		{
			get => _baseModel?.Bezeichnung;
			private set
			{
				if (_baseModel != null)
					_baseModel.Bezeichnung = value;
			}
		}

		public ObservableCollection<BaseTreeViewItem> Children { get; private set; } = new ObservableCollection<BaseTreeViewItem>();

		public BaseTreeViewItem(BaseModel dataModel)
		{
			_baseModel = dataModel;
		}
	}
}