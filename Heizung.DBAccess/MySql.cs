using System;
using System.Collections.Generic;
using Heizung.Model;
using MySql.Data.MySqlClient;

namespace Heizung.DBAccess
{
	public sealed class MySql : IDisposable
	{
		private readonly MySqlConnection connection;
		private static Lazy<MySql> lazy = new Lazy<MySql>(() => new MySql());
		public static MySql Instance => lazy.Value;

		#region Initialisierung
		private MySql()
		{
			connection = new MySqlConnection();
		}

		public bool SetConnection(string user, string password, string host, string dbName)
		{
			MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder()
			{
				Server = host,
				UserID = user,
				Password = password,
				Database = dbName
			};
			if (connection.ConnectionString.Equals(csb.ConnectionString))
				return true;
			if (connection.State == System.Data.ConnectionState.Closed)
				connection.ConnectionString = csb.ConnectionString;

			// TODO: herausfinden und loggen, ob und warum die Verbindung nicht geklappt hat
			return true;
		}
		#endregion

		#region Lade Tabellen
		public List<Wohnung> LoadAll()
		{
			List<Wohnung> wohnungen = LoadWohnungen();
			wohnungen.ForEach(w =>
			{
				List<Raum> raeume = LoadRaeume(w);
				raeume.ForEach(r =>
				{
					List<Messpunkt> mps = LoadMesspunkte(r);
					mps.ForEach(mp =>
					{
						List<MessWert> werte = LoadMesswerte(mp);
					});
				});
			});
			return wohnungen;
		}

		public List<Wohnung> LoadWohnungen()
		{
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen)
				connection.Open();
			List<Wohnung> retval = new List<Wohnung>();

			using(MySqlCommand cmd = new MySqlCommand(SqlConstants.SQL_LoadWohnungen, connection))
			{
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while(reader.Read())
					{
						Wohnung w = new Wohnung();
						w.FromReader(reader);
						retval.Add(w);
					}
				}
			}
			if (!wasOpen)
				connection.Clone();
			return retval;
		}

		/// <summary>
		/// Lädt alle Räume einer Wohnung
		/// </summary>
		/// <param name="wohnung"></param>
		/// <returns></returns>
		public List<Raum> LoadRaeume(Wohnung wohnung)
		{
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen)
				connection.Open();
			List<Raum> retval = new List<Raum>();
			wohnung.Raeume.Clear();

			using (MySqlCommand cmd = new MySqlCommand(SqlConstants.SQL_LoadRaeume, connection))
			{
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Raum raum = new Raum();
						raum.FromReader(reader);
						if (raum.WohnungID == wohnung.WohnungID)
						{
							raum.Wohnung = wohnung;
							wohnung.Raeume.Add(raum);
							retval.Add(raum);
						}
					}
				}
			}
			if (!wasOpen)
				connection.Clone();
			return retval;
		}

		/// <summary>
		/// Lädt alle Messpunkte eines Raums
		/// </summary>
		/// <returns></returns>
		public List<Messpunkt> LoadMesspunkte(Raum raum)
		{
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen)
				connection.Open();
			List<Messpunkt> retval = new List<Messpunkt>();
			raum.Messpunkte.Clear();

			using (MySqlCommand cmd = new MySqlCommand(SqlConstants.SQL_LoadMesspunkte, connection))
			{
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Messpunkt mp = new Messpunkt();
						mp.FromReader(reader);
						if (mp.RaumID == raum.RaumID)
						{
							mp.Raum = raum;
							raum.Messpunkte.Add(mp);
							retval.Add(mp);
						}
					}
				}
			}
			if (!wasOpen)
				connection.Clone();
			return retval;
		}

		/// <summary>
		/// Lädt die Messwerte eines Messpunkts
		/// </summary>
		/// <returns></returns>
		public List<MessWert> LoadMesswerte(Messpunkt messpunkt)
		{
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen)
				connection.Open();
			List<MessWert> retval = new List<MessWert>();
			messpunkt.Werte.Clear();
			using (MySqlCommand cmd = new MySqlCommand(SqlConstants.SQL_LoadMesswerte, connection))
			{
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						MessWert mw = new MessWert();
						mw.FromReader(reader);
						if (mw.MesspunktID == messpunkt.MesspunktID)
						{
							mw.Messpunkt = messpunkt;
							messpunkt.Werte.Add(mw);
							retval.Add(mw);
						}
					}
				}
			}
			if (!wasOpen)
				connection.Clone();
			return retval;
		}
		#endregion

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}