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
where WertID=?wid;";

		public const string SQL_LoadMesspunkte = "select MesspunktID, RaumID, Bez, Einheit from Messpunkte;";
		public const string SQL_DeleteMesspunkt = "delete from Messpunkte where MesspunktID=?mpid;";
		public const string SQL_InsertMesspunkt = @"insert into Messpunkte (RaumID, Bez, Einheit) values (?rid, ?bez, ?einheit);
select LAST_INSERT_ID() as mpid;";
		public const string SQL_UpdateMesspunkt = @"update Messpunkte
set
	RaumID=?rid,
	Bez=?bez,
	Einheit=?einheit
Where MesspunktID=?mpid;";

		public const string SQL_LoadRaeume = "select RaumID, WohnungID, Bez from Raeume;";
		public const string SQL_DeleteRaum = "delete from Raeume where RaumID=?rid;";
		public const string SQL_InsertRaum = @"insert into Raeume (WohnungID, Bez) values (?wid, ?bez);
select LAST_INSERT_ID() as rid;";
		public const string SQL_UpdateRaum = @"update Raeume
set
	WohnungID=?wid,
	Bez=?bez
where RaumID=?rid";

		public const string SQL_LoadWohnungen = "select WohnungID, Bez from Wohnungen;";
		public const string SQL_DeleteWohnung = "delete from Wohnungen where WohnungID=?wid;";
		public const string SQL_InsertWohnung = @"insert into Wohnungen (Bez) values (?bez);
select LAST_INSERT_ID() as wid;";
		public const string SQL_UpdateWohnung = @"update Wohnungen
set
	Bez=?bez
where WohnungID=?wid;";

	}
}