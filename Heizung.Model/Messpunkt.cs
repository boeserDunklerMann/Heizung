using Heizung.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Model
{
	public sealed class Messpunkt : BaseModel
	{
		[SqlParameterName("?mpid")]
		public int MesspunktID { get => ID; set => ID = value; }

		[SqlParameterName("?rid")]
		public int RaumID { get; set; }

		[SqlParameterName("?einheit")]
		[String(10)]
		public string Einheit { get; set; }

		[OmitDb]
		public List<MessWert> Werte { get; } = new List<MessWert>();

		public override bool Equals(object obj)
		{
			if (obj is Messpunkt)
				return ((Messpunkt)obj).ID == ID;
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}