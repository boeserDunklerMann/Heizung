using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Heizung.Model;
using Heizung.Model.Attributes;
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

			using (MySqlCommand cmd = new MySqlCommand(SqlConstants.SQL_LoadWohnungen, connection))
			{
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
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

		#region Schreibe Tabellen
		public void WriteModel(BaseModel model)
		{
			string delete = "";
			string insert = "";
			string update = "";
			if (model is MessWert)
			{
				delete = SqlConstants.SQL_DeleteMesswert;
				insert = SqlConstants.SQL_InsertMesswert;
				update = SqlConstants.SQL_UpdateMesswert;
			}
			bool wasOpen = connection.State == System.Data.ConnectionState.Open;
			if (!wasOpen)
				connection.Open();
			if (model.DeleteMe)
			{
				using (MySqlCommand cmd = new MySqlCommand(delete, connection))
				{
					FillParameters(cmd.Parameters, model);
					cmd.ExecuteNonQuery();
				}
			}
			else
			{
				if (model.IsNew)
				{
					using (MySqlCommand cmd = new MySqlCommand(insert, connection))
					{
						FillParameters(cmd.Parameters, model);
						using (MySqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								model.ID = Convert.ToInt32(reader[0]);
							}
							else
							{
								// TODO: hier müsste man eigentlich eskalieren, da wir keine ID zurückbekommen haben.
							}
						}
					}
				}
				else
				{
					using(MySqlCommand cmd = new MySqlCommand(update, connection))
					{
						FillParameters(cmd.Parameters, model);
						cmd.ExecuteNonQuery();
					}
				}
			}
		}

		public void WriteMesswert(MessWert wert)
		{
			if (wert.Messpunkt != null)
				wert.MesspunktID = wert.Messpunkt.MesspunktID;
			WriteModel(wert);
		}
		public void WriteMesspunkt(Messpunkt messpunkt)
		{
			if (messpunkt.Raum != null)
				messpunkt.RaumID = messpunkt.Raum.RaumID;
			WriteModel(messpunkt);
		}
		#endregion

		private static void FillParameters(MySqlParameterCollection parameters, BaseModel model)
		{
			PropertyInfo[] pinfos = model.GetType().GetProperties();
			pinfos.ToList().ForEach(pi =>
			{
				Attribute sqlParamName = pi.GetCustomAttribute(typeof(SqlParameterNameAttribute));
				if (sqlParamName != null)
				{
					SqlParameterNameAttribute sqlParameterNameAttribute = (SqlParameterNameAttribute)sqlParamName;
					parameters.AddWithValue(sqlParameterNameAttribute.ParameterName, pi.GetValue(model));
				}
			});
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}