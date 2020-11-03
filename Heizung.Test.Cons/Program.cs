using System;
using System.Linq;
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
			ReSTWrapper.Messpunkt messpunktrest = ReSTWrapper.Messpunkt.Instance;
			ReSTWrapper.Messwert messwertrest = ReSTWrapper.Messwert.Instance;

			//var w = wohnungrest.GetAllWohnungen().Result;
			//var r = raumrest.GetRaeume(w[0]).Result;
			//Model.Wohnung wohnung1 = w[0];
			return;

			/** Lösche Keller und Gasuhr **
			Model.Raum keller = wohnung1.Raeume.Last();
			keller.DeleteMe = true;
			Model.Messpunkt gaszaehler = keller.Messpunkte.Last();
			gaszaehler.DeleteMe = true;
			Model.MessWert wert = gaszaehler.Werte.Last();
			wert.DeleteMe = true;
			messwertrest.WriteMesswert(wert).Wait();
			messpunktrest.WriteMesspunkt(gaszaehler).Wait();
			raumrest.WriteRaum(keller).Wait();	// Hier muss es eskalieren, wenn es den Gaszähler in der DB noch gibt.
			/**/

			/** Erstelle Keller, Gasuhr und Messwert **
			Model.Raum keller = new Raum() { Bezeichnung = "Keller", WohnungID = wohnung1.WohnungID };
			keller = raumrest.WriteRaum(keller).Result;
			Model.Messpunkt gaszaehler = new Messpunkt()
			{
				Bezeichnung = "Gasuhr",
				RaumID = keller.RaumID,
				Einheit = "kWh"
			};
			gaszaehler = messpunktrest.WriteMesspunkt(gaszaehler).Result;

			Model.MessWert wert = new MessWert()
			{
				MesspunktID = gaszaehler.MesspunktID,
				Wert = 568.43M,
				/*Stamp = DateTime.Now das setzt die DB für uns, *
				//TODO: das liefert sie uns nach dem Speichern aber nicht zurück *
			};
			wert = messwertrest.WriteMesswert(wert).Result;

			/**/
		}
	}
}
