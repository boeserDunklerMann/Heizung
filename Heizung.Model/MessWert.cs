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

		[OmitDb]
		public Messpunkt Messpunkt { get; set; }

		/// <summary>
		/// Überschreiben wir und lesen es NICHT aus der DB <see cref="OmitDbAttribute"/>
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