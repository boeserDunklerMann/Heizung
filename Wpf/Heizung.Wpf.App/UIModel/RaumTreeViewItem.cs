using System;
using System.Collections.Generic;
using System.Text;
using Heizung.Model;

namespace Heizung.Wpf.App.UIModel
{
	public class RaumTreeViewItem : BaseTreeViewItem
	{
		public RaumTreeViewItem(Raum r) : base(r)
		{
			r.Messpunkte.ForEach(mp =>
			{
				MesspunktTreeViewItem mptvi = new MesspunktTreeViewItem(mp);
				Children.Add(mptvi);
			});
		}
	}
}