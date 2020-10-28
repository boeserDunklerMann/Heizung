using System;
using System.Collections.Generic;
using System.Text;
using Heizung.Model;

namespace Heizung.Wpf.App.UIModel
{
	public class WohnungTreeViewItem : BaseTreeViewItem
	{
		public WohnungTreeViewItem(Wohnung dataModel):base(dataModel)
		{
			dataModel.Raeume.ForEach(r =>
			{
				RaumTreeViewItem rtvi = new RaumTreeViewItem(r);
				Children.Add(rtvi);
			});
		}
	}
}
