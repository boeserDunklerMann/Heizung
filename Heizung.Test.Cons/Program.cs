using System;
using Heizung.DBAccess;
using Heizung.Model;

namespace Heizung.Test.Cons
{
	class Program
	{
		static void Main(string[] args)
		{
			DBAccess.MySql db = DBAccess.MySql.Instance;
			db.SetConnection("heizung", "heizung", "192.168.1.3", "Heizung");
			/*
			var wohnungen = db.LoadAll();
			MessWert w = new MessWert
			{
				Wert = 512,
				MesspunktID = 6,
				ID = 6
			};
			db.WriteMesswert(w);
			*/
			ReSTWrapper.Wohnung wohnungrest = ReSTWrapper.Wohnung.Instance;
			ReSTWrapper.Raum raumrest = ReSTWrapper.Raum.Instance;
			var w = wohnungrest.GetAllWohnungen().Result;
			var r = raumrest.GetRaeume(w[0]).Result;
			Model.Wohnung wohnung1 = w[0];
			wohnung1.Raeume.Add(new Raum() { Bezeichnung = "Keller", WohnungID = wohnung1.WohnungID });
			wohnungrest.WriteWohnung(wohnung1);
			//db.WriteWohnung(wohnung1);
		}
	}
}
