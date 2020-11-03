using System;
using System.Collections.Generic;
using System.Text;

namespace Heizung.ReSTWrapper
{
	public class Messwert:BaseWrapper
	{
		public Messwert():base()
		{
			reSTUrl = "api/heizung/Messwert";
		}
	}
}
