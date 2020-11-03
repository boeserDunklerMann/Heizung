using Heizung.Model.Attributes;
using System.Collections.Generic;

namespace Heizung.Model
{
	public sealed class Wohnung : BaseModel
	{
		public int WohnungID { get => ID; set => ID = value; }

		[OmitDb]
		public List<Raum> Raeume { get; } = new List<Raum>();
		public override bool Equals(object obj)
		{
			if (obj is Wohnung)
				return ((Wohnung)obj).WohnungID == WohnungID;
			return false;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}