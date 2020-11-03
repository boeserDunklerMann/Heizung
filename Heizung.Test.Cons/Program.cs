using System;
using Heizung.DBAccess;
using Heizung.Model;

namespace Heizung.Test.Cons
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			DBAccess.MySql db = DBAccess.MySql.Instance;
			db.SetConnection("heizung", "heizung", "192.168.1.3", "Heizung");
			var wohnungen = db.LoadAll();
			MessWert w = new MessWert
			{
				Wert = 512,
				MesspunktID = 6,
				ID = 6
			};
			db.WriteMesswert(w);
			*/
			ReSTWrapper.Wohnung wohnung = ReSTWrapper.Wohnung.Instance;
			ReSTWrapper.Raum raum = ReSTWrapper.Raum.Instance;
			var w = wohnung.GetAllWohnungen().Result;
			var r = raum.GetRaeume(w[0]).Result;

		}
	}
}
