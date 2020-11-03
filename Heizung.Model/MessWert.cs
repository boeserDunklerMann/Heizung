using Heizung.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Model
{
	public sealed class MessWert : BaseModel
	{
		[SqlParameterName("?wid")]
		public int WertID { get => ID; set => ID = value; }

		/// <summary>
		/// Überschreiben wir und lesen es NICHT aus der DB <see cref="OmitDbAttribute"/>
		/// Am Messwert selbst gibt es nämlich keine Bezeichnung, das
		/// <see cref="BaseModel.FromReader(System.Data.IDataReader)"/> würde sonst eskalieren.
		/// </summary>
		[OmitDb]
		public new string Bezeichnung { get; set; }

		[SqlParameterName("?mpid")]
		public int MesspunktID { get; set; }

		public DateTime Stamp { get; set; }

		[SqlParameterName("?wert")]
		public decimal Wert { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is MessWert)
				return ((MessWert)obj).WertID == WertID;
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}