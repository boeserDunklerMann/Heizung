using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.DBAccess
{
	public static class SqlConstants
	{
		public const string SQL_LoadMesswerte = @"select WertID,
													  MesspunktID,
													  Stamp,
													  Wert
												from Werte;";
		public const string SQL_DeleteMesswert = "delete from Werte where WertID=?wid;";
		public const string SQL_InsertMesswert = @"insert into Werte (MesspunktID, Wert) values (?mpid, ?wert);
select LAST_INSERT_ID()as wid;";
		public const string SQL_UpdateMesswert = @"update Werte
set
	MesspunktID=?mpid,
	Wert=?wert
where WertID=?wid";

		public const string SQL_LoadMesspunkte = "select MesspunktID, RaumID, Bez, Einheit from Messpunkte;";
		public const string SQL_LoadRaeume = "select RaumID, WohnungID, Bez from Raeume;";
		public const string SQL_LoadWohnungen = "select WohnungID, Bez from Wohnungen;";
	}
}