using System;
using System.Collections.Generic;
using System.Text;
using Heizung.Model;

namespace Heizung.Wpf.Model
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
