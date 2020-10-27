using Heizung.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.Model
{
	public sealed class MessWert : BaseModel
	{
		public int WertID { get => ID; set => ID = value; }

		[OmitDb]
		public Messpunkt Messpunkt { get; set; }

		public DateTime Stamp { get; set; }

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