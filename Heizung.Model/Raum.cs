using Heizung.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Model
{
	public sealed class Raum : BaseModel
	{
		public int RaumID { get => ID; set => ID = value; }

		//[DbField("Bez")]
		//public string Bezeichnung { get; set; }

		[OmitDb]
		public Wohnung Wohnung { get; set; }

		public int WohnungID { get; set; }

		[OmitDb]
		public List<Messpunkt> Messpunkte { get; } = new List<Messpunkt>();

		public override bool Equals(object obj)
		{
			if (obj is Raum)
				return ((Raum)obj).RaumID == RaumID;
			else
				return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}