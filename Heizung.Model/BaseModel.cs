using Heizung.Model.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace Heizung.Model
{
	/// <summary>
	/// Basisklasse für alle Datenmodelle
	/// </summary>
	public abstract class BaseModel
	{
		[OmitDb]	// ID-Feld heißt in jeder Tabelle anders, welches in den abgeleiteten Klassen definiert wird.
		public int ID { get; set; } = 0;

		/// <summary>
		/// brauchen wir, um später festzustellen, ob ein INSERT oder UPDATE gemacht werden soll
		/// </summary>
		[OmitDb]
		public bool IsNew => ID == 0;

		/// <summary>
		/// Flag, das anzeigt, ob dieser Datensatz gelöscht werden soll
		/// </summary>
		[OmitDb]
		public bool DeleteMe { get; set; } = false;
		
		/// <summary>
		/// die meisten Datenobjekte haben eine Bezeichnung (außer <see cref="MessWert"/>)
		/// </summary>
		[DbField("Bez")]
		public string Bezeichnung { get; set; }
		/// <summary>
		/// Liest die Daten aus einem DataReader per Reflection in die Properties dieser und der abgeleiteten Klassen
		/// </summary>
		/// <param name="reader"></param>
		/// <remarks>
		/// Das funktioniert bei solch kleinen Datenmengen sehr gut, bei größeren ist Reflection eine ziemliche Performance-Bremse
		/// </remarks>
		public virtual void FromReader(System.Data.IDataReader reader)
		{
			Type myType = this.GetType();
			// gib mir alle Properties von mir selbst
			PropertyInfo[] properties = myType.GetProperties();
			properties.ToList().ForEach(pi =>
			{
				Attribute dbField = pi.GetCustomAttribute(typeof(DbFieldAttribute));
				Attribute omitField = pi.GetCustomAttribute(typeof(OmitDbAttribute));
				// überspringe Properties, die mit OmitDb attributiert sind
				if (omitField == null)
				{
					string columnName;
					if (dbField != null)
						columnName = ((DbFieldAttribute)dbField).DbFieldName;
					else
						columnName = pi.Name;
					object data = reader[columnName];
					if (data is DBNull)
						pi.SetValue(this, null);
					else
						pi.SetValue(this, data);
				}
			});
		}
		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}