using System;

namespace Heizung.Model.Attributes
{
	/// <summary>
	/// Attribut-Klasse, welche definiert, aus welchem DB-Feld eine Property geladen wird.
	/// Dies ist dort nützlich, wo die Property eine andere Bezeichnung hat, als das Feld in der Tabelle.
	/// </summary>
	public class DbFieldAttribute : Attribute
	{
		public string DbFieldName { get; private set; }
		public DbFieldAttribute(string dbFieldName)
		{
			DbFieldName = dbFieldName;
		}
	}
}
